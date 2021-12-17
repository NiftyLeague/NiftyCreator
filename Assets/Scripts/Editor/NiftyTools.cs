#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;


public class NiftyTools : MonoBehaviour
{
	private static string IMPORT_PATH_RELATIVE = "Assets/Sprites/Characters";
	private static string CHARACTER_IMPORT_PATH = Path.Combine(Application.dataPath, "Sprites/Characters");
	private static string LAYER_DATA_PATH = "Assets/Prefabs/LayerData";
	private static string ASSETS_PATH = Application.dataPath;
	private static int ABS_PATH_LEN = Path.GetDirectoryName(Application.dataPath).Length + 1;
	private static string ANIMATION_MANAGER_PATH = "Assets/Prefabs/AnimationManager.prefab";

	private static string PREFABS_PATH_RELATIVE = "Assets/Prefabs";
	private static string PREFABS_PATH = Path.Combine(Application.dataPath, "Prefabs");
	private static string ANIMATION_VARIATIONS_FOLDER = "AnimationVariations";
	private static string ANIMATION_LAYERS_FOLDER = "AnimationLayers";
	private static string LAYER_DATA_FOLDER = "LayerData";
	private const int TRAIT_ID_COLOR_OFFSET = 10;
	private const int TRAIT_ID_OFFSET = 150;
	private const int TRAIT_ID_UNSET = -1;
	private static string TRAITS_EXPORT_PATH = Path.Combine(Application.dataPath, "T4/Data");
	private static string TRAIT_RANGES_PATH = Path.GetFullPath(Path.Combine(TRAITS_EXPORT_PATH, "TraitRanges.json"));
	private static string TRAITS_PATH = Path.GetFullPath(Path.Combine(TRAITS_EXPORT_PATH, "Traits.json"));

	private static Dictionary<string, AnimationLayerVariation> variations = new Dictionary<string, AnimationLayerVariation>();
	private static int traitId = TRAIT_ID_OFFSET;
	private static int colorTraitId = TRAIT_ID_COLOR_OFFSET;
	private static CharacterType[] characterTypes = new CharacterType[] { CharacterType.Share, CharacterType.Ape, CharacterType.Human, CharacterType.Doge, CharacterType.Frog, CharacterType.Cat, CharacterType.Alien };
	private static AccessoryType[] accessoryTypes = new AccessoryType[] {
		AccessoryType.Hair, AccessoryType.Mouth, AccessoryType.Beard, AccessoryType.Top, AccessoryType.Outerwear, AccessoryType.Print, AccessoryType.Bottom, AccessoryType.Footwear, AccessoryType.Belt,
		AccessoryType.Hat, AccessoryType.Eyewear, AccessoryType.Piercing, AccessoryType.Wrist, AccessoryType.Hands, AccessoryType.Neckwear, AccessoryType.LeftItem, AccessoryType.RightItem,
	};

	private static Dictionary<AnimationLayerVariation, int> generatedTraits = new Dictionary<AnimationLayerVariation, int>();


