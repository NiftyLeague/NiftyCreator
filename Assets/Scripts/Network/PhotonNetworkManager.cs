#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
using Photon.Realtime;
using PhotonPlayer = Photon.Realtime.Player;
using ExitGames.Client.Photon;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

#if PHOTON_UNITY_NETWORKING
public class PhotonNetworkManager : MonoBehaviourPunCallbacks
#else
public class PhotonNetworkManager : MonoBehaviour
#endif
{
	private bool isConnecting = false;

#if PHOTON_UNITY_NETWORKING
	public bool IsConnectedAndReady { get { return PhotonNetwork.IsConnected && PhotonNetwork.IsConnectedAndReady && PhotonNetwork.CurrentLobby != null; } }
#else
	public bool IsConnectedAndReady = false;
#endif

#if PHOTON_UNITY_NETWORKING
	public void Initialize()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.SendRate = 60;
	}


	public void Connect()
	{
		if (PhotonNetwork.IsConnected && !PhotonNetwork.InRoom)
		{
			print("Connect: Already connected, joining lobby");
			PhotonNetwork.JoinLobby();
		}
		else
		{
			isConnecting = true;
			PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = Application.version;
		}
	}

	public void JoinRoom()
	{
		if (PhotonNetwork.IsConnected && !PhotonNetwork.InRoom)
		{
			PhotonNetwork.JoinRandomRoom(GetExpectedRoomProperties(), 4);
		}
	}


	private Hashtable GetExpectedRoomProperties()
	{
		return new Hashtable {
			{ ROOM_STATE_KEY, RoomState.Matchmaking },
		};
	}

	private RoomOptions GetRoomCreationOptions()
	{
		return new RoomOptions
		{
			MaxPlayers = 4,
			CustomRoomProperties = GetExpectedRoomProperties(),
			CustomRoomPropertiesForLobby = new string[] { ROOM_STATE_KEY },
		};
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public void LoadArena()
	{
		PhotonNetwork.LoadLevel("JoinScreen");
	}


	public override void OnConnectedToMaster()
	{
		if (isConnecting)
		{
			isConnecting = false;
			PhotonNetwork.JoinLobby();
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		print("OnJoinRandomFailed " + returnCode + " " + message);
		PhotonNetwork.CreateRoom(null, GetRoomCreationOptions());
	}

	public override void OnJoinedRoom()
	{
		print("Room name: " + PhotonNetwork.CurrentRoom.Name);

		//PhotonNetwork.Instantiate(NetworkPrefabs.Player.name, Vector3.zero, Quaternion.identity);
		SetPlayerProps();

		if (PhotonNetwork.IsMasterClient)
		{
			Invoke(nameof(LoadArena), 0.1f);
		}
	}


	private void SetPlayerProps()
	{
		PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
		{
			{ "id", NiftyUsers.me.id },
			{ "bros", NiftyUsers.me.bros.Select(b=>b.traitsStr).ToArray() },
			{ "broIdx", 0 },
		});
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		print("OnDisconnected: " + cause);
	}

	public override void OnLeftRoom()
	{
		//SceneManager.LoadScene("Test");
	}

	public override void OnPlayerEnteredRoom(PhotonPlayer other)
	{
		Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
		}
	}


	public override void OnPlayerLeftRoom(PhotonPlayer other)
	{
		Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
		}
	}


	[NaughtyAttributes.Button]
	private void PrintPlayerProps()
	{
		foreach (var p in PhotonNetwork.PlayerList)
		{
			print(p.ToStringFull());
		}
	}

	const string ROOM_STATE_KEY = "rs";
	enum RoomState
	{
		None,
		Matchmaking,
		Starting,
		InProgress,
	}
#endif //#if PHOTON_UNITY_NETWORKING
}
