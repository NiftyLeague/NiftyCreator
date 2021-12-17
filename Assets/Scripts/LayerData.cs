using UnityEngine;


[CreateAssetMenu(fileName = "LayerData", menuName = "Scriptables/LayerData")]
public class LayerData : ScriptableObject
{
	public string layer;
	public Sprite[] frames;
}