	[MenuItem("Tools/Nifty/Import and Generate Everything")]
	private static void ImportLayers()
	{
		if (!Directory.Exists(CHARACTER_IMPORT_PATH))
		{
			Debug.LogError(CHARACTER_IMPORT_PATH + " does not exist.");
			return;
		}
		variations.Clear();
		var animationManagers = FindObjectsOfType<AnimationManager>();
		foreach (var cm in animationManagers)
		{
			DestroyImmediate(cm.gameObject);
		}
		AnimationManager animationManager = AssetDatabase.LoadAssetAtPath<AnimationManager>(ANIMATION_MANAGER_PATH);
		animationManager = Instantiate(animationManager, Vector3.zero, Quaternion.identity);
		animationManager.gameObject.name = animationManager.gameObject.name.Replace("(Clone)", "");

		int numSpritesImported = 0;
		Dictionary<int, AnimationLayer> animationLayers = new Dictionary<int, AnimationLayer>();
		string[] directoryEntries = AssetDatabase.GetSubFolders(IMPORT_PATH_RELATIVE);
		Dictionary<int, List<AnimationLayerVariation>> layerVariations = new Dictionary<int, List<AnimationLayerVariation>>();
		foreach (string dir in directoryEntries)
		{
			string dirName = Path.GetFileName(dir);
			string[] layerNameTokens = dirName.Split('_');
			int layerNum = int.Parse(layerNameTokens[0]);

			if (!animationLayers.ContainsKey(layerNum))
			{
				GameObject newLayer = Utils.CreateGameObject(null, layerNameTokens[0] + '_' + layerNameTokens[1]);
				AnimationLayer cl = newLayer.AddComponent<AnimationLayer>();
				cl.layer = layerNum;
				cl.layerName = newLayer.name;
				cl.customizationCategory = Accessories.GetAccessoryType(layerNum);
				GameObject sceneLayer = newLayer;
				newLayer = PrefabUtility.SaveAsPrefabAssetAndConnect(newLayer,
					Path.Combine(PREFABS_PATH_RELATIVE, ANIMATION_LAYERS_FOLDER, $"{newLayer.name}.prefab"), InteractionMode.AutomatedAction);
				animationLayers.Add(layerNum, newLayer.GetComponent<AnimationLayer>());
				layerVariations.Add(layerNum, new List<AnimationLayerVariation>());
				DestroyImmediate(sceneLayer);
			}

			AnimationLayer layer = animationLayers[layerNum];
			string optionName = dirName.Substring(dirName.IndexOf('_') + 1);
			// layer.required = optionName.StartsWith("b_") || optionName.StartsWith("fx_") || optionName.StartsWith("bat_");
			layer.layerType = AnimationManager.GetLayerType(layerNameTokens[1]);
			layer.characterType = AnimationManager.GetCharacterType(layerNameTokens[2]);

			GameObject variationGo = Utils.CreateGameObject(null, optionName);
			AnimationLayerVariation variation = variationGo.AddComponent<AnimationLayerVariation>();
			variation.variationName = variationGo.name;
			variation.animationLayer = layer;
			variation.characterType = AnimationManager.GetCharacterType(dirName.Split('_')[2]);

			LayerData layerData = ScriptableObject.CreateInstance<LayerData>();
			layerData.frames = new Sprite[AnimationTags.totalFrameCount];
			layerData.layer = variation.variationName;

			foreach (var f in Directory.GetFiles(Path.Combine(CHARACTER_IMPORT_PATH, Path.GetFileName(dir)), "*.png"))
			{
				bool isOriginalFrame = f.ToLower().EndsWith("_0000.png");
				if (isOriginalFrame)
				{
					AssetDatabase.ImportAsset(f.Substring(ABS_PATH_LEN));
				}
				string path = "Assets" + f.Substring(ASSETS_PATH.Length).Replace("\\", "/");
				//path = "Assets" + path.Substring(0, path.LastIndexOf('.'));
				Sprite frameSprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
				int frameNum = int.Parse(frameSprite.name.Substring(frameSprite.name.Length - 4));
				layerData.frames[frameNum] = frameSprite;
				numSpritesImported++;
				if (isOriginalFrame)
				{
					variation.originalFrame = frameSprite;
				}
			}

			AssetDatabase.CreateAsset(layerData, Path.Combine(LAYER_DATA_PATH, $"{variation.variationName}.asset"));

			variation.Initialize(layerData);
			GameObject sceneVariation = variationGo;
			string prefabPath = Path.Combine(PREFABS_PATH_RELATIVE, ANIMATION_VARIATIONS_FOLDER, $"{variation.variationName}.prefab");
			variationGo = PrefabUtility.SaveAsPrefabAssetAndConnect(variationGo, prefabPath, InteractionMode.AutomatedAction);

			layerVariations[layerNum].Add(variationGo.GetComponent<AnimationLayerVariation>());
			variations.Add(variation.variationName, variationGo.GetComponent<AnimationLayerVariation>());
			DestroyImmediate(sceneVariation);
			if (Input.GetKey(KeyCode.LeftControl))
			{
				return;
			}
		}

		foreach (var kv in animationLayers)
		{
			AnimationLayer layer = (AnimationLayer)PrefabUtility.InstantiatePrefab(kv.Value);
			layer.variations = layerVariations[kv.Key].ToArray();
			PrefabUtility.ApplyPrefabInstance(layer.gameObject, InteractionMode.AutomatedAction);
			DestroyImmediate(layer.gameObject);
		}

		animationManager.layers = (new List<AnimationLayer>(animationLayers.Values)).ToArray();
		GameObject savedPrefab = PrefabUtility.SaveAsPrefabAssetAndConnect(animationManager.gameObject, ANIMATION_MANAGER_PATH, InteractionMode.AutomatedAction);
		Debug.Log($"{ANIMATION_MANAGER_PATH} prefab updated`");
		Debug.Log($"{numSpritesImported} sprited on {animationLayers.Count} layers imported successfully");
		EditorGUIUtility.PingObject(savedPrefab);

		GenerateTraitIds();
	}

