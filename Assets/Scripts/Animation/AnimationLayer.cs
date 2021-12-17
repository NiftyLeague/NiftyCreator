using System;
using System.Linq;
using UnityEngine;

public class AnimationLayer : MonoBehaviour
{
	public int layer;
	public string layerName;
	public AnimationLayerType layerType;
	public CharacterType characterType;
	public AccessoryType customizationCategory;
	public AnimationLayerVariation[] variations;
	public bool Required { get { return layerType != AnimationLayerType.Accessory; } }

	public AnimationLayerVariation[] GetCharacterVariations(CharacterType characterType)
	{
		return variations.Where(v => v.characterType == characterType || v.characterType == CharacterType.Share).ToArray();
	}
}
