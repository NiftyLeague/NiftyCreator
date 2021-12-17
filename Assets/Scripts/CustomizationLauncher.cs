using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomizationLauncher : MonoBehaviour
{
	private static CustomizationLauncher I;
	public static int[] removedTraits;

	public CustomizationManager customizationManager;
	public RectTransform options;
	public Text versionText;
	public RectTransform block;

	internal static string apiNetwork;
	internal static string apiVersion;
	internal static string apiUrl;

	private int initializationRetries = 2;
	private bool isCustomizationStarted = false;


	private void Awake()
	{
		I = this;
#if UNITY_STANDALONE && !UNITY_EDITOR
		StartCustomization();
	}
#else
		versionText.text = $"{Application.version}{Accessories.version}";
		block.gameObject.SetActive(true);
	}

	private void Start()
	{
		JSWrapper.DispatchEvent("OnGameReady", "");
		//StartCoroutine(GetConfiguration(0.25f));
		Invoke(nameof(DisplayInitializationDialog), 1f);
		Invoke(nameof(StartCustomization), 3.5f);
	}

	private IEnumerator GetConfiguration(float delay)
	{
		yield return new WaitForSeconds(delay);
		initializationRetries--;
		UnityWebRequest request = UnityWebRequest.Get("https://d7ct17ettlkln.cloudfront.net/assets/config");
		yield return request.SendWebRequest();
		Dictionary<string, string> config = null;
		try
		{
			config = ParseConfig(request.downloadHandler.text);
		}
		catch (Exception)
		{
			Debug.LogWarning("Failed to parse configuration");
		}

		if (config == null || config.Count < 1 || !config.ContainsKey("MinAllowedVersion") ||
			string.Compare(Application.version.Replace("m", ""), config["MinAllowedVersion"]) < 0)
		{
			AuthenticationFailed();
			yield break;
		}

		JSWrapper.GetConfiguration(gameObject.name, nameof(OnGetConfigurationResponse));
	}


	private Dictionary<string, string> ParseConfig(string configText)
	{
		Dictionary<string, string> config = new Dictionary<string, string>();
		string configValue = XUtils.Decrypt(Convert.FromBase64String(configText), true);
		Debug.LogWarning(configValue);
		foreach (string line in configValue.Split('\n'))
		{
			var tokens = line.Split(',');
			if (tokens.Length == 2)
			{
				config.Add(tokens[0], tokens[1]);
			}
		}
		return config;
	}

	private void DisplayInitializationDialog()
	{
		CharacterCreatorLevel.DisplayTvTip("WE ARE SOLD OUT!\nALL 9900 DEGENS\nMINTED", 15f);
	}

	private void GetRemovedTraits()
	{
		SetStatusText("Loading Available\nTraits");
		JSWrapper.GetRemovedTraits(gameObject.name, nameof(OnRemovedTraitsStringReady));
	}

	private void OnGetConfigurationResponse(string result)
	{
		CancelInvoke(nameof(DisplayInitializationDialog));
		try
		{
			string[] tokens = result.ToLower().Split(',');
			apiNetwork = tokens[0];
			apiVersion = tokens.Length > 1 && !string.IsNullOrEmpty(tokens[1]) ? tokens[1] : "0";
			print($"Using Network ({apiNetwork}) version ({apiVersion})");
			apiUrl = $"https://odgwhiwhzb.execute-api.us-east-1.amazonaws.com/prod/info?network={apiNetwork}&version={apiVersion}&characters=true";
		}
		catch (Exception e)
		{
			Debug.LogError("Initialization failed");
			Debug.LogError(e);
			AuthenticationFailed();
			return;
		}
		if (string.IsNullOrEmpty(apiNetwork))
		{
			AuthenticationFailed();
			return;
		}
#if USE_NETHEREUM
		ContractHelper.GetRemovedTraits(userAddress, OnRemovedTraitsReady);
#else
		if (apiNetwork == "localhost")
		{
			JSWrapper.GetRemovedTraits(gameObject.name, nameof(OnRemovedTraitsStringReady));
		}
		else
		{
			StartCoroutine(DownloadRemovedTraits());
		}
#endif
	}


	private IEnumerator DownloadRemovedTraits()
	{
		UnityWebRequest www = UnityWebRequest.Get(apiUrl);
		yield return www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.Log(www.error);
		}
		else
		{
			JObject response = null;
			try
			{
				response = JObject.Parse(www.downloadHandler.text);
			}
			catch
			{ }
			if (response != null && response["data"]["removedTraits"].Type == JTokenType.Array)
			{
				OnRemovedTraitsStringReady(response["data"]["removedTraits"].ToString());
			}
			else
			{
				Debug.LogWarning("Downloadnig removed traits failed, falling back to browser provided info");
				JSWrapper.GetRemovedTraits(gameObject.name, nameof(OnRemovedTraitsStringReady));
			}

			if (response != null && response["data"]["characters"].Type == JTokenType.Array)
			{
				customizationManager.SetExistingCharacters(response["data"]["characters"].Select(s => s.ToObject<int[]>()).ToList());
			}
		}
	}

	private void OnRemovedTraitsReady(int[] traits)
	{
		removedTraits = traits;
		if (isCustomizationStarted == false)
		{
			Invoke(nameof(StartCustomization), 0.2f);
			isCustomizationStarted = true;
		}
	}

	private void OnRemovedTraitsStringReady(string traitsStr)
	{
		traitsStr = traitsStr.Replace("[", "").Replace("]", "").Replace(" ", "");
		if (traitsStr.Length > 0)
		{
			string[] tokens = traitsStr.Split(',');
			OnRemovedTraitsReady(tokens.Select(s => int.Parse(s)).ToArray());
		}
		else
		{
			OnRemovedTraitsReady(new int[0]);
		}
	}

	private void AuthenticationFailed()
	{
		if (initializationRetries > 0)
		{
			SetStatusText("Initialization\nFailed!\nRetrying...");
			StartCoroutine(GetConfiguration(4f));
		}
		else
		{
			SetStatusText("Initialization\nFailed");
		}
	}

	public static void SetBlockInput(bool block)
	{
		I.block.gameObject.SetActive(block);
	}

	public static void SubmitTraits(string traits)
	{
		CharacterCreatorLevel.HideTv();
		I.block.gameObject.SetActive(true);
		CustomizationManager.SetActionState(-1, true);
		JSWrapper.SubmitTraits(traits, I.gameObject.name, nameof(OnSubmitTraitsResponse));
		CustomizationManager.SetActionState(-1, true);
		I.Invoke(nameof(StopRotatingPose), 1f);
	}

	private void StopRotatingPose()
	{
		CustomizationManager.SetActionState(0, true);
	}

	private void OnSubmitTraitsResponse(string result)
	{
		I.block.gameObject.SetActive(false);
		Debug.Log($"Submit trait response ready: {result}");
		bool success = bool.Parse(result);
		if (success)
		{
			try
			{
				if (AnimationManager.I != null && AnimationManager.I.gameObject != null)
				{
					DestroyImmediate(AnimationManager.I.gameObject);
				}
				if (CustomizationManager.I != null && CustomizationManager.I.gameObject != null)
				{
					DestroyImmediate(CustomizationManager.I.gameObject);
				}
			}
			catch
			{ }
			CustomizationManager.Reset();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}
		else
		{
			CharacterCreatorLevel.EndMintEffects();
			CustomizationManager.SetActionState(0, true);
		}
	}

	private void SetStatusText(string text)
	{
		print(text);
		CharacterCreatorLevel.DisplayTvOverlay(text, -1f);
	}

#endif //#else //#if UNITY_STANDALONE

	private void StartCustomization()
	{
#if !UNITY_STANDALONE || UNITY_EDITOR
		//CharacterCreatorLevel.HideTv();
#endif
		isCustomizationStarted = true;
		CharacterCreatorLevel.I.OnCustomizationInitialized();
		customizationManager.gameObject.SetActive(true);
		options.gameObject.SetActive(true);
		block.gameObject.SetActive(false);
	}
}
