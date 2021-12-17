using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class AnimationLayerVariation : MonoBehaviour
{
	public int traitId;
	public string variationName;
	public CharacterType characterType;
	public AccessoryInfo accessoryInfo;
	public AnimationLayer animationLayer;
	public ColorVariationInfo[] colorVariations;
	[NaughtyAttributes.ReadOnly]
	public PaletteColor[] palleteColors;
	[NaughtyAttributes.ReadOnly]
	public Color32[] colors;
	public PaletteColor[] originalColors;

	[SerializeField]
	public LayerData layerData;
	public Sprite originalFrame;

	internal bool layerDataLoaded = false;

	private static Sprite[] emptyFrames = new Sprite[AnimationTags.totalFrameCount];
	private static bool isInitializing = false;


	private void LoadLayerData()
	{
		ClearColorPalettePixels();
		layerDataLoaded = true;
	}

	public int GetTraitId(int colorIndex)
	{
		if (colorIndex <= 0)
		{
			return traitId;
		}
		Assert.IsTrue(colorIndex < colorVariations.Length);
		return traitId + colorIndex;
	}

	public void ClearFrames()
	{
	}

	public void Initialize(LayerData layerData)
	{
		isInitializing = true;
#if UNITY_EDITOR && false
		layerDataAddress = layerData.GetAddressableAssetEntry().address;
#endif
		this.layerData = layerData;
		InitializeFrameColors();
		InitializeVariationColors();
		accessoryInfo = Accessories.GetAccessoryInfo(variationName);
		isInitializing = false;
	}

	private void InitializeVariationColors()
	{
		Sprite f = originalFrame;
		if (f == null)
		{
			return;
		}
		int w = f.texture.width;
		int h = f.texture.height;
		Color32[] pix = f.texture.GetPixels32();
		List<PaletteColor> fromColors = new List<PaletteColor>();

		int i = h - 1;
		int j = 0;

		while (pix[i * w + j].a > 0f)
		{
			fromColors.Add(PaletteInfo.colorMap[pix[i * w + j]]);
			j++;
		}
		originalColors = fromColors.ToArray();

		j = 0;
		i--;

		List<Color32> toColors;
		List<ColorVariationInfo> variations = new List<ColorVariationInfo>();
		while (pix[i * w + j].a > 0f)
		{
			toColors = new List<Color32>();
			while (pix[i * w + j].a > 0f)
			{
				toColors.Add(pix[i * w + j]);
				j++;
			}
			if (toColors.Count > 0)
			{
				if (fromColors.Count == toColors.Count)
				{
					variations.Add(new ColorVariationInfo(originalColors, toColors.ToArray()));
				}
				else
				{
					throw new Exception($"{name} color variations are not configured correctly");
				}
			}
			i--;
			j = 0;
		}
		colorVariations = variations.ToArray();
	}

	public void ClearColorPalettePixels()
	{
		if (isInitializing)
		{
			return;
		}
		Sprite f = originalFrame;
		if (f == null)
		{
			return;
		}
		int w = f.texture.width;
		int h = f.texture.height;
		Color32[] pix = f.texture.GetPixels32();

		int i = h - 1;
		int j = 0;

		bool textureDirty = false;
		while (pix[i * w + j].a > 0f)
		{
			while (pix[i * w + j].a > 0f)
			{
				f.texture.SetPixel(j, i, Color.clear);
				textureDirty = true;
				j++;
			}
			i--;
			j = 0;
		}
		if (textureDirty)
		{
			f.texture.Apply();
		}
	}

	private void InitializeFrameColors()
	{
		HashSet<byte> greens = new HashSet<byte>();
		for (int i = 1; i < AnimationTags.totalFrameCount; i++)
		{
			Sprite f = GetFrame(i);
			if (f != null)
			{
				Color32[] pix = f.texture.GetPixels32();
				foreach (Color32 c in pix)
				{
					if (c.a > 0f)
					{
						greens.Add(c.g);
					}
				}
			}
		}

		var palletes = new List<PaletteColor>();
		List<Color32> allColors = new List<Color32>();

		foreach (var v in greens.OrderBy(p => p))
		{
			if (!PaletteInfo.indexMap.ContainsKey(v))
			{
				Debug.LogError(v + " : " + PaletteInfo.indexMap.ContainsKey(v));
			}
			palletes.Add(PaletteInfo.indexMap[v]);
			allColors.Add(PaletteInfo.indexMap[v].color);
		}
		palleteColors = palletes.ToArray();
		colors = allColors.ToArray();
	}

	public Sprite GetFrame(int frameNum)
	{
		if (layerDataLoaded == false)
		{
			LoadLayerData();
		}

		if (frameNum == 0)
		{
			return originalFrame;
		}
		return layerData ? layerData.frames[frameNum] : emptyFrames[frameNum];
	}

	public Sprite GetFrame(AnimationTagType tag, int frameNum)
	{
		return GetFrame(AnimationTags.tags[tag].frames[frameNum]);
	}


	[Serializable]
	public class ColorVariationInfo
	{
		public PaletteColor[] from;
		public Color32[] to;

		public ColorVariationInfo(PaletteColor[] from, Color32[] to)
		{
			this.from = from;
			this.to = to;
		}
	}

	public override string ToString()
	{
		return accessoryInfo != null && !string.IsNullOrEmpty(accessoryInfo.displayName) ? accessoryInfo.displayName : variationName;
	}
}
