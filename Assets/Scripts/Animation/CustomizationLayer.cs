using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationLayer : MonoBehaviour
{
	public AnimationLayerVariation layerVariation;
	[OnValueChanged(nameof(ColorValueChanged))]
	public Color32[] colors = new Color32[PaletteInfo.numColors];
	[SerializeField]
	private string variationName;
	[SerializeField]
	public int currentColorVariationIndex;
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	private Texture2D colorSwapTexture;


	private void Start()
	{
		if (layerVariation == null)
		{
			layerVariation = AnimationManager.I.variations[variationName];
		}
	}

	public void Initialize(AnimationLayerVariation variation, int colorVariationIndex)
	{
		this.currentColorVariationIndex = -1;
		this.layerVariation = variation;
		this.variationName = variation.variationName;
		spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.material = CustomizationManager.I.defaultMaterial;
		InitializeColorSwapper();
		SetColorVariation(colorVariationIndex);
		SetFrame(AnimationTagType.Accessories, 0);
	}

	private void InitializeColorSwapper()
	{
		colorSwapTexture = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
		colorSwapTexture.filterMode = FilterMode.Point;
		Color blackColor = new Color(0f, 0f, 0f, 0f);
		for (int i = 0; i < colorSwapTexture.width; ++i)
		{
			colorSwapTexture.SetPixel(i, 0, blackColor);
		}
		colorSwapTexture.Apply();
		spriteRenderer.material.SetTexture("_SwapTex", colorSwapTexture);
	}

	public Sprite GetFrame(AnimationTagType tag, int frame)
	{
		return layerVariation.GetFrame(AnimationTags.tags[tag].frames[frame]);
	}

	public Sprite GetFrameAbsolute(int frame)
	{
		return layerVariation.GetFrame(frame);
	}

	public void SetFrame(AnimationTagType tag, int frame)
	{
		spriteRenderer.sprite = GetFrame(tag, frame);
	}

	public void SetFrameAbsolute(int frame)
	{
		spriteRenderer.sprite = GetFrameAbsolute(frame);
	}

	public void SetFlipX(bool flipX)
	{
		spriteRenderer.flipX = flipX;
	}

	public void SetColorVariation(int colorVariationIndex)
	{
		if (layerVariation.colorVariations.Length > 0 && colorVariationIndex < layerVariation.colorVariations.Length)
		{
			SetColorVariation(layerVariation.colorVariations[Mathf.Max(0, colorVariationIndex)]);
		}
	}

	public void SetColorVariation(AnimationLayerVariation.ColorVariationInfo colorVariationInfo)
	{
		for (int i = 0; i < colorVariationInfo.to.Length; i++)
		{
			ReplaceColor(i, colorVariationInfo.to[i]);
		}
		currentColorVariationIndex = layerVariation.colorVariations.Length > 1 ? Array.FindIndex(layerVariation.colorVariations, cv => cv == colorVariationInfo) : -1;
	}

	public void ReplaceColor(int index, Color32 color)
	{
		PaletteColor pl = layerVariation.originalColors.Length > 0 ? layerVariation.originalColors[index] : layerVariation.palleteColors[index];
		colors[pl.index] = color;
		ColorValueChanged();
	}

	private void ColorValueChanged()
	{
		for (int i = 0; i < colors.Length; i++)
		{
			Color32 color = colors[i];
			if (color.a > 0)
			{
				colorSwapTexture.SetPixel((int)PaletteInfo.paletteColors[i].swapIndex, 0, color);
			}
		}
		colorSwapTexture.Apply();
	}

	[Button]
	private void ExportTexture()
	{
		byte[] _bytes = colorSwapTexture.EncodeToPNG();
		System.IO.File.WriteAllBytes($"{Application.persistentDataPath}/{variationName}.png", _bytes);
	}
}