	[MenuItem("Tools/Nifty/Generate Trait IDs")]
	private static void GenerateTraitIds()
	{
		if (variations == null || variations.Count == 0)
		{
			variations = GetAllVariations();
		}

		generatedTraits = new Dictionary<AnimationLayerVariation, int>();

		Dictionary<AccessoryType, List<AnimationLayerVariation>> accessoryMap = new Dictionary<AccessoryType, List<AnimationLayerVariation>>();
		Dictionary<string, List<AnimationLayerVariation>> accessoryNameMap = new Dictionary<string, List<AnimationLayerVariation>>();

		foreach (AccessoryType t in accessoryTypes)
		{
			accessoryMap.Add(t, new List<AnimationLayerVariation>());
		}

		foreach (var alv in variations.Values)
		{
			generatedTraits.Add(alv, TRAIT_ID_UNSET);
			AccessoryType type = alv.accessoryInfo != null ? alv.accessoryInfo.accessoryType : AccessoryType.None;
			if (accessoryMap.ContainsKey(type))
			{
				accessoryMap[type].Add(alv);
				string name = alv.accessoryInfo.displayName;
				if (!accessoryNameMap.ContainsKey(name))
				{
					accessoryNameMap.Add(name, new List<AnimationLayerVariation>());
				}
				accessoryNameMap[name].Add(alv);
			}
		}

		traitId = TRAIT_ID_OFFSET;
		foreach (AccessoryType t in accessoryTypes)
		{
			SetTraitIdsForAccessoryType(t, accessoryMap[t], accessoryNameMap);
		}

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		ExportTraitIds();
		print("Last trait ID: " + traitId);
	}

