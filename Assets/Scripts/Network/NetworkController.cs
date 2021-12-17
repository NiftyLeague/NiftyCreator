#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif

using System;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
	private static NetworkController I;
	public static bool debug = false;
	[SerializeField]
	private PhotonNetworkManager photonNetworkManager;

	public static bool IsConnectedAndReady { get { return I.photonNetworkManager.IsConnectedAndReady; } }

#if PHOTON_UNITY_NETWORKING

	public static bool HasAuthorityWithWarning
	{
		get
		{
			bool hasAuth = HasAuthority;
			if (!hasAuth) { Debug.LogWarning("Unexpected HasAuthority test"); }
			return hasAuth;
		}
	}


	public static bool HasAuthority { get { return PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected; } }

	public static void Initialize()
	{
		DontDestroyOnLoad(I.gameObject);
		DontDestroyOnLoad(I.photonNetworkManager.gameObject);
		I.photonNetworkManager.Initialize();
		I.photonNetworkManager.Connect();
	}

	public static void JoinRoom()
	{
		I.photonNetworkManager.JoinRoom();
	}

	private void Awake()
	{
		I = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			debug = !debug;
		}
	}
#endif //#if PHOTON_UNITY_NETWORKING
}
