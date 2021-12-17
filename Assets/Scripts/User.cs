
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class NiftyUsers
{
	public static NiftyUser me { get; private set; }

	private static Dictionary<string, NiftyBro> broCache = new Dictionary<string, NiftyBro>();


	public static NiftyUser Login(string address)
	{
		me = new NiftyUser(address);
		return me;
	}

	public static NiftyBro GetNiftyBro(int[] traitsInt)
	{
		return GetNiftyBro(string.Join(".", traitsInt));
	}

	public static NiftyBro GetNiftyBro(string traitStr)
	{
		if (broCache.ContainsKey(traitStr))
		{
			return broCache[traitStr];
		}
		var bro = new NiftyBro(traitStr);
		return bro;
	}
}

[System.Serializable]
public class NiftyUser
{
	public string id { get; }
	public List<NiftyBro> bros;
	public string Address { get { return $"0x{id}"; } }

	public NiftyUser(string address)
	{
		id = address.Replace("0x", "").ToLower();
		bros = new List<NiftyBro>();
	}

	public void SetBros(List<int[]> characterTraits)
	{
		characterTraits.RemoveAll(traits => traits.Length != CustomizationManager.NumTraitsKeys);
		bros = new List<NiftyBro>();
		foreach (var traits in characterTraits)
		{
			bros.Add(NiftyUsers.GetNiftyBro(traits));
		}
	}

	public void RasterizeBros()
	{
		foreach (NiftyBro bro in bros)
		{
			bro.Rasterize();
		}
	}
}


[System.Serializable]
public class NiftyBro
{
	public Dictionary<string, Trait> traits { get; }
	public Color32 primaryColor { get; }

	public List<Sprite> sprites;
	public string hash { get; private set; }
	public CharacterType type { get; }
	public string traitsStr { get; }

	public NiftyBro(int[] traitInts)
	{
		traits = CustomizationManager.I.ValidateTraits(traitInts);
		type = CustomizationManager.I.GetCharacterTypeFromTraits(traitInts);
		string skinColor = traits[CustomizationManager.Names.SkinColor].name;
		primaryColor = PaletteInfo.nameMap[skinColor.Replace($" {traits[CustomizationManager.Names.Tribe].name}", "")].color;
		traitsStr = string.Join(".", traitInts);
	}

	public NiftyBro(string traitsStr) : this(traitsStr.Split('.').Select(s => int.Parse(s)).ToArray()) { }

	public void Rasterize(Action<NiftyBro> onRasterizationComplete = null)
	{
		if (sprites == null || sprites.Count != AnimationTags.totalFrameCount)
		{
			CustomizationManager.GenerateSprites(traits, (sprites, hash) =>
			{
				this.sprites = sprites;
				this.hash = hash;
				if (onRasterizationComplete != null)
				{
					onRasterizationComplete(this);
				}
			});
		}
	}
}