	[MenuItem("Tools/Nifty/Export Trait IDs")]
	private static void ExportTraitIds()
	{
		colorTraitId = TRAIT_ID_COLOR_OFFSET;
		if (variations == null || variations.Count == 0)
		{
			variations = GetAllVariations();
		}

		Dictionary<AccessoryType, List<KeyValuePair<int, string>>> accessoryTraits = new Dictionary<AccessoryType, List<KeyValuePair<int, string>>>();
		Dictionary<CharacterType, List<KeyValuePair<int, string>>> characterTraits = new Dictionary<CharacterType, List<KeyValuePair<int, string>>>();
		Dictionary<CharacterType, List<KeyValuePair<int, string>>> colorTraits = new Dictionary<CharacterType, List<KeyValuePair<int, string>>>();
		Dictionary<CharacterType, List<KeyValuePair<int, string>>> secondaryColorTraits = new Dictionary<CharacterType, List<KeyValuePair<int, string>>>();

		List<KeyValuePair<int, string>> eyeTraits = new List<KeyValuePair<int, string>>();
		List<KeyValuePair<int, string>> secondaryEyeTraits = new List<KeyValuePair<int, string>>();

		for (int i = 1; i < characterTypes.Length; i++)
		{
			AddCharacterTraitToMap(characterTraits, i, characterTypes[i].ToString(), characterTypes[i]);
			AddColorTraitsToMap(colorTraits, characterTraits, characterTypes[i]);
		}

		for (int i = 1; i < characterTypes.Length; i++)
		{
			AddSecondaryColorTraitsToMap(secondaryColorTraits, characterTraits, characterTypes[i]);
		}


		AddEyeTraitsToMap(eyeTraits, secondaryEyeTraits, characterTraits);

		foreach (var e in variations.OrderBy(e => e.Value.traitId))
		{
			AnimationLayerVariation v = e.Value;
			if (v.traitId > 0)
			{
				if (v.colorVariations.Length <= 1)
				{
					AddCharacterTraitToMap(characterTraits, v.traitId, v.accessoryInfo.displayName, v.characterType);
					AddAccessoryTraitToMap(accessoryTraits, v.traitId, v.accessoryInfo.displayName, v.accessoryInfo.accessoryType);
				}
				else
				{
					for (int i = 0; i < v.colorVariations.Length; i++)
					{
						string name = $"{v.accessoryInfo.displayName} {i + 1:00}";
						int id = v.traitId + i;
						AddCharacterTraitToMap(characterTraits, id, name, v.characterType);
						AddAccessoryTraitToMap(accessoryTraits, id, name, v.accessoryInfo.accessoryType);
					}
				}
			}
		}
		string res = $"// v{Accessories.version}\n{{";
		for (int i = 1; i < characterTypes.Length; i++)
		{
			string s = $"\"{characterTypes[i]}\": [";
			foreach (var trait in characterTraits[characterTypes[i]].Distinct())
			{
				s += trait.Key + ",";
			}
			res += "\n  " + s.Substring(0, s.Length - 1) + "],";
		}

		foreach (AccessoryType at in accessoryTypes)
		{
			string s = $"\"{at}\": [";
			if (accessoryTraits.ContainsKey(at))
			{
				foreach (var trait in accessoryTraits[at].Distinct())
				{
					s += trait.Key + ",";
				}
			}
			res += "\n  " + s.Substring(0, s.Length - 1) + "],";
		}
		res = res.Substring(0, res.Length - 1) + "\n}\n";
		using (StreamWriter writer = new StreamWriter(TRAIT_RANGES_PATH, false))
		{
			writer.Write(res);
		}
		print("Trait Ranges exported to " + TRAIT_RANGES_PATH);


		res = $"// v{Accessories.version}\n{{";
		res += "\n  // Tribes";
		for (int i = 1; i < characterTypes.Length; i++)
		{
			res += $"\n  {i}: \"{characterTypes[i]}\",";
		}

		res += "\n  // Skin Colors";
		for (int i = 1; i < characterTypes.Length; i++)
		{
			res += "\n  // " + characterTypes[i];
			foreach (var kv in colorTraits[characterTypes[i]])
			{
				res += $"\n  {kv.Key}: \"{kv.Value}\",";
			}
		}

		res += "\n  // Secondary Skin Colors";
		for (int i = 1; i < characterTypes.Length; i++)
		{
			res += "\n  // " + characterTypes[i];
			foreach (var kv in secondaryColorTraits[characterTypes[i]])
			{
				res += $"\n  {kv.Key}: \"{kv.Value}\",";
			}
		}

		res += "\n  // Eye Colors";
		foreach (var kv in eyeTraits)
		{
			res += $"\n  {kv.Key}: \"{kv.Value}\",";
		}


		res += "\n  // Secondary Eye Colors";
		foreach (var kv in secondaryEyeTraits)
		{
			res += $"\n  {kv.Key}: \"{kv.Value}\",";
		}

		foreach (AccessoryType at in accessoryTypes)
		{
			string s = $"\n  // {at}";
			if (accessoryTraits.ContainsKey(at))
			{
				foreach (var trait in accessoryTraits[at].Distinct())
				{
					s += $"\n  {trait.Key}: \"{trait.Value}\",";
				}
			}
			res += s;
		}

		res = res.Substring(0, res.Length - 1) + "\n}\n";
		using (StreamWriter writer = new StreamWriter(TRAITS_PATH, false))
		{
			writer.Write(res);
		}
		print("Traits exported to " + TRAITS_PATH);
		Debug.LogWarning("Don't forget to re-generate the TraitInfo template");
	}

