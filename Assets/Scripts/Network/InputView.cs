#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif
using System;
using UnityEngine;

#if PHOTON_UNITY_NETWORKING
public class InputView : MonoBehaviourPun
#else
public class InputView : MonoBehaviour
#endif
{
	public int minimumSendRate;
	[HideInInspector]
	public InputReader.Device inputDevice;

	private InputState input;
	private uint lastInputs = 0;
	private byte inputSeqNo = 0;
	private bool inputReceivedLastFrame = false;
	private float lastSendTime = 0f;

	private float minSendRatePerSecond;

	private void Awake()
	{
		minSendRatePerSecond = minimumSendRate > 0 ? 1f / minimumSendRate : float.PositiveInfinity;
	}

#if PHOTON_UNITY_NETWORKING

	void Update()
	{
		if (photonView.IsMine)
		{
			InputReader.GetInput(inputDevice, input);
			uint newInputs = PackInputState(input);

			if (newInputs != lastInputs)
			{
				inputSeqNo++;
				uint seqNo = (uint)inputSeqNo << 24;
				photonView.RPC(nameof(ReceiveInput), RpcTarget.Others, (int)(seqNo | newInputs), new object[] { transform.position.x, transform.position.y });
				lastInputs = newInputs;
				lastSendTime = Time.realtimeSinceStartup;
			}
			else if (Time.realtimeSinceStartup - lastSendTime > minSendRatePerSecond)
			{
				photonView.RPC(nameof(ReceiveExtraData), RpcTarget.Others, new object[] { transform.position.x, transform.position.y });
				lastSendTime = Time.realtimeSinceStartup;
			}
		}
		if (inputReceivedLastFrame)
		{
			InputReader.CacheLastInput(input);
			inputReceivedLastFrame = false;
		}
	}

	[PunRPC]
	private void ReceiveInput(int inputs, object[] extraData)
	{
		float x = (float)extraData[0];
		float y = (float)extraData[1];
		uint uInputs = (uint)inputs;
		// string sInput = Convert.ToString(uInputs, 2).PadLeft(32, '0');
		// print($"{sInput.Substring(0, 8)} {sInput.Substring(8, 8)} {sInput.Substring(16, 8)} {sInput.Substring(24, 8)}");
		uint seqNo = uInputs >> 24;
		if (seqNo <= inputSeqNo && seqNo != 0)
		{
			Debug.LogWarning($"Received out of date seq '{seqNo}'. Last seq num `{inputSeqNo}`");
			return;
		}
		UnpackInputState(uInputs, ref input);
		inputReceivedLastFrame = true;
		transform.position = new Vector3(x, y, transform.position.z);
	}


	[PunRPC]
	private void ReceiveExtraData(object[] extraData)
	{
		float x = (float)extraData[0];
		float y = (float)extraData[1];
		transform.position = new Vector3(x, y, transform.position.z);
	}

	private void UnpackInputState(uint inputs, ref InputState unpackTo)
	{
		if (unpackTo != null)
		{
			unpackTo.aButton = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.bButton = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.xButton = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.yButton = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.up = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.down = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.left = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.right = (inputs & 1) == 1;
			inputs = inputs >> 1;
			unpackTo.start = (inputs & 1) == 1;
		}
	}

	private uint PackInputState(InputState input)
	{
		uint inputs = 0;
		inputs |= (input.aButton ? 1u : 0) << 0;
		inputs |= (input.bButton ? 1u : 0) << 1;
		inputs |= (input.xButton ? 1u : 0) << 2;
		inputs |= (input.yButton ? 1u : 0) << 3;
		inputs |= (input.up ? 1u : 0) << 4;
		inputs |= (input.down ? 1u : 0) << 5;
		inputs |= (input.left ? 1u : 0) << 6;
		inputs |= (input.right ? 1u : 0) << 7;
		inputs |= (input.start ? 1u : 0) << 8;
		return inputs;
	}
#endif //#if PHOTON_UNITY_NETWORKING

	public void SetInputStateRef(ref InputState input)
	{
		this.input = input;
	}
}
