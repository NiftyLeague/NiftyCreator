using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using NaughtyAttributes;

public class CharacterCustomizer : MonoBehaviour
{
	public Transform layersRoot;
	public CustomizationLayer[] layers;

	[SerializeField]
	[OnValueChanged(nameof(ReplaceBodyColors))]
	private Color32[] bodyColors;

	[SerializeField]
	[OnValueChanged(nameof(ReplaceEyeColors))]
	private Color32[] eyeColors;

	private AnimationTagType currentTag = AnimationTagType.Win;
	private int currentFrame = -1;


	public void SetFrame(AnimationTagType tag)
	{
		SetFrame(tag, 0);
	}

	public void SetFrame(AnimationTagType tag, int frame, bool force = false)
	{
		if (tag != currentTag || currentFrame != frame || force)
		{
			currentTag = tag;
			currentFrame = frame;
			for (int i = 0; i < layers.Length; i++)
			{
				layers[i].SetFrame(tag, frame);
			}
		}
	}

	public void SetFrameAbsolute(int frame, bool force = false)
	{
		if (currentFrame != frame || force)
		{
			currentFrame = frame;
			for (int i = 0; i < layers.Length; i++)
			{
				layers[i].SetFrameAbsolute(frame);
			}
		}
	}

	public void SetColor(Color color)
	{
	}

	public Color GetColor()
	{
		return Color.white;
	}

	public void SetFlipX(bool flipX)
	{
		for (int i = 0; i < layers.Length; i++)
		{
			layers[i].SetFlipX(flipX);
		}
	}

	public Vector3 GetSpriteOffset()
	{
		return layersRoot.localPosition;
	}

	public Quaternion GetSpriteRotation()
	{
		return layersRoot.localRotation;
	}

	public void SetSpriteRotation(Quaternion rotation)
	{
		layersRoot.localRotation = rotation;
	}

	public void SetSpriteOffset(Vector3 offset)
	{
		layersRoot.localPosition = offset;
	}

	public void SetLayer(AnimationLayer layer, AnimationLayerVariation variation)
	{
		List<AnimationLayerVariation> animLayes = new List<AnimationLayerVariation>();
		foreach (var l in layers)
		{
			if (l.layerVariation.animationLayer.layer != layer.layer)
			{
				animLayes.Add(l.layerVariation);
			}
		}
		if (variation != null)
		{
			animLayes.Add(variation);
		}
		SetLayers(animLayes.ToArray());
		ReplaceBodyAndEyeColors();
	}

	public void SetLayer(AnimationLayer layer, AnimationLayerVariation variation, AnimationLayerVariation.ColorVariationInfo colorVariationInfo)
	{
		SetLayer(layer, variation);
		CustomizationLayer cl = layers.Single(l => l.layerVariation == variation);
		cl.SetColorVariation(colorVariationInfo);
	}

	public void CharacterTypeChanged()
	{
		foreach (CustomizationLayer cl in layers)
		{
			var variation = cl.layerVariation;
			if (variation.animationLayer.Required)
			{
				if (Regex.Match(variation.variationName, @"^b_[a-z0-9]+_head$").Success)
				{
					bodyColors = variation.colorVariations[0].to.ToArray();
				}
				else if (Regex.Match(variation.variationName, @"^b_[a-z]+_eyes$").Success)
				{
					eyeColors = variation.colorVariations[0].to.ToArray();
				}
			}
		}
	}

	public void SetLayers(AnimationLayerVariation[] animationLayerVariations)
	{
		Dictionary<AnimationLayerVariation, int> currentColorVariations = new Dictionary<AnimationLayerVariation, int>();
		foreach (var cl in layers)
		{
			currentColorVariations[cl.layerVariation] = cl.currentColorVariationIndex;
		}

		ClearLayers();
		float z = 0f;
		List<CustomizationLayer> customizationLayers = new List<CustomizationLayer>();
		foreach (AnimationLayerVariation variation in animationLayerVariations.OrderBy(v => v.animationLayer.layer))
		{
			GameObject layer = Utils.CreateGameObject(layersRoot.gameObject, variation.variationName);
			layer.layer = LayerMask.NameToLayer("Character");
			CustomizationLayer cl = layer.AddComponent<CustomizationLayer>();
			int colorVariationIndex = currentColorVariations.ContainsKey(variation) ? currentColorVariations[variation] : -1;
			cl.Initialize(variation, colorVariationIndex);

			Vector3 pos = layer.transform.localPosition;
			pos.z = z;
			z += 0.01f;
			layer.transform.localPosition = pos;
			customizationLayers.Add(cl);
		}
		layers = customizationLayers.ToArray();
	}

