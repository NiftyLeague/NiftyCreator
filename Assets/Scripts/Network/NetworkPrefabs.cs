using UnityEngine;

public class NetworkPrefabs : MonoBehaviour
{
	private static NetworkPrefabs I;

	[SerializeField]
	private GameObject fly;
	public static GameObject Fly { get { return I.fly; } }

	[SerializeField]
	private GameObject character;
	public static GameObject Character { get { return I.character; } }


	[SerializeField]
	private GameObject player;
	public static GameObject Player { get { return I.player; } }


	private void Awake()
	{
		I = this;
	}
}
