#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PhotonPlayer = Photon.Realtime.Player;
using Photon.Pun.UtilityScripts;
#endif //#if PHOTON_UNITY_NETWORKING
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using NaughtyAttributes;
using UnityEngine.UI;
using System;

#if PHOTON_UNITY_NETWORKING
public class LobbyContoller : MonoBehaviourPunCallbacks
#else
public class LobbyContoller : MonoBehaviour
#endif
{
	public JoinCanvas[] joinCanvas;
	public Text serverInfoText;

#if PHOTON_UNITY_NETWORKING
	private void Awake()
	{
		if (CustomizationManager.I == null)
		{
			SceneManager.LoadScene("Launcher");
			return;
		}
		StartCoroutine(UpdateCanvasPlayers());
		InvokeRepeating(nameof(UpdatePing), 0.5f, 0.25f);
	}

	private void UpdatePing()
	{
		PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "ping", PhotonNetwork.GetPing() } });
	}

	private void FixedUpdate()
	{
		serverInfoText.text = $"{PhotonNetwork.CloudRegion.ToUpper()}: {PhotonNetwork.ServerTimestamp:#,#}";
		serverInfoText.enabled = NetworkController.debug;
	}

	private IEnumerator UpdateCanvasPlayers()
	{
		for (; ; )
		{
			for (int i = 0; i < joinCanvas.Length; i++)
			{
				JoinCanvas join = joinCanvas[i];
				PhotonPlayer player = GetPlayerByIndex(i);
				if (player != null && !join.HasAssignedPlayer())
				{
					StartCoroutine(AssignPlayer(player));
				}
				else if (player == null && join.HasAssignedPlayer())
				{
					join.UnassignPlayer();
				}
			}
			yield return new WaitForSeconds(0.25f);
		}
	}

	private IEnumerator AssignPlayer(PhotonPlayer photonPlayer)
	{
		yield return new WaitUntil(() => { return photonPlayer.GetPlayerNumber() >= 0; });
		yield return new WaitUntil(() => { return photonPlayer.CustomProperties.ContainsKey("bros") && (photonPlayer.CustomProperties["bros"] as string[]).Length > 0; });
		int playerIndex = photonPlayer.GetPlayerNumber();

		JoinCanvas join = joinCanvas[playerIndex];
		Player player = new Player(photonPlayer, playerIndex + (photonPlayer.IsLocal ? 4 : 0));
		Character ch = null;
		join.AssignPlayer(player, playerIndex);
		if (photonPlayer.IsLocal)
		{
			ch = PhotonNetwork.Instantiate(NetworkPrefabs.Character.name, Terrain.GetSpawnPoint(playerIndex), Quaternion.identity).GetComponent<Character>();
		}
		else
		{
			for (; ; )
			{
				yield return new WaitForSeconds(0.1f);
				var characterView = FindObjectsOfType<CharacterView>().FirstOrDefault(cv => cv.photonView.Owner == photonPlayer);
				if (characterView != null)
				{
					ch = characterView.gameObject.GetComponent<Character>();
					break;
				}
			}
		}
		ch.SetActive(false);
		ch.SetPlayer(player);
		player.SetCharacter(ch);
		join.UpdatePlayerBro(true);
	}

	private PhotonPlayer GetPlayerByIndex(int index)
	{
		return PhotonNetwork.PlayerList.FirstOrDefault(p => p.GetPlayerNumber() == index);
	}

	public override void OnPlayerPropertiesUpdate(PhotonPlayer player, Hashtable changedProps)
	{
		foreach (var p in changedProps)
		{
			//print($"{p.Key}: {player.CustomProperties[p.Key]}");
		}

		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
	}
#endif //#if PHOTON_UNITY_NETWORKING
}
