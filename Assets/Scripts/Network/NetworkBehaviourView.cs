using UnityEngine;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
public class NetworkBehaviourView : MonoBehaviourPun, IOnPhotonViewOwnerChange, IOnPhotonViewControllerChange, IPunInstantiateMagicCallback
#else
public class NetworkBehaviourView : MonoBehaviour
#endif
{
	public virtual MonoBehaviour localBehaviour { get; }

#if PHOTON_UNITY_NETWORKING
	private void Awake()
	{
		if (photonView == null)
		{
			DestroyImmediate(this);
			return;
		}
	}

	private void Start()
	{
		localBehaviour.enabled = photonView.IsMine;
	}


	private void OnOwnershipChanged()
	{
		localBehaviour.enabled = photonView.IsMine;
	}

	public void OnOwnerChange(Photon.Realtime.Player newOwner, Photon.Realtime.Player previousOwner)
	{
		print($"OnOwnerChange (newOwner: {newOwner}, previousOwner: {previousOwner})");
		OnOwnershipChanged();
	}

	public void OnControllerChange(Photon.Realtime.Player newController, Photon.Realtime.Player previousController)
	{
		print($"OnControllerChange (newController: {newController}, previousController: {previousController})");
		OnOwnershipChanged();
	}

	public void OnEnable()
	{
		photonView.AddCallbackTarget(this);
	}

	public void OnDisable()
	{
		photonView.RemoveCallbackTarget(this);
	}

	private void OnDestroy()
	{
		if (photonView.IsMine)
		{
			PhotonNetwork.Destroy(photonView);
		}
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		//print(info.Sender.ToStringFull());
	}
#endif //#if PHOTON_UNITY_NETWORKING
}
