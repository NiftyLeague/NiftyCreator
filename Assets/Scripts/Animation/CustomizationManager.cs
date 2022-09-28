using Beebyte.Obfuscator;
using NaughtyAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class CustomizationManager : MonoBehaviour
{
	public static CustomizationManager I;

	public SpriteRasterizer spriteRasterizer;
	public Transform floatParent;
	public Character character;
	[SerializeField]
	private CharacterType characterType;
	public UIOptions categoryUIOption;
	public UIOptions[] subcategoryUIOptions;
	public float rotationRate;
	public float rotationStart;
	public float floatRate;
	public float floatDomain;
	public Material defaultMaterial;
	public int frame;
	public Texture2D swapTexture;
	public float randomizationProbabiity;
	public Color32[] colors = PaletteInfo.colors;
	public List<Sprite> sprites;
	public static Action onChange;
	public static Action onInitialized;
	public static Action onOptionBurst;
	public static Action<string> onSubmitTraits;

	public static int NumTraitsKeys { get { return traitKeys.Length; } }

	private CharacterCustomizer cs { get { return character.GetComponent<CharacterCustomizer>(); } }
	private static Dictionary<CharacterType, Dictionary<AnimationLayer, List<AnimationLayerVariation>>> charLayerMap;

	private static CharacterType[] allCharacterTypes = new CharacterType[6] {
		CharacterType.Alien, CharacterType.Ape, CharacterType.Cat, CharacterType.Doge, CharacterType.Frog, CharacterType.Human };
	private static string[] categoryOptionKeys = new string[] { Names.Character, Names.Head, Names.Clothing, Names.Accessories, Names.Items };
	private static Dictionary<string, string[]> subcategoriesOptionIds = new Dictionary<string, string[]> {
		{ Names.Character, new string[] { Names.Tribe, Names.SkinColor, Names.FurColor, Names.EyeColor, Names.PupilColor } },
		{ Names.Head, new string[] { Names.Hair, Names.Mouth, Names.Beard } },
		{ Names.Clothing, new string[] { Names.Top, Names.Outerwear, Names.Print, Names.Bottom, Names.Footwear, Names.Belt } },
		{ Names.Accessories, new string[] { Names.Hat, Names.Eyewear, Names.Piercing, Names.Wrist, Names.Hands, Names.Neckwear } },
		{ Names.Items, new string[] { Names.LeftItem, Names.RightItem } },
	};
	private static string[] traitKeys = new string[] {
		Names.Tribe, Names.SkinColor, Names.FurColor, Names.EyeColor, Names.PupilColor, Names.Hair, Names.Mouth, Names.Beard, Names.Top,
		Names.Outerwear, Names.Print, Names.Bottom, Names.Footwear, Names.Belt, Names.Hat, Names.Eyewear, Names.Piercing, Names.Wrist,
		Names.Hands, Names.Neckwear, Names.LeftItem, Names.RightItem,
	};

	private static Dictionary<string, string[]> subcategoryOptionKeys = new Dictionary<string, string[]> {
		{ Names.Tribe, new string[] { "Ape", "Human", "Doge", "Frog", "Cat", "Alien" } },
	};

	private static Dictionary<string, AccessoryType> accessoryTypeMap = new Dictionary<string, AccessoryType> {
		{ Names.Beard, AccessoryType.Beard },
		{ Names.Belt, AccessoryType.Belt },
		{ Names.Bottom, AccessoryType.Bottom },
		{ Names.Eyewear, AccessoryType.Eyewear },
		{ Names.Footwear, AccessoryType.Footwear },
		{ Names.Hair, AccessoryType.Hair },
		{ Names.Hands, AccessoryType.Hands },
		{ Names.Hat, AccessoryType.Hat },
		{ Names.LeftItem, AccessoryType.LeftItem },
		{ Names.Mouth, AccessoryType.Mouth },
		{ Names.Neckwear, AccessoryType.Neckwear },
		{ Names.Outerwear, AccessoryType.Outerwear },
		{ Names.Piercing, AccessoryType.Piercing },
		{ Names.Print, AccessoryType.Print },
		{ Names.RightItem, AccessoryType.RightItem },
		{ Names.Top, AccessoryType.Top },
		{ Names.Wrist, AccessoryType.Wrist }
	};

	private static Dictionary<string, Trait> traits = new Dictionary<string, Trait>
	{
		{ Names.Tribe, TraitInfo.EmptyTrait },
		{ Names.SkinColor, TraitInfo.EmptyTrait },
		{ Names.FurColor, TraitInfo.EmptyTrait },
		{ Names.EyeColor, TraitInfo.EmptyTrait },
		{ Names.PupilColor, TraitInfo.EmptyTrait },
		{ Names.Hair, TraitInfo.EmptyTrait },
		{ Names.Mouth, TraitInfo.EmptyTrait },
		{ Names.Beard, TraitInfo.EmptyTrait },
		{ Names.Top, TraitInfo.EmptyTrait },
		{ Names.Outerwear, TraitInfo.EmptyTrait },
		{ Names.Print, TraitInfo.EmptyTrait },
		{ Names.Bottom, TraitInfo.EmptyTrait },
		{ Names.Footwear, TraitInfo.EmptyTrait },
		{ Names.Belt, TraitInfo.EmptyTrait },
		{ Names.Hat, TraitInfo.EmptyTrait },
		{ Names.Eyewear, TraitInfo.EmptyTrait },
		{ Names.Piercing, TraitInfo.EmptyTrait },
		{ Names.Wrist, TraitInfo.EmptyTrait },
		{ Names.Hands, TraitInfo.EmptyTrait },
		{ Names.Neckwear, TraitInfo.EmptyTrait },
		{ Names.LeftItem, TraitInfo.EmptyTrait },
		{ Names.RightItem, TraitInfo.EmptyTrait },
	};

	private static Dictionary<AnimationLayer, string> animationLayerSubcategoryMap = new Dictionary<AnimationLayer, string>();

	private static AnimationLayerVariation.ColorVariationInfo[] eyeColors;
	private static HashSet<Trait> removedTraits = new HashSet<Trait>();
	private static HashSet<string> existingCharacters = new HashSet<string>();
	private static bool isInitialized = false;
	private bool isCustomizationScene = false;
	private float rotationTime = 0f;
	private int rotationFrame = 1;

	private float burstClickClearTime = 0f;
	private int numBurstClicks = 0;
	private float lastChangeTime = 0f;
	private float lastRemovedTraitsFetchTime = 0f;
	private string lastTraitStr = "";

	private void Awake()
	{
		I = this;
#if !UNITY_STANDALONE || UNITY_EDITOR
		SceneManager.sceneLoaded += OnSceneLoaded;
		DontDestroyOnLoad(gameObject);
		isInitialized = false;
		isCustomizationScene = SceneManager.GetActiveScene().name == NiftyLeague.Scenes.NiftyCreator;
		if (isCustomizationScene)
		{
			InvokeRepeating(nameof(CleaupUnusedFrames), 10f, 10f);
			//StartCoroutine(UpdateRemovedTraits());
		}
		lastRemovedTraitsFetchTime = Time.time;
#endif
	}

	[SkipRename]
	private void CleaupUnusedFrames()
	{
		StartCoroutine(AnimationManager.CleanupUnusedFrames());
	}

#if !UNITY_STANDALONE || UNITY_EDITOR
	private IEnumerator UpdateRemovedTraits()
	{
		yield return new WaitForSeconds(2f);
		float delay = 0.1f;
		while (true)
		{
			yield return new WaitForSeconds(delay);
			delay = 6f;
			if (CharacterCreatorLevel.IsMinting() || Time.time - lastChangeTime > 9f && Time.time - lastRemovedTraitsFetchTime < 10f)
			{
				continue;
			}
			if (string.IsNullOrEmpty(CustomizationLauncher.apiUrl) || CustomizationLauncher.apiNetwork == "localhost")
			{
				continue;
			}
			lastRemovedTraitsFetchTime = Time.time;
			UnityWebRequest www = UnityWebRequest.Get(CustomizationLauncher.apiUrl);
			yield return www.SendWebRequest();
			try
			{
				if (www.result != UnityWebRequest.Result.Success)
				{
					Debug.Log(www.error);
					continue;
				}
				JObject response = JObject.Parse(www.downloadHandler.text);
				if (response["data"]["removedTraits"].Type == JTokenType.Array)
				{
					SetRemovedTraits(response["data"]["removedTraits"].Select(s => s.ToObject<int>()).ToArray());
				}
				if (response["data"]["characters"].Type == JTokenType.Array)
				{
					SetExistingCharacters(response["data"]["characters"].Select(s => s.ToObject<int[]>()).ToList());
				}
			}
			catch (Exception e)
			{
				Debug.Log(e);
			}
		}
	}
#endif

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
	{
		isCustomizationScene = scene.name == NiftyLeague.Scenes.NiftyCreator;
	}

	private void Start()
	{
		Initalize();
	}

	private void LateUpdate()
	{
		if (!isCustomizationScene)
		{
			return;
		}

		if (frame == -1)
		{
			rotationTime += Time.deltaTime;
			rotationFrame = Mathf.RoundToInt(rotationTime * rotationRate) % AnimationTags.Turnaround.frameCount;
			cs.SetFrame(AnimationTagType.Turnaround, rotationFrame, true);
		}
		else if (frame < -1)
		{
			cs.SetFrameAbsolute(Mathf.RoundToInt(Time.time * 10f) % AnimationTags.totalFrameCount, true);
		}
		else
		{
			if (rotationFrame != 1)
			{
				rotationTime += Time.deltaTime;
				rotationFrame = Mathf.RoundToInt(rotationTime * rotationRate) % AnimationTags.Turnaround.frameCount;
				cs.SetFrame(AnimationTagType.Turnaround, rotationFrame, true);
			}
			else
			{
				cs.SetFrameAbsolute(frame, true);
			}
		}
		floatParent.localPosition = new Vector3(0f, (Mathf.Sin(Time.time * floatRate) * floatDomain) + (Mathf.Sin(Time.time * floatRate * 0.6f) * 0.8f * floatDomain), 0f);
	}

	private void Initalize()
	{
		character.enabled = false;
		character.GetComponent<CharacterAnimator>().enabled = false;
		if (isCustomizationScene && CustomizationLauncher.removedTraits != null)
		{
			SetRemovedTraits(CustomizationLauncher.removedTraits);
		}
		InitializeCharacterLayerMap();
		InitializeEyeColors();
		if (isCustomizationScene)
		{
			categoryUIOption.Initialize(new SimpleOptionEnumrator("category", categoryOptionKeys, categoryOptionKeys, OnCategoryOptionsChanged, null, false));
			//actionUIOption.Initialize(new SimpleOptionEnumrator("action", new string[] { "POSE", "ROTATE", "-DEBUG-" }, new object[] { 0, -1, -2 }, OnActionChange, null, false));
		}
		isInitialized = true;
		if (onInitialized != null)
		{
			onInitialized();
		}
	}

	internal void SetExistingCharacters(List<int[]> characters)
	{
		existingCharacters.Clear();
		foreach (int[] character in characters)
		{
			existingCharacters.Add(string.Join(",", character));
		}
	}

	private void SetRemovedTraits(int[] removed)
	{
		removedTraits = new HashSet<Trait>();
#if !UNITY_STANDALONE || UNITY_EDITOR
		HashSet<int> removedSet = new HashSet<int>(removed);
		List<KeyValuePair<string, Trait>> usedRemoveTraits = new List<KeyValuePair<string, Trait>>();
		foreach (int t in removedSet)
		{
			Trait trait = TraitInfo.traits[t];
			removedTraits.Add(trait);
			var kvPair = traits.FirstOrDefault(e => e.Value.id == trait.id);
			if (kvPair.Value.id != TraitInfo.EmptyTrait.id)
			{
				usedRemoveTraits.Add(kvPair);
				traits[kvPair.Key] = TraitInfo.EmptyTrait;
			}
		}

		if (usedRemoveTraits.Count > 0)
		{
			var newTraits = traits.ToDictionary(e => e.Key, e => e.Value.id);
			SetFromTraits(newTraits);
			CharacterTypeChanged(false);
			ReselectOptions();
			Debug.Log($"Selected removed traits {usedRemoveTraits.Count}");
			var kv = XRandom.NextMember(usedRemoveTraits);
			var itemName = kv.Value.name.ToLower();
			string displayName = TraitInfo.displayNameMap.ContainsKey(itemName) ? TraitInfo.displayNameMap[itemName] : null;

			if (!string.IsNullOrEmpty(displayName))
			{
				string tip = $"\"{displayName}\" {kv.Key} JUST WENT OUT OF STOCK".ToUpper();
				CharacterCreatorLevel.DisplayTvTip(tip, 15f);
			}
		}
#endif
	}

	public void ReselectOptions()
	{
		categoryUIOption.Reselect();
		for (int i = 0; i < subcategoryUIOptions.Length; i++)
		{
			if (i < subcategoriesOptionIds[categoryUIOption.GetCurrent().ToString()].Length)
			{
				try
				{
					subcategoryUIOptions[i].Reselect();
				}
				catch { }
			}
		}
	}

	public static bool IsInitialized()
	{
		return isInitialized;
	}

	private void OnChange()
	{
		if (onChange != null)
		{
			onChange();
		}
		lastChangeTime = Time.time;
	}

	public static void GenerateSprites(Dictionary<string, Trait> traits, Action<List<Sprite>, string> onRasterizationComplete)
	{
		string traitStr = "";
		foreach (var t in traitKeys)
		{
			traitStr += $"{t}:{traits[t].id}";
		}
		I.spriteRasterizer.RasterizeAllFrames(traits, Utils.GetMD5Hash(traitStr), onRasterizationComplete);
	}

	private void OnRasterizationComplete(List<Sprite> sprites, string traitHash)
	{
		this.sprites = sprites;
	}

	public Texture2D GenerateTexture2D(int frame)
	{
		var texs = spriteRasterizer.RasterizeFrame(frame);
		return texs;
	}

	private void InitializeEyeColors()
	{
		(_, var eyes) = GetHeadAndEyesVariations(CharacterType.Ape);
		eyeColors = eyes.colorVariations;
	}

	private object GetCurrentSelection(string optionId)
	{
		switch (optionId)
		{
		case Names.Tribe:
			return AnimationManager.GetCharacterString(character.type);
		case Names.SkinColor:
			return cs.GetCurrentBodyColorInfo(0);
		case Names.FurColor:
			return cs.GetCurrentBodyColorInfo(characterType == CharacterType.Cat ? 2 : 1);
		case Names.EyeColor:
		case Names.PupilColor:
			int colorIndex = optionId == Names.EyeColor ? 0 : 1;
			var colors = cs.GetCurrentEyeColorInfo(colorIndex);
			if (colors != null)
			{
				foreach (var cv in eyeColors)
				{
					if (cv.to[colorIndex].SameColorAs(colors.to[colorIndex]))
					{
						return cv;
					}
				}
			}
			return null;
		}
		return null;
	}

	private AccessoryOptionEnumrator.AccessoryOption GetCurrentAccessorySelection(string optionId, AnimationLayerVariation[] variations)
	{
		return cs.GetCurrentAccessoryOption(variations);
	}

	public static void SetActionState(int state, bool setOption)
	{
#if WEBGL_MOBILE_BUILD
		state = 0;
#endif
		if (state != I.frame)
		{
			I.OnChange();
			if (state == -1 && I.rotationFrame == 1)
			{
				I.rotationTime = I.rotationStart;
			}
			else if (state == -2)
			{
				I.rotationFrame = 1;
				I.rotationTime = I.rotationStart;
			}
			I.frame = state;
		}
#if !UNITY_STANDALONE || UNITY_EDITOR
		if (setOption)
		{
			CharacterCreatorLevel.SetPoseRotate(state == 0);
		}
#endif
	}

	private void OnCategoryOptionsChanged(OptionEnumarator options)
	{
		OnChange();
		string category = options.Current.Key;
		for (int i = 0; i < subcategoryUIOptions.Length; i++)
		{
			bool subcategoryActive = false;
			bool soldOut = false;
			if (i < subcategoriesOptionIds[category].Length)
			{
				string subcategoryId = subcategoriesOptionIds[category][i];
				if (accessoryTypeMap.ContainsKey(subcategoryId))
				{
					var layer = AnimationManager.GetLayer(accessoryTypeMap[subcategoryId], characterType);
					if (layer)
					{
						animationLayerSubcategoryMap[layer] = subcategoryId;
						var variations = layer.GetCharacterVariations(characterType);
						subcategoryUIOptions[i].Initialize(new AccessoryOptionEnumrator(layer, variations.Select(v => v.accessoryInfo.displayName).ToArray(), variations, OnAccessoryOptionsChanged, GetCurrentAccessorySelection(subcategoryId, variations), true, $"--{subcategoryId.Replace(" ", "-")}--"));
						subcategoryActive = true;
					}
					else
					{
						subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(subcategoryId, new string[] { subcategoryId }, new string[] { subcategoryId }, null, null, false));
						subcategoryActive = false;
					}
				}
				else if (subcategoryOptionKeys.ContainsKey(subcategoryId))
				{
					subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(subcategoryId, subcategoryOptionKeys[subcategoryId], subcategoryOptionKeys[subcategoryId], OnSubcategoryOptionsChanged, GetCurrentSelection(subcategoryId), false));
					subcategoryActive = true;
				}
				else if (category != Names.Character)
				{
					subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(subcategoryId, new string[] { subcategoryId }, new string[] { subcategoryId }, OnSubcategoryOptionsChanged, GetCurrentSelection(subcategoryId), false));
				}
				else if (subcategoryId == Names.SkinColor || subcategoryId == Names.FurColor || subcategoryId == Names.EyeColor || subcategoryId == Names.PupilColor)
				{
					subcategoryActive = subcategoryUIOptions[i].GetId().ToString().ToLower().Contains("color");
				}
				soldOut = subcategoryActive && subcategoryUIOptions[i].IsSoldOut;
				subcategoryActive = subcategoryActive && subcategoryUIOptions[i].GetCount() > 0 && subcategoryUIOptions[i].GetCurrent() != null;
			}
			else
			{
				subcategoryActive = false;
			}
			if (soldOut)
			{
				subcategoryUIOptions[i].gameObject.SetActive(true);
			}
			else
			{
				subcategoryUIOptions[i].gameObject.SetActive(subcategoryActive);
			}
		}
		if (category == Names.Items)
		{
			SetActionState(0, true);
		}
	}

	private void OnSubcategoryOptionsChanged(OptionEnumarator options)
	{
		OnChange();
		object key = options.Id;
		if (key is string && (string)key == Names.Tribe)
		{
			characterType = AnimationManager.GetCharacterType((string)options.Current.Value);
			CharacterTypeChanged(characterType != character.type);
		}
		TraitChanged(options);
	}

	private void OnAccessoryOptionsChanged(OptionEnumarator options)
	{
		OnChange();
		object key = options.Id;
		if (options is AccessoryOptionEnumrator)
		{
			SetLayerVariation(key as AnimationLayer, options.Current.Value as AccessoryOptionEnumrator.AccessoryOption);
		}
		TraitChanged(options);
		if (isCustomizationScene)
		{
			if (Time.time - burstClickClearTime > 1.5f)
			{
				numBurstClicks = 0;
				burstClickClearTime = Time.time;
			}
			else if (options.SelectionIndex > 2)
			{
				numBurstClicks++;
				if (numBurstClicks > 5)
				{
					if (onOptionBurst != null)
					{
						onOptionBurst.Invoke();
					}
				}
			}
		}
	}

	private void SetLayerVariation(AnimationLayer layer, AccessoryOptionEnumrator.AccessoryOption option)
	{
		if (option.colorVariationIndex < 0)
		{
			cs.SetLayer(layer, option.animationLayerVariation);
		}
		else
		{
			cs.SetLayer(layer, option.animationLayerVariation, option.animationLayerVariation.colorVariations[option.colorVariationIndex]);
		}
	}

	private void SetLayerVariation(AnimationLayer layer, AnimationLayerVariation[] accessoryVariations, Trait trait)
	{
		if (trait.id == TraitInfo.EmptyTrait.id)
		{
			cs.SetLayer(layer, null);
			return;
		}

		AnimationLayerVariation alv = AnimationManager.I.variationTraitMap[trait.id];
		AnimationLayerVariation characterAlv = accessoryVariations.First(v => v.traitId == alv.traitId);
		if (alv.colorVariations.Length <= 1)
		{
			cs.SetLayer(layer, characterAlv);
		}
		else
		{
			cs.SetLayer(layer, characterAlv, characterAlv.colorVariations[trait.id - alv.traitId]);
		}
	}

	public static Trait GetTraitFromOption(OptionEnumarator options, KeyValuePair<string, object> option)
	{
		string id;
		if (options is AccessoryOptionEnumrator)
		{
			id = animationLayerSubcategoryMap[options.Id as AnimationLayer];
		}
		else
		{
			id = (string)options.Id;
		}
		CharacterType characterType = I.characterType;

		string characterString = AnimationManager.GetCharacterString(characterType);
		Trait trait = TraitInfo.EmptyTrait;
		switch (id)
		{
		case Names.Tribe:
			trait = TraitInfo.GetTrait(characterType, characterString);
			break;
		case Names.SkinColor:
			trait = TraitInfo.GetTrait(characterType, $"{option.Key} {characterString}");
			break;
		case Names.FurColor:
			trait = TraitInfo.GetTrait(characterType, $"{option.Key.Replace(" Fur", "")} {characterString} Fur");
			break;
		case Names.EyeColor:
		case Names.PupilColor:
			trait = TraitInfo.GetTrait(characterType, option.Key);
			break;
		default:
			if (string.IsNullOrEmpty(option.Key) || !(option.Value is AccessoryOptionEnumrator.AccessoryOption))
			{
				break;
			}
			AccessoryOptionEnumrator.AccessoryOption opt = option.Value as AccessoryOptionEnumrator.AccessoryOption;
			string accessoryName = Utils.ToFirstLetterUppercase(opt.ToString());
			trait = TraitInfo.GetTrait(opt.animationLayerVariation.accessoryInfo.accessoryType, accessoryName);
			break;
		}

		if (!string.IsNullOrEmpty(option.Key) && option.Key != Names.Character && trait.id == 0)
		{
			Debug.LogWarning($"Trait '{id}: {option.Key}' for character '{characterType}' not found. Defaulting to EmptyTrait");
		}
		return trait;
	}

	private void TraitChanged(OptionEnumarator options)
	{
		string id = null;
		if (options is AccessoryOptionEnumrator)
		{
			id = animationLayerSubcategoryMap[options.Id as AnimationLayer];
		}
		else
		{
			id = (string)options.Id;
		}
		if (!string.IsNullOrEmpty(id))
		{
			traits[id] = GetTraitFromOption(options, options.Current);
			CancelInvoke(nameof(SetUniqueness));
			Invoke(nameof(SetUniqueness), 0.1f);
		}

		if (id == Names.LeftItem || id == Names.RightItem)
		{
			SetActionState(0, true);
		}
	}

	[SkipRename]
	private void SetUniqueness()
	{
#if !UNITY_STANDALONE || UNITY_EDITOR
		string traitStr = string.Join(",", traits.Select(e => e.Value.id));
		if (traitStr != lastTraitStr && lastTraitStr != "")
		{
			bool duplicated = existingCharacters.Contains(traitStr);
			CharacterCreatorLevel.SetUniqueness(!duplicated);
		}
		lastTraitStr = traitStr;
#endif
	}

	private void InitializeCharacterLayerMap()
	{
		charLayerMap = new Dictionary<CharacterType, Dictionary<AnimationLayer, List<AnimationLayerVariation>>>();
		foreach (var ct in allCharacterTypes)
		{
			charLayerMap.Add(ct, new Dictionary<AnimationLayer, List<AnimationLayerVariation>>());
			foreach (var al in AnimationManager.I.layers)
			{
				charLayerMap[ct].Add(al, new List<AnimationLayerVariation>());
				foreach (var av in al.variations)
				{
					if (av.characterType == ct || av.characterType == CharacterType.Share)
					{
						charLayerMap[ct][al].Add(av);
					}
				}
			}
		}
	}

	public static void Reset()
	{
		isInitialized = false;
		I = null;
		onChange = null;
		onInitialized = null;
		onSubmitTraits = null;
	}

	public static void ResetCharacter()
	{
		//I.character.type = CharacterType.Share;
		I.categoryUIOption.SetIndex(0);
		I.InitializeCharacter(true);
		I.CharacterTypeChanged(true);
	}

	private void CharacterTypeChanged(bool resetTraits = false)
	{
		animationLayerSubcategoryMap.Clear();
		if (resetTraits)
		{
			foreach (var k in traits.Keys.ToArray())
			{
				if (k != Names.Tribe)
				{
					traits[k] = TraitInfo.EmptyTrait;
				}
			}
		}

		(AnimationLayerVariation headLayer, AnimationLayerVariation eyesLayer) = InitializeCharacter();

		if (!isCustomizationScene || (categoryUIOption.GetCurrent() != null && categoryUIOption.GetCurrent().ToString() != Names.Character))
		{
			return;
		}

		int i = 1;
		string id = subcategoriesOptionIds[Names.Character][1];
		subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(id, headLayer.colorVariations.Select((v, i) => $"{PaletteInfo.colorMap[v.to[0]].name}").ToArray(), headLayer.colorVariations, OnSkinColorChanged, GetCurrentSelection(id), false));
		subcategoryUIOptions[i].gameObject.SetActive(true);
		i++;

		if (characterType == CharacterType.Ape || characterType == CharacterType.Cat || characterType == CharacterType.Doge)
		{
			id = subcategoriesOptionIds[Names.Character][2];
			subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(id, headLayer.colorVariations.Select((v, i) => $"{PaletteInfo.colorMap[v.to[v.to.Length - 1]].name} Fur").ToArray(), headLayer.colorVariations, OnSecondarySkinColorChanged, GetCurrentSelection(id), false));
			subcategoryUIOptions[i].gameObject.SetActive(true);
			i++;
		}
		id = subcategoriesOptionIds[Names.Character][3];
		subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(id, eyeColors.Select((v, i) => $"{PaletteInfo.colorMap[v.to[0]].name} Eyes").ToArray(), eyeColors, OnEyeColorChanged, GetCurrentSelection(id), false));
		subcategoryUIOptions[i].gameObject.SetActive(true);
		i++;

		if (characterType != CharacterType.Doge && characterType != CharacterType.Alien)
		{
			id = subcategoriesOptionIds[Names.Character][4];
			subcategoryUIOptions[i].Initialize(new SimpleOptionEnumrator(id, eyeColors.Select((v, i) => $"{PaletteInfo.colorMap[v.to[v.to.Length - 1]].name} Pupils").ToArray(), eyeColors, OnSecondaryEyeColorChanged, GetCurrentSelection(id), false));
			subcategoryUIOptions[i].gameObject.SetActive(true);
			i++;
		}

		for (; i < subcategoryUIOptions.Length; i++)
		{
			subcategoryUIOptions[i].gameObject.SetActive(false);
		}
	}

	private (AnimationLayerVariation, AnimationLayerVariation) InitializeCharacter(bool force = false)
	{
		AnimationLayerVariation headLayer = null;
		AnimationLayerVariation eyesLayer = null;
		List<AnimationLayerVariation> layers = new List<AnimationLayerVariation>();
		if (characterType != character.type || force)
		{
			character.type = characterType;
			foreach (var layerAndVariations in charLayerMap[characterType])
			{
				AnimationLayer l = layerAndVariations.Key;
				List<AnimationLayerVariation> vars = layerAndVariations.Value;
				if (l.Required)
				{
					if (vars.Count > 0)
					{
						int randomSelection = UnityEngine.Random.Range(0, vars.Count);
						AnimationLayerVariation variation = vars[randomSelection];
						layers.Add(variation);
					}
				}
			}

			cs.SetLayers(layers.ToArray());
			cs.CharacterTypeChanged();
			cs.ReplaceBodyAndEyeColors();
		}

		foreach (var layerAndVariations in charLayerMap[characterType])
		{
			AnimationLayer l = layerAndVariations.Key;
			List<AnimationLayerVariation> vars = layerAndVariations.Value;
			if (l.Required && vars.Count > 0)
			{
				if (vars[0].variationName.EndsWith("_head"))
				{
					headLayer = vars[0];
				}
				else if (vars[0].variationName.EndsWith("_eyes"))
				{
					eyesLayer = vars[0];
				}
			}
		}
		return (headLayer, eyesLayer);
	}

	private void OnSkinColorChanged(OptionEnumarator options)
	{
		OnChange();
		SetSkinColor(options.Current.Value as AnimationLayerVariation.ColorVariationInfo);
		TraitChanged(options);
	}

	private void SetSkinColor(AnimationLayerVariation.ColorVariationInfo cvi)
	{
		switch (characterType)
		{
		case CharacterType.Alien:
		case CharacterType.Frog:
		case CharacterType.Human:
		case CharacterType.Ape:
		case CharacterType.Doge:
			cs.SetBodyColor(0, cvi.to[0]);
			break;
		case CharacterType.Cat:
			cs.SetBodyColor(0, cvi.to[0]);
			cs.SetBodyColor(1, cvi.to[1]);
			break;
		}
	}

	private void OnSecondarySkinColorChanged(OptionEnumarator options)
	{
		OnChange();
		SetSecondarySkinColor(options.Current.Value as AnimationLayerVariation.ColorVariationInfo);
		TraitChanged(options);
	}
	private void SetSecondarySkinColor(AnimationLayerVariation.ColorVariationInfo cvi)
	{
		switch (characterType)
		{
		case CharacterType.Ape:
		case CharacterType.Doge:
			cs.SetBodyColor(1, cvi.to[1]);
			break;
		case CharacterType.Cat:
			cs.SetBodyColor(2, cvi.to[2]);
			break;
		}
	}

	private void OnEyeColorChanged(OptionEnumarator options)
	{
		OnChange();
		SetEyeColor(options.Current.Value as AnimationLayerVariation.ColorVariationInfo);
		TraitChanged(options);
	}

	private void SetEyeColor(AnimationLayerVariation.ColorVariationInfo cvi)
	{
		cs.SetEyeColor(0, cvi.to[0]);
	}

	private void OnSecondaryEyeColorChanged(OptionEnumarator options)
	{
		OnChange();
		SetSecondaryEyeColor(options.Current.Value as AnimationLayerVariation.ColorVariationInfo);
		TraitChanged(options);
	}

	private static (AnimationLayerVariation, AnimationLayerVariation) GetHeadAndEyesVariations(CharacterType characterType)
	{
		string headName = $"b_{characterType.ToString().ToLower()}_head";
		string eyesName = $"b_{characterType.ToString().ToLower()}_eyes";
		return (AnimationManager.I.variations[headName], AnimationManager.I.variations[eyesName]);
	}

	private void SetSecondaryEyeColor(AnimationLayerVariation.ColorVariationInfo cvi)
	{
		cs.SetEyeColor(1, cvi.to[1]);
	}

	public Dictionary<string, Trait> ValidateTraits(int[] traits)
	{
		Dictionary<string, Trait> traitDict = new Dictionary<string, Trait>();
		for (int i = 0; i < traitKeys.Length; i++)
		{
			traitDict.Add(traitKeys[i], traits[i] != TraitInfo.EmptyTrait.id ? TraitInfo.traits[traits[i]] : TraitInfo.EmptyTrait);
		}
		return traitDict;
	}

	public CharacterType GetCharacterTypeFromTraits(int[] traits)
	{
		return AnimationManager.GetCharacterType(subcategoryOptionKeys[Names.Tribe][traits[0] - 1]);
	}

	public void SetFromTraitsForRasterization(Dictionary<string, Trait> traits)
	{
		SetFromTraits(traits.ToDictionary(e => e.Key, e => e.Value.id), true);
	}

	private void SetFromTraits(Dictionary<string, int> traits, bool forRasterization = false)
	{
		if (!forRasterization && spriteRasterizer.Busy)
		{
			Debug.LogError("Tried setting traits while rasterization jobs in progress!");
			return;
		}
		const int EMPTY_TRAIT = 0;

		foreach (var kv in traits)
		{
			CustomizationManager.traits[kv.Key] = kv.Value != EMPTY_TRAIT ? TraitInfo.traits[kv.Value] : TraitInfo.EmptyTrait;
		}

		characterType = AnimationManager.GetCharacterType(subcategoryOptionKeys[Names.Tribe][traits[Names.Tribe] - 1]);
		(AnimationLayerVariation headLayer, _) = InitializeCharacter();
		CharacterTypeChanged(false);
		(_, AnimationLayerVariation eyeLayer) = GetHeadAndEyesVariations(CharacterType.Ape);

		string skinColorName = TraitInfo.traits[traits[Names.SkinColor]].name.Replace($" {AnimationManager.GetCharacterString(characterType)}", "");
		SetSkinColor(GetColorVariationFromName(headLayer, 0, skinColorName));
		if (characterType != CharacterType.Human && characterType != CharacterType.Frog && characterType != CharacterType.Alien && traits[Names.FurColor] != EMPTY_TRAIT)
		{
			string furColorName = TraitInfo.traits[traits[Names.FurColor]].name.Replace($" {AnimationManager.GetCharacterString(characterType)} Fur", "");
			SetSecondarySkinColor(GetColorVariationFromName(headLayer, characterType == CharacterType.Cat ? 2 : 1, furColorName));
		}

		string eyeColorName = TraitInfo.traits[traits[Names.EyeColor]].name.Replace(" Eyes", "");
		SetEyeColor(GetColorVariationFromName(eyeLayer, 0, eyeColorName));
		if (characterType != CharacterType.Alien && characterType != CharacterType.Doge && traits[Names.PupilColor] != EMPTY_TRAIT)
		{
			string pupilColorName = TraitInfo.traits[traits[Names.PupilColor]].name.Replace(" Pupils", "");
			SetSecondaryEyeColor(GetColorVariationFromName(eyeLayer, 1, pupilColorName));
		}

		foreach (var kv in traits)
		{
			if (accessoryTypeMap.ContainsKey(kv.Key))
			{
				var layer = AnimationManager.GetLayer(accessoryTypeMap[kv.Key], characterType);
				if (layer)
				{
					animationLayerSubcategoryMap[layer] = kv.Key;
					var variations = layer.GetCharacterVariations(characterType);
					SetLayerVariation(layer, variations, TraitInfo.traits[kv.Value]);
				}
			}
			CustomizationManager.traits[kv.Key] = kv.Value > 0 ? TraitInfo.traits[kv.Value] : TraitInfo.EmptyTrait;
		}
	}

	private AnimationLayerVariation.ColorVariationInfo GetColorVariationFromName(AnimationLayerVariation alv, int index, string colorName)
	{
		Color32 c = PaletteInfo.nameMap[colorName].color;
		return alv.colorVariations.FirstOrDefault(cv => c.SameColorAs(cv.to[index]));
	}

	public void SetFromTraitsString(string traitsStr)
	{
		Dictionary<string, int> traits = new Dictionary<string, int>();
		traitsStr = Regex.Replace(traitsStr, @"[^a-zA-z0-9,\]\[ ]", "").Replace("[", "").Replace("],", ":");
		foreach (string trait in traitsStr.Split(':'))
		{
			var tokens = trait.Replace("]", "").Split(',');
			int traitId = int.Parse(tokens[1].Trim());
			traits.Add(tokens[0].Trim(), traitId);
		}
		SetFromTraits(traits);
	}

	public static bool IsRemovedTrait(Trait trait)
	{
		return removedTraits != null && removedTraits.Contains(trait);
	}

	public static bool IsRemovedTrait(int traitId)
	{
		return IsRemovedTrait(TraitInfo.traits[traitId]);
	}

	public static bool TraitCategorySelected(string category)
	{
		return traits[category].id > 0;
	}

	public void UI_SubmitTraitMap()
	{
		string traitStr = GetTraitsString(traits);
		SetFromTraitsString(traitStr);
		if (onSubmitTraits != null)
		{
			onSubmitTraits(traitStr);
		}
	}

	private string GetTraitsString(Dictionary<string, Trait> traits)
	{
		string res = "[";
		foreach (string key in traitKeys)
		{
			res += $"\n  [\"{key}\", {traits[key].id}],";
		}
		res = res.Substring(0, res.Length - 1);
		res += "\n]";
		return res;
	}

	[Button]
	private void GenerateSprites()
	{
		spriteRasterizer.RasterizeAllFrames(traits, Utils.GetMD5Hash((XUtils.Timestamp() / 100).ToString()), OnRasterizationComplete);
	}


	[Button]
	internal Dictionary<string, Trait> GetRandomTraits()
	{
		Dictionary<string, Trait> randomTraits = new Dictionary<string, Trait>();
		string characterString = XRandom.NextMember(subcategoryOptionKeys[Names.Tribe]);
		CharacterType characterType = AnimationManager.GetCharacterType(characterString);
		randomTraits[Names.Tribe] = TraitInfo.GetTrait(characterType, characterString);
		foreach (string key in traitKeys)
		{
			switch (key)
			{
			case Names.Tribe:
				continue;
			case Names.SkinColor:
			case Names.FurColor:
			case Names.EyeColor:
			case Names.PupilColor:
				randomTraits.Add(key, TraitInfo.GetRandomTrait(characterType, key, removedTraits));
				break;
			default:
				if (XRandom.NextFloat() > randomizationProbabiity)
				{
					randomTraits.Add(key, TraitInfo.GetRandomTrait(characterType, accessoryTypeMap[key], removedTraits));
				}
				else
				{
					randomTraits.Add(key, TraitInfo.EmptyTrait);
				}
				break;
			}
		}
		return randomTraits;
	}


	public void GetRandomSprites(Action<List<Sprite>> onSpritesReady)
	{
		spriteRasterizer.RasterizeAllFrames(GetRandomTraits(), Utils.GetMD5Hash((XUtils.Timestamp() / 1).ToString()), (sprites, hash) => { onSpritesReady(sprites); });
	}

	public static void RandomizeTraits()
	{
		var traits = I.GetRandomTraits().ToDictionary(e => e.Key, e => e.Value.id);
		I.SetFromTraits(traits);
		I.CharacterTypeChanged(false);
		I.ReselectOptions();
		I.CleaupUnusedFrames();
	}

