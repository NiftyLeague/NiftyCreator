#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTest : MonoBehaviour
{
	private void SpawnFly()
	{
#if PHOTON_UNITY_NETWORKING
		PhotonNetwork.InstantiateRoomObject(NetworkPrefabs.Fly.name, Vector3.zero, Quaternion.identity, 0, new object[3]);
#endif
	}
}
