using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections;

#if UNITY_EDITOR || UNITY_STANDALONE
using System.Reflection;
#endif // #if UNITY_EDITOR

public class JSWrapper : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR

	[DllImport("__Internal")]
	public static extern void DispatchEvent(string eventName, string detail);
	[DllImport("__Internal")]
	public static extern void SubmitTraits(string traits, string name, string callback);
	[DllImport("__Internal")]
	public static extern void StartAuthentication(string name, string callback);
	[DllImport("__Internal")]
	public static extern void SignMessage(string from, string message, string password, string name, string callback);
	[DllImport("__Internal")]
	public static extern void GetRemovedTraits(string name, string callback);
	[DllImport("__Internal")]
	public static extern void GetConfiguration(string name, string callback);

#else // #if UNITY_WEBGL && !UNITY_EDITOR

	private static MonoBehaviour _launcher = null;
	private static MonoBehaviour Launcher
	{
		get
		{
			if (_launcher != null)
			{
				return _launcher;
			}
			_launcher = FindObjectOfType<Launcher>();
			if (_launcher == null)
			{
				_launcher = FindObjectOfType<CustomizationLauncher>();
			}
			return _launcher;
		}
	}

	public static void DispatchEvent(string eventName, string detail)
	{
		PrintCallDetails(MethodBase.GetCurrentMethod(), eventName, detail);
	}


	public static void SubmitTraits(string traits, string name, string callback)
	{
		PrintCallDetails(MethodBase.GetCurrentMethod(), traits, name, callback);

		Launcher.StartCoroutine(OnSubmitTraitMapMock());
		IEnumerator OnSubmitTraitMapMock()
		{
			yield return new WaitForSeconds(5f);
			Launcher.SendMessage(callback, "true");
		}
	}

	public static void StartAuthentication(string name, string callback)
	{
		PrintCallDetails(MethodBase.GetCurrentMethod(), name, callback);
		Launcher.StartCoroutine(OnAuthenticatedMock());

		IEnumerator OnAuthenticatedMock()
		{
			yield return new WaitForEndOfFrame();
			//string result = $"true,0x0b9c624f45493f044ca468971110955345281679,Vitalik";
			string result = $"true,0xb970e591772f2ceb482bcd03a8d2f1924a4044ce,Snarfy";
			Launcher.SendMessage(callback, result);
		}
	}

	public static void GetConfiguration(string name, string callback)
	{
		PrintCallDetails(MethodBase.GetCurrentMethod(), name, callback);
		Launcher.StartCoroutine(OnGetConfigurationMock());

		IEnumerator OnGetConfigurationMock()
		{
			yield return new WaitForEndOfFrame();
			//string result = $"rinkeby,1.1.3";
			string result = $"mainnet,";
			Launcher.SendMessage(callback, result);
		}
	}

	public static void SignMessage(string from, string message, string password, string name, string callback)
	{
		PrintCallDetails(MethodBase.GetCurrentMethod(), from, message, password, name, callback);
		Launcher.StartCoroutine(OnVerifyMock());

		IEnumerator OnVerifyMock()
		{
			yield return new WaitForEndOfFrame();
			string result = "true,0x4b533d695cca9f1e65c11a225cebb5087980d1fa14e0a77b42a5e5c5d87e418c177bbef85104c30b6f402616577f613efec1a850c3234976b0b727cd097687161b";
			Launcher.SendMessage(callback, result);
		}
	}

	public static void GetRemovedTraits(string name, string callback)
	{
		PrintCallDetails(MethodBase.GetCurrentMethod(), name, callback);
		Launcher.StartCoroutine(OnGetRemovedTraitsMock());

		IEnumerator OnGetRemovedTraitsMock()
		{
			yield return new WaitForEndOfFrame();
			string result = $"[150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339]";
			Launcher.SendMessage(callback, result);
		}
	}

	private static void PrintCallDetails(MethodBase methodBase, params object[] args)
	{
		string s = $"<b>JSWrapper.{methodBase.Name}</b> was called with the following args";
		var paramsInfo = methodBase.GetParameters();
		for (int i = 0; i < args.Length; i++)
		{
			s += $"\n{paramsInfo[i].Name}: <i>\"{args[i]}\"</i>";
		}
		Debug.Log(s);
	}

	private static GameObject GetLauncher()
	{
		Launcher launcher = FindObjectOfType<Launcher>();
		return launcher != null ? launcher.gameObject : FindObjectOfType<CustomizationLauncher>().gameObject;
	}
#endif // #else
}