	public void ReplaceBodyAndEyeColors()
	{
		ReplaceBodyColors();
		ReplaceEyeColors();
	}

	public void SetBodyColor(int index, Color32 color)
	{
		bodyColors[index] = color;
		ReplaceBodyColors();
	}

	public AnimationLayerVariation.ColorVariationInfo GetCurrentBodyColorInfo(int index)
	{
		foreach (CustomizationLayer cl in layers)
		{
			string name = cl.layerVariation.variationName;
			if (name.StartsWith("b_") && name.EndsWith("_head"))
			{
				foreach (var variation in cl.layerVariation.colorVariations)
				{
					if (variation.to[index].SameColorAs(bodyColors[index]))
					{
						return variation;
					}
				}
			}
		}
		return null;
	}

	public AccessoryOptionEnumrator.AccessoryOption GetCurrentAccessoryOption(AnimationLayerVariation[] variations)
	{
		CustomizationLayer cl = layers.FirstOrDefault(l => Array.Exists(variations, v => v == l.layerVariation));
		if (cl)
		{
			return new AccessoryOptionEnumrator.AccessoryOption(cl.layerVariation, cl.currentColorVariationIndex);
		}
		return null;
	}

	public AnimationLayerVariation.ColorVariationInfo GetCurrentEyeColorInfo(int index)
	{
		foreach (CustomizationLayer cl in layers)
		{
			string name = cl.layerVariation.variationName;
			if (name.StartsWith("b_") && name.EndsWith("_eyes"))
			{
				foreach (var variation in cl.layerVariation.colorVariations)
				{
					if (variation.to[index].SameColorAs(eyeColors[index]))
					{
						return variation;
					}
				}
			}
		}
		return null;
	}

	private void ReplaceBodyColors()
	{
		foreach (CustomizationLayer cl in layers)
		{
			string name = cl.layerVariation.variationName;
			if (!name.StartsWith("b_"))
			{
				continue;
			}

			if (name.Contains("_fur_paws") || name.Contains("_fur_chest"))
			{
				Color32 furColor = bodyColors[bodyColors.Length - 1];
				cl.ReplaceColor(0, furColor);
			}
			else if (name.EndsWith("_head"))
			{
				cl.ReplaceColor(0, bodyColors[0]);
				try
				{
					cl.ReplaceColor(1, bodyColors[1]);
				}
				catch { }
			}
			else if (name.EndsWith("_head"))
			{
				cl.ReplaceColor(0, bodyColors[0]);
				try
				{
					cl.ReplaceColor(1, bodyColors[1]);
				}
				catch { }
			}
			else if (name == "b_share_base" || name == "b_share_arms_above")
			{
				cl.ReplaceColor(0, bodyColors[0]);
			}
			else if (name.EndsWith("tail_behind") || name.EndsWith("tail_front"))
			{
				cl.ReplaceColor(1, bodyColors[0]);
			}
		}
	}

	public void SetEyeColor(int index, Color32 color)
	{
		eyeColors[index] = color;
		ReplaceEyeColors();
	}

	private void ReplaceEyeColors()
	{
		foreach (CustomizationLayer cl in layers)
		{
			string name = cl.layerVariation.variationName;
			if (name.StartsWith("b_") && name.EndsWith("_eyes"))
			{
				cl.ReplaceColor(0, eyeColors[0]);
				try
				{
					cl.ReplaceColor(1, eyeColors[1]);
				}
				catch { }
			}
		}
	}

	public void ClearLayers()
	{
		foreach (var l in layers)
		{
			DestroyImmediate(l.gameObject);
		}
		layers = new CustomizationLayer[0];

		int max = 100;
		while (layersRoot.childCount != 0 && max-- > 0)
		{
			DestroyImmediate(layersRoot.GetChild(0).gameObject);
		}
	}

	public void RunFrame(AnimationTagType tag, int frame, bool clamp)
	{
		int frameCount = AnimationTags.tags[tag].frameCount;
		if (clamp)
		{
			SetFrame(tag, Mathf.Clamp(frame, 0, frameCount - 1));
		}
		else
		{
			SetFrame(tag, frame % frameCount);
		}
	}
}