#if UNITY_EDITOR
	[Button]
	private void RunTraitRemovalSimulation()
	{
		StartCoroutine(DoRunSim());
	}

	private IEnumerator DoRunSim()
	{
		var poolSizes = new Dictionary<string, int>();
		poolSizes.Add(Names.SkinColor, 59);
		poolSizes.Add(Names.FurColor, 30);
		poolSizes.Add(Names.EyeColor, 9);
		poolSizes.Add(Names.PupilColor, 9);
		foreach (var trait in traitKeys)
		{
			if (accessoryTypeMap.ContainsKey(trait))
			{
				AccessoryType t = accessoryTypeMap[trait];
				poolSizes.Add(trait, TraitInfo.accessoryTraits[t].Count);
			}
		}


		removedTraits = new HashSet<Trait>();
		var removedPerCategory = new Dictionary<string, int>();
		string output = "Minted,Total";
		foreach (var trait in traitKeys)
		{
			if (trait != Names.Tribe)
			{
				removedPerCategory.Add(trait, 0);
				output += $",{trait}";
			}
		}
		output += "\n";
		string traitsOutput = "";
		int count = 0;
		while (count++ < 10000)
		{
			var traits = GetRandomTraits();
			traitsOutput += $"{string.Join(",", traits.Select(e => e.Value.id))},\n";
			if (count % 100 == 0)
			{
				output += $"{count},{removedTraits.Count}";
				foreach (var kv in removedPerCategory)
				{
					output += $",{poolSizes[kv.Key] - kv.Value}";
				}
				output += "\n";
			}
			RemoveTrait(count, traits, removedPerCategory, poolSizes);
			if (count % 10 == 0)
			{
				print(count);
				yield return new WaitForEndOfFrame();
			}
		}
		using (StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, $"T4/Data/{XUtils.Timestamp()}.csv"), false))
		{
			writer.Write(output);
		}
		using (StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, $"T4/Data/minted-{XUtils.Timestamp()}.json"), false))
		{
			writer.Write(traitsOutput);
		}
		output = "";
		foreach (var trait in removedTraits)
		{
			output += $"{trait.id},";
		}
		print(output);

		void RemoveTrait(int newCharId, Dictionary<string, Trait> traits, Dictionary<string, int> categories, Dictionary<string, int> originalPoolSizes)
		{
			int numRemoved = removedTraits.Count;
			if ((numRemoved < 100 && newCharId % 7 == 0) ||
			(numRemoved >= 100 && numRemoved < 200 && newCharId % 9 == 0) ||
			(numRemoved >= 200 && numRemoved < 300 && newCharId % 11 == 0) ||
			(numRemoved >= 300 && numRemoved < 400 && newCharId % 13 == 0))
			{
				var randomTraitToRemove = traits.Keys.ToArray()[XRandom.NextInt(5, 22)];
				var trait = traits[randomTraitToRemove];
				if (trait.id == 0)
				{
					return;
				}

				int poolSize = originalPoolSizes[randomTraitToRemove];
				bool doRemove = XRandom.NextInt(10, 70) > (500 / poolSize);

				if (doRemove)
				{
					if (removedTraits.Contains(trait))
					{
						Debug.LogError($"Trait {trait.id} already removed! What's up?");
					}
					removedTraits.Add(trait);
					categories[randomTraitToRemove]++;
				}
			}
		}
	}
#endif


	public static class Names
	{
		public const string Character = "Character";
		public const string Head = "Head";
		public const string Clothing = "Clothing";
		public const string Accessories = "Accessories";
		public const string Items = "Items";
		public const string Tribe = "Tribe";
		public const string SkinColor = "Skin Color";
		public const string FurColor = "Fur Color";
		public const string EyeColor = "Eye Color";
		public const string PupilColor = "Pupil Color";
		public const string Hair = "Hair";
		public const string Mouth = "Mouth";
		public const string Beard = "Beard";
		public const string Top = "Top";
		public const string Outerwear = "Outerwear";
		public const string Print = "Print";
		public const string Bottom = "Bottom";
		public const string Footwear = "Footwear";
		public const string Belt = "Belt";
		public const string Hat = "Hat";
		public const string Eyewear = "Eyewear";
		public const string Piercing = "Piercing";
		public const string Wrist = "Wrist";
		public const string Hands = "Hands";
		public const string Neckwear = "Neckwear";
		public const string LeftItem = "Left Item";
		public const string RightItem = "Right Item";
	}
}