	private static void AddEyeTraitsToMap(List<KeyValuePair<int, string>> eyeTraits, List<KeyValuePair<int, string>> secondaryEyeTraits, Dictionary<CharacterType, List<KeyValuePair<int, string>>> characterTraits)
	{
		(var headLayer, var eyesLayer) = GetHeadAndEyesVariations(CharacterType.Ape);
		foreach (var cv in eyesLayer.colorVariations)
		{
			string name = $"{PaletteInfo.colorMap[cv.to[0]].name} Eyes";
			eyeTraits.Add(new KeyValuePair<int, string>(colorTraitId, name));
			for (int i = 1; i < characterTypes.Length; i++)
			{
				characterTraits[characterTypes[i]].Add(new KeyValuePair<int, string>(colorTraitId, name));
			}
			colorTraitId++;
		}

		foreach (var cv in eyesLayer.colorVariations)
		{
			string name = $"{PaletteInfo.colorMap[cv.to[cv.to.Length - 1]].name} Pupils";
			secondaryEyeTraits.Add(new KeyValuePair<int, string>(colorTraitId, name));
			for (int i = 1; i < characterTypes.Length; i++)
			{
				characterTraits[characterTypes[i]].Add(new KeyValuePair<int, string>(colorTraitId, name));
			}
			colorTraitId++;
		}
	}

	private static void AddColorTraitsToMap(Dictionary<CharacterType, List<KeyValuePair<int, string>>> colorTraits, Dictionary<CharacterType, List<KeyValuePair<int, string>>> characterTraits, CharacterType characterType)
	{
		colorTraits.Add(characterType, new List<KeyValuePair<int, string>>());
		(var headLayer, var eyesLayer) = GetHeadAndEyesVariations(characterType);
		foreach (var cv in headLayer.colorVariations)
		{
			string name = $"{PaletteInfo.colorMap[cv.to[0]].name} {characterType}";
			colorTraits[characterType].Add(new KeyValuePair<int, string>(colorTraitId, name));
			characterTraits[characterType].Add(new KeyValuePair<int, string>(colorTraitId, name));
			colorTraitId++;
		}
	}

	private static void AddSecondaryColorTraitsToMap(Dictionary<CharacterType, List<KeyValuePair<int, string>>> secondaryColorTraits, Dictionary<CharacterType, List<KeyValuePair<int, string>>> characterTraits, CharacterType characterType)
	{
		secondaryColorTraits.Add(characterType, new List<KeyValuePair<int, string>>());
		if (characterType == CharacterType.Alien || characterType == CharacterType.Frog || characterType == CharacterType.Human)
		{
			return;
		}
		(var headLayer, var eyesLayer) = GetHeadAndEyesVariations(characterType);
		foreach (var cv in headLayer.colorVariations)
		{
			string name = $"{PaletteInfo.colorMap[cv.to[cv.to.Length - 1]].name} {characterType} Fur";
			secondaryColorTraits[characterType].Add(new KeyValuePair<int, string>(colorTraitId, name));
			characterTraits[characterType].Add(new KeyValuePair<int, string>(colorTraitId, name));
			colorTraitId++;
		}
	}

	private static void AddCharacterTraitToMap(Dictionary<CharacterType, List<KeyValuePair<int, string>>> characterTraits, int traitId, string traitName, CharacterType characterType)
	{
		if (characterType == CharacterType.Share)
		{
			for (int i = 1; i < characterTypes.Length; i++)
			{
				AddCharacterTraitToMap(characterTraits, traitId, traitName, characterTypes[i]);
			}
		}
		else
		{
			if (!characterTraits.ContainsKey(characterType))
			{
				characterTraits.Add(characterType, new List<KeyValuePair<int, string>>());
			}
			characterTraits[characterType].Add(new KeyValuePair<int, string>(traitId, traitName));
		}
	}

