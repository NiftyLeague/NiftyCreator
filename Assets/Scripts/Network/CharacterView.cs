#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif
using UnityEngine;

public class CharacterView : NetworkBehaviourView
{
	public Character character;

#if PHOTON_UNITY_NETWORKING
	private void Awake()
	{
		if (photonView.IsMine)
		{
			character.GetComponent<CharacterEvents>().onEventDispatched += OnEventDispatched;
		}
	}

	private void OnEventDispatched(CharacterEventType eventType, params object[] args)
	{
		Vector2 eventPos = character.transform.position;
		Character victim = null;
		switch (eventType)
		{
		case CharacterEventType.HitInit:
			victim = args[0] as Character;
			photonView.RPC(nameof(RPC_HitInit), RpcTarget.All, eventPos, GetViewId(victim), (Vector2)victim.transform.position, args[1], args[2], args[3]);
			break;
		case CharacterEventType.SoftHitInit:
			victim = args[0] as Character;
			photonView.RPC(nameof(RPC_SoftHitInit), RpcTarget.All, eventPos, GetViewId(victim), (Vector2)victim.transform.position, args[1], args[2], args[3], args[4]);
			break;
		}
	}


	[PunRPC]
	private void RPC_SoftHitInit(Vector2 pos, int victimViewId, Vector2 victimPos, Vector2 dir, float power, float victimTimeBump, float attackerTimeBump)
	{
		character.transform.position = new Vector3(pos.x, pos.y, character.transform.position.z);
		PhotonView victim = PhotonNetwork.GetPhotonView(victimViewId);
		victim.transform.position = new Vector3(victimPos.x, victimPos.y, victim.transform.position.z);
		Character victimCh = victim.GetComponent<Character>();
		victimCh.GetSoftHitInit(dir, character, power, victimTimeBump, attackerTimeBump);

		if (victim.IsMine)
		{
			victim.RPC(nameof(RPC_SotHitStart), RpcTarget.All, dir, photonView.ViewID, power, victimTimeBump, attackerTimeBump);
		}
	}

	[PunRPC]
	private void RPC_SotHitStart(Vector2 dir, int attackerViewId, float power, float victimTimeBump, float attackerTimeBump)
	{
		character.GetSoftHit(dir, GetCharacter(attackerViewId), power, victimTimeBump, attackerTimeBump);
	}


	[PunRPC]
	private void RPC_HitInit(Vector2 pos, int victimViewId, Vector2 victimPos, Vector2 dir, float power, int hitsTaken)
	{
		character.transform.position = new Vector3(pos.x, pos.y, character.transform.position.z);
		PhotonView victim = PhotonNetwork.GetPhotonView(victimViewId);
		victim.transform.position = new Vector3(victimPos.x, victimPos.y, victim.transform.position.z);
		Character victimCh = victim.GetComponent<Character>();
		victimCh.GetHitInit(dir, hitsTaken, power, character);

		if (victim.IsMine)
		{
			victim.RPC(nameof(RPC_HitStart), RpcTarget.All, dir, power, photonView.ViewID);
		}
	}

	[PunRPC]
	private void RPC_HitStart(Vector2 dir, float power, int attackerViewId)
	{
		character.GetHit(dir, character.hitsTaken + power, GetCharacter(attackerViewId));
	}


	private Character GetCharacter(int viewId)
	{
		return PhotonNetwork.GetPhotonView(viewId).GetComponent<Character>();
	}

	private int GetViewId(MonoBehaviour mb)
	{
		return mb.GetComponent<PhotonView>().ViewID;
	}

#endif //#if PHOTON_UNITY_NETWORKING
}
