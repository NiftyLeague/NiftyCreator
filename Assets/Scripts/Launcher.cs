using NaughtyAttributes;
#if USE_NETHEREUM
using Nethereum.Signer;
#endif //#if USE_NETHEREUM
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
	public static Launcher I;
	public LauncherUI ui;
	public float connectionTimeout;

	private State state = State.Begin;
	private float connectionStartTime = 0f;
	[SerializeField]
	private NiftyUser user = null;
	private string interimUserAddress = "";
	private float startDelay;
	private string sessionId;


	private void Awake()
	{
		I = this;
		state = State.Begin;
		Application.runInBackground = true;
		ui.SetVersion($"v{Application.version}");
		ui.SetStatus("Initializing Assets");
		startDelay = XRandom.NextFloat(0f, 0.5f);
		sessionId = System.Guid.NewGuid().ToString();
		print(sessionId);
	}


	private void Start()
	{
		JSWrapper.DispatchEvent("OnGameReady", "");
	}

	private void Update()
	{
		ui.SetStatusColorFlash(state != State.Connected && state != State.Failure);

		switch (state)
		{
		case State.Begin:
			if (Time.time > startDelay)
			{
				StartAuthentication();
			}
			break;
		case State.Authentication:
		case State.Verification:
		case State.Initialization:
			break;
		case State.Rasterization:
			int count = user.bros.Count(bro => bro.sprites != null && bro.sprites.Count == AnimationTags.totalFrameCount);
			if (count > 0 && count == user.bros.Count)
			{
				StartConnection();
			}
			else
			{
				ui.SetStatus($"Loading Your NiftyBros\n{count + 1}/{user.bros.Count}");
			}

			break;
		case State.Connection:
			if (NetworkController.IsConnectedAndReady)
			{
				state = State.Connected;
				ui.SetStatus("Connected");
				ui.FadeStatus();
				ui.EnableOptions();
			}
			else if (Time.realtimeSinceStartup - connectionStartTime > connectionTimeout)
			{
				Fail("Connection Failed\nRetrying");
				Invoke(nameof(StartConnection), 2f);
			}
			break;
		case State.Connected:

			break;
		}
	}

	private void StartAuthentication()
	{
		state = State.Authentication;
		ui.SetStatus("Authenticating");
		JSWrapper.StartAuthentication(gameObject.name, nameof(OnAuthencationResponse));
	}

	private void VerifyAccount(string address)
	{
		ui.SetStatus("Verifying Account");
		string verifiedAddress = GetCachedVerifiedAddress();
		if (!string.IsNullOrEmpty(verifiedAddress) && (verifiedAddress.ToLower() == address.ToLower() || Application.isEditor))
		{
			user = NiftyUsers.Login(address);
			state = State.Initialization;
			InitializeProfile();
			Debug.LogError("use cache");
		}
		else
		{
			string verificationMessage = GetVerificationMessage(address);
			JSWrapper.SignMessage(interimUserAddress, verificationMessage, sessionId, gameObject.name, nameof(OnSignatureResponse));
		}
	}

	private string GetCachedVerifiedAddress()
	{
		string cached = PlayerPrefs.GetString("cached", null);
		if (string.IsNullOrEmpty(cached))
		{
			return null;
		}
		string value = XUtils.Decrypt(Convert.FromBase64String(cached), true);
		string[] tokens = value.Split(',');
		if (tokens.Length != 3)
		{
			return null;
		}

		int timestamp;
		int.TryParse(tokens[0], out timestamp);
		if (timestamp <= 0 || XUtils.Timestamp() - timestamp > XUtils.MIN * 15)
		{
			PlayerPrefs.DeleteKey("cached");
			PlayerPrefs.Save();
			return null;
		}

		long id;
		long.TryParse(tokens[1], out id);
		if (id != XUtils.GetSimpleDeviceId())
		{
			PlayerPrefs.DeleteKey("cached");
			PlayerPrefs.Save();
			return null;
		}

		if (!tokens[2].StartsWith("0x") || tokens[2].Length < 20)
		{
			PlayerPrefs.DeleteKey("cached");
			PlayerPrefs.Save();
			return null;
		}
		return tokens[2];
	}

	private void SetCachedVerifiedAddress(string address)
	{
		string value = $"{XUtils.Timestamp()},{XUtils.GetSimpleDeviceId()},{address.ToLower()}";
		string cacheValue = Convert.ToBase64String(XUtils.Encrypt(value, true));
		PlayerPrefs.SetString("cached", cacheValue);
		PlayerPrefs.Save();
	}

	private void StartConnection()
	{
		state = State.Connection;
		ui.SetStatus("Contacting Nifty Servers");
#if PHOTON_UNITY_NETWORKING
		NetworkController.Initialize();
#endif
		connectionStartTime = Time.realtimeSinceStartup;
	}

	private void OnSignatureResponse(string result)
	{
		user = null;
		try
		{
			string[] tokens = result.Split(',');
			bool success = bool.Parse(tokens[0]);
			if (!success)
			{
				string reason = tokens.Length > 1 ? tokens[1] : "Unknown";
				throw new Exception($"Verification unsuccessful: {reason}");
			}
#if USE_NETHEREUM
			EthereumMessageSigner signer = new EthereumMessageSigner();
			var signedMessage = tokens[1];
			var address = signer.EncodeUTF8AndEcRecover(GetVerificationMessage(interimUserAddress), signedMessage);
			print($"Interim: {interimUserAddress}, Verified: {address.ToLower()}");
			if (interimUserAddress.ToLower() == address.ToLower() || Application.isEditor)
			{
				user = NiftyUsers.Login(address);
				SetCachedVerifiedAddress(address);
			}
#endif //#if USE_NETHEREUM
		}
		catch (Exception e)
		{
			Debug.LogError("Verification failed");
			Debug.LogError(e);
			VerificationFailed();
		}

		if (user == null)
		{
			VerificationFailed();
			return;
		}

		state = State.Initialization;
		InitializeProfile();
	}

	private void InitializeProfile()
	{
		ui.SetStatus("Initializing Profile");
#if USE_NETHEREUM
		ContractHelper.GetCharacters(user.Address, (characterTraits) =>
		{
			if (characterTraits.Count == 0)
			{
				characterTraits = new List<int[]> {
					CustomizationManager.I.GetRandomTraits().Select(e=>e.Value.id).ToArray(),
					CustomizationManager.I.GetRandomTraits().Select(e=>e.Value.id).ToArray(),
					CustomizationManager.I.GetRandomTraits().Select(e=>e.Value.id).ToArray(),
					CustomizationManager.I.GetRandomTraits().Select(e=>e.Value.id).ToArray(),
					CustomizationManager.I.GetRandomTraits().Select(e=>e.Value.id).ToArray(),
					CustomizationManager.I.GetRandomTraits().Select(e=>e.Value.id).ToArray(),
				};
			}
			user.SetBros(characterTraits);
			if (user.bros.Count == 0)
			{
				Fail("No Nifty Bros available on your account to play with!");
				return;
			}
			user.RasterizeBros();
			state = State.Rasterization;
			ui.SetStatus("Loading Your NiftyBros");
		});
#endif //#if USE_NETHEREUM
	}

	private void OnAuthencationResponse(string result)
	{
		user = null;
		interimUserAddress = null;
		try
		{
			bool success = false;
			string address;
			List<string> characterTraits = new List<string>();

			string[] tokens = result.Split(',');
			success = bool.Parse(tokens[0]);
			if (!success)
			{
				throw new Exception($"Authentication unsuccessful: {result}");
			}
			address = tokens[1];
			if (address.Length < 20 || !Utils.IsHexString(address))
			{
				throw new Exception("Account address is not a valid hex string");
			}
			interimUserAddress = address.ToLower();
		}
		catch (Exception e)
		{
			Debug.LogError("Authentication failed");
			Debug.LogError(e);
			AuthenticationFailed();
			return;
		}
		if (interimUserAddress == null)
		{
			AuthenticationFailed();
			return;
		}
		state = State.Verification;
		VerifyAccount(interimUserAddress);
	}

	private void AuthenticationFailed()
	{
		Fail("Authentication Failed\nRetrying");
		Invoke(nameof(StartAuthentication), 2f);
	}

	private void VerificationFailed()
	{
		Fail("Failed to Verify Account\nRetrying");
		Invoke(nameof(StartAuthentication), 2f);
	}


	private void Fail(string message)
	{
		ui.SetStatus(message);
		state = State.Failure;
	}

	private string GetVerificationMessage(string address)
	{
		string id = address.Replace("0x", "").ToLower();
		string nonce = $"0x{sessionId.GetHashCode():X}".ToLower();
		return $"Please sign this message to verify that 0x{id.Substring(0, 4)}...{id.Substring(id.Length - 4)} belongs to you: {nonce}";
	}

	public void UI_Start()
	{
		if (state == State.Connected)
		{
#if PHOTON_UNITY_NETWORKING
			NetworkController.JoinRoom();
#endif
		}
	}


	private enum State
	{
		Begin,
		Authentication,
		Verification,
		Initialization,
		Connection,
		Rasterization,
		Connected,
		Ready,
		Failure,
	}
}