	private static void AddAccessoryTraitToMap(Dictionary<AccessoryType, List<KeyValuePair<int, string>>> accessoryTraits, int traitId, string traitName, AccessoryType type)
	{
		if (!accessoryTraits.ContainsKey(type))
		{
			accessoryTraits.Add(type, new List<KeyValuePair<int, string>>());
		}
		accessoryTraits[type].Add(new KeyValuePair<int, string>(traitId, Utils.ToFirstLetterUppercase(traitName)));
	}

	private static void SetTraitIdsForAccessoryType(AccessoryType type, List<AnimationLayerVariation> variations, Dictionary<string, List<AnimationLayerVariation>> accessoryNameMap)
	{
		variations.OrderBy(v => accessoryNameMap[v.accessoryInfo.displayName].Count);
		foreach (var v in variations)
		{
			if (generatedTraits[v] != TRAIT_ID_UNSET)
			{
				continue;
			}

			List<AnimationLayerVariation> perTribeAccessoryList = accessoryNameMap[v.accessoryInfo.displayName];
			if (perTribeAccessoryList.Count > 1)
			{
				int colorVariationCount = v.colorVariations.Length;
				if (perTribeAccessoryList.Count != characterTypes.Length - 1)
				{
					Debug.LogWarning($"A shared unique-per-tribe accessory found '{v.accessoryInfo.displayName}' but accessory is not available for all character types ({perTribeAccessoryList.Count} found)");
				}

				var maxColorVars = perTribeAccessoryList.Aggregate((v1, v2) => v1.colorVariations.Length > v2.colorVariations.Length ? v1 : v2);
				foreach (var variation in perTribeAccessoryList)
				{
					if (maxColorVars.colorVariations.Length != variation.colorVariations.Length)
					{
						Debug.LogWarning($"A shared unique-per-tribe accessory found '({variation.variationName}) {variation.accessoryInfo.displayName}' but color variation count does not match. Defaulting to '{maxColorVars.variationName}' colors");
						variation.colorVariations = maxColorVars.colorVariations;
						variation.originalColors = maxColorVars.originalColors;
						SaveVariation(variation);
					}
					SetTraitId(variation);
				}
				traitId += Mathf.Max(1, maxColorVars.colorVariations.Length);
			}
		}

		foreach (CharacterType characterType in characterTypes)
		{
			var characterVariations = variations.Where(v => v.characterType == characterType);
			foreach (var v in characterVariations)
			{
				if (generatedTraits[v] != TRAIT_ID_UNSET)
				{
					continue;
				}

				bool success = SetTraitId(v);
				if (success)
				{
					traitId += Mathf.Max(1, v.colorVariations.Length);
				}
			}
		}
	}

