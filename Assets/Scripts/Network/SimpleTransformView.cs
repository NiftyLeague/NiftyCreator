using UnityEngine;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;

public class SimpleTransformView : MonoBehaviourPun, IPunObservable
#else
public class SimpleTransformView : MonoBehaviour
#endif
{
	private float m_Distance;

	private Vector2 m_Direction;
	private Vector2 m_NetworkPosition;
	private Vector2 m_StoredPosition;
	private bool m_firstTake = false;

	private void Awake()
	{
		m_StoredPosition = transform.localPosition;
		m_NetworkPosition = Vector2.zero;
	}

	private void OnEnable()
	{
		m_firstTake = true;
	}
#if PHOTON_UNITY_NETWORKING
	private void Update()
	{
		var tr = transform;

		if (!this.photonView.IsMine)
		{
			tr.position = Vector2.MoveTowards(tr.position, this.m_NetworkPosition, this.m_Distance * (1.0f / PhotonNetwork.SerializationRate));
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		var tr = transform;
		if (stream.IsWriting)
		{
			this.m_Direction = (Vector2)tr.position - this.m_StoredPosition;
			this.m_StoredPosition = tr.position;
			stream.SendNext((Vector2)tr.position);
			stream.SendNext(this.m_Direction);
			stream.SendNext(tr.localScale.x);
		}
		else
		{
			this.m_NetworkPosition = (Vector2)stream.ReceiveNext();
			this.m_Direction = (Vector2)stream.ReceiveNext();

			if (m_firstTake)
			{
				tr.position = this.m_NetworkPosition;
				this.m_Distance = 0f;
				m_firstTake = false;
			}
			else
			{
				float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
				print((int)(lag* 1000) + " ms lag");
				this.m_NetworkPosition += this.m_Direction * lag;
				this.m_Distance = Vector3.Distance(tr.position, this.m_NetworkPosition);
			}
			tr.localScale = new Vector3((float)stream.ReceiveNext(), 1f, 1f);
		}
	}
#endif //#if PHOTON_UNITY_NETWORKING
}