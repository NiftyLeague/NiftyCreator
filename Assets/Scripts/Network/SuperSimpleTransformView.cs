using UnityEngine;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
public class SuperSimpleTransformView : MonoBehaviourPun, IPunObservable
#else
public class SuperSimpleTransformView : MonoBehaviour
#endif
{
	public float positionErrorThreshold = 1f;

#if PHOTON_UNITY_NETWORKING
	private Vector3 networkPosition = Vector3.zero;

	private void Awake()
	{
		if (photonView == null)
		{
			DestroyImmediate(this);
			return;
		}
	}


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(transform.position.x);
			stream.SendNext(transform.position.y);
		}
		else
		{
			transform.position = new Vector3((float)stream.ReceiveNext(), (float)stream.ReceiveNext(), transform.position.z);
			float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
			print((int)(lag * 1000) + " ms lag");
		}
	}

	public void LateUpdate()
	{
		if (!photonView.IsMine)
		{
			Vector3 posError = networkPosition - transform.position;
			print(posError);
			if (posError.magnitude > positionErrorThreshold)
			{
				transform.position = networkPosition;
			}
		}
	}
#endif //#if PHOTON_UNITY_NETWORKING
}