	private static bool SetTraitId(AnimationLayerVariation variation)
	{
		if (generatedTraits[variation] == TRAIT_ID_UNSET)
		{
			generatedTraits[variation] = traitId;
			AnimationLayerVariation prefabInstance = (AnimationLayerVariation)PrefabUtility.InstantiatePrefab(variation);
			prefabInstance.traitId = traitId;
			PrefabUtility.ApplyPrefabInstance(prefabInstance.gameObject, InteractionMode.AutomatedAction);
			DestroyImmediate(prefabInstance.gameObject);
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(variation));
			return true;
		}
		return false;
	}


	private static void SaveVariation(AnimationLayerVariation variation)
	{
		AnimationLayerVariation prefabInstance = (AnimationLayerVariation)PrefabUtility.InstantiatePrefab(variation);
		PrefabUtility.ApplyPrefabInstance(prefabInstance.gameObject, InteractionMode.AutomatedAction);
		DestroyImmediate(prefabInstance.gameObject);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(variation));
	}


	private static Dictionary<string, AnimationLayerVariation> GetAllVariations()
	{
		AnimationManager animationManager = AssetDatabase.LoadAssetAtPath<AnimationManager>(ANIMATION_MANAGER_PATH);
		Dictionary<string, AnimationLayerVariation> variations = new Dictionary<string, AnimationLayerVariation>();
		foreach (AnimationLayer l in animationManager.layers)
		{
			foreach (AnimationLayerVariation lv in l.variations)
			{
				variations.Add(lv.variationName, lv);
			}
		}
		return variations;
	}

	private static (AnimationLayerVariation, AnimationLayerVariation) GetHeadAndEyesVariations(CharacterType characterType)
	{
		string headName = $"b_{characterType.ToString().ToLower()}_head";
		string eyesName = $"b_{characterType.ToString().ToLower()}_eyes";
		return (variations[headName], variations[eyesName]);
	}

	private static void RemoveAllPrefabs(string path)
	{
		foreach (var f in Directory.GetFiles(path))
		{
			File.Delete(f);
		}
	}


	[MenuItem("Tools/Nifty/Update Reference to All Sprites")]
	public static void UpdateSpriteReferences()
	{
		var animationManagers = FindObjectsOfType<AnimationManager>();
		foreach (var cm in animationManagers)
		{
			DestroyImmediate(cm.gameObject);
		}
		AnimationManager animationManager = AssetDatabase.LoadAssetAtPath<AnimationManager>(ANIMATION_MANAGER_PATH);
		animationManager = Instantiate(animationManager, Vector3.zero, Quaternion.identity);
		animationManager.gameObject.name = animationManager.gameObject.name.Replace("(Clone)", "");

		Dictionary<string, AnimationLayerVariation> variations = new Dictionary<string, AnimationLayerVariation>();
		animationManager.allSprites = new List<Sprite>();
		foreach (AnimationLayer l in animationManager.layers)
		{
			foreach (AnimationLayerVariation lv in l.variations)
			{
#if WEBGL_MOBILE_BUILD
				animationManager.allSprites.Add(lv.originalFrame);
#else
				animationManager.allSprites.AddRange(lv.layerData.frames.Where(f => f != null));
#endif
			}
		}

		PrefabUtility.SaveAsPrefabAssetAndConnect(animationManager.gameObject, ANIMATION_MANAGER_PATH, InteractionMode.AutomatedAction);
		print(animationManager.allSprites.Count + " sprites added to all sprites");
	}


	[MenuItem("Tools/Nifty/Update ALVs LayerData")]
	public static void UpdateAnimationVariationLayerData()
	{
		AnimationManager animationManager = AssetDatabase.LoadAssetAtPath<AnimationManager>(ANIMATION_MANAGER_PATH);
		foreach (AnimationLayer l in animationManager.layers)
		{
			foreach (AnimationLayerVariation variation in l.variations)
			{
#if WEBGL_MOBILE_BUILD
				variation.layerData = null;
#else
				string path = AssetDatabase.GetAssetPath(variation).Replace(ANIMATION_VARIATIONS_FOLDER, LAYER_DATA_FOLDER).Replace(".prefab", ".asset");
				variation.layerData = AssetDatabase.LoadAssetAtPath<LayerData>(path);
#endif
			}
		}
	}
}


class NiftyBuildProcessor : IPreprocessBuildWithReport
{
	private static string ANIMATION_MANAGER_PATH = "Assets/Prefabs/AnimationManager.prefab";

	public int callbackOrder { get { return 1; } }


	public void OnPreprocessBuild(BuildReport report)
	{
		Debug.Log("NiftyBuildProcessor");
		//AnimationManager animationManager = AssetDatabase.LoadAssetAtPath<AnimationManager>(ANIMATION_MANAGER_PATH);
		//foreach (var l in animationManager.layers)
		//{
		//	if (!l.Required)
		//	{
		//		foreach (var lv in l.variations)
		//		{
		//			// lv.ClearFrames();
		//		}
		//	}
		//}

		NiftyTools.UpdateAnimationVariationLayerData();
		NiftyTools.UpdateSpriteReferences();
	}
}
#endif //#if UNITY_EDITOR
