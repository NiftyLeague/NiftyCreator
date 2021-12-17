#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace PlayerOne
{
	[InitializeOnLoad]
	public class AutoRelaunch
	{
		private static string key = "";
		private static bool isCompiling = false;
		private static bool justReloaded = false;
		private static bool isRestarting = false;
		private static bool pollIsCompiling = false;
		private static bool allowsThreadIsPlayig = false;
		private static double relaunchTime = 0f;
		private static Queue<System.Threading.Timer> timers = new Queue<System.Threading.Timer>();

		static AutoRelaunch()
		{
			string stringVersion = Application.unityVersion;
			int version = 546;
			if (stringVersion.StartsWith("20"))
			{
				int.TryParse(stringVersion.Substring(0, 6).Replace(".", ""), out version);
			}
			allowsThreadIsPlayig = version >= 20193;


			EditorApplication.update -= Update;
			EditorApplication.update += Update;
			var p = System.Diagnostics.Process.GetCurrentProcess();
			key = String.Format("unity_{0}_was_compiling", p.Id);
			justReloaded = true;

			if (allowsThreadIsPlayig)
			{
				TryPrepareRelaunch();
				DisposeOldTimers();
				timers.Enqueue(new System.Threading.Timer((e) =>
				{
					UpdateCompileStatus();
				}, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(200)));
			}
		}

		private static void UpdateCompileStatus()
		{
			if (!isCompiling && EditorApplication.isCompiling)
			{
				isCompiling = true;
				EditorApplication.update -= Update;
				Environment.SetEnvironmentVariable(key, "1");
			}
		}

		private static void TryPrepareRelaunch()
		{
			var enabled = EditorPrefs.GetBool("AutoLaunch-Enabled", true);
			var relaunchOnPaused = EditorPrefs.GetBool("AutoLaunch-RelaunchOnPaused", false);
			if (enabled && Environment.GetEnvironmentVariable(key) == "1")
			{
				isCompiling = false;
				Environment.SetEnvironmentVariable(key, "0");
				if (EditorApplication.isPlaying && (!EditorApplication.isPaused || relaunchOnPaused))
				{
					EditorApplication.isPlaying = false;
					EditorApplication.isPaused = false;
					isRestarting = true;
					relaunchTime = EditorApplication.timeSinceStartup + EditorPrefs.GetFloat("AutoLaunch-RelaunchDelay", 0f);
				}
			}
		}

		private static void Update()
		{
			try
			{
				if (isRestarting && !EditorApplication.isPlaying)
				{
					if (relaunchTime < EditorApplication.timeSinceStartup)
					{
						isRestarting = false;
						EditorApplication.isPlaying = true;
					}
				}
				if (!allowsThreadIsPlayig && justReloaded)
				{
					justReloaded = false;
					TryPrepareRelaunch();
					DisposeOldTimers();
					timers.Enqueue(new System.Threading.Timer((e) =>
					{
						pollIsCompiling = true;
					}, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(200)));
				}

				if (pollIsCompiling)
				{
					pollIsCompiling = false;
					UpdateCompileStatus();
				}
			}
			catch { }
		}

		private static void DisposeOldTimers()
		{
			while (timers.Count > 0)
			{
				timers.Dequeue().Dispose();
			}
		}
	}

	public class AutoRelaunchEditorPrefs : EditorWindow
	{
		bool enabled = true;
		float relaunchDelay = 0f;
		bool relaunchOnPaused = false;

		[MenuItem("Tools/Auto Relaunch Settings")]
		static void Init()
		{
			try
			{
				AutoRelaunchEditorPrefs window = (AutoRelaunchEditorPrefs)EditorWindow.GetWindow(typeof(AutoRelaunchEditorPrefs), false, "Auto Relaunch Settings");
				window.Show();
			}
			catch { }
		}

		void OnGUI()
		{
			try
			{
				EditorGUILayout.Space();
				enabled = EditorGUILayout.BeginToggleGroup("Enable Auto Relaunch", enabled);
				EditorGUILayout.Space();
				relaunchDelay = EditorGUILayout.Slider(new GUIContent("Relaunch Delay"), relaunchDelay, 0f, 10.0f, GUILayout.MaxWidth(350f));
				EditorGUILayout.Space();
				relaunchOnPaused = EditorGUILayout.Toggle(new GUIContent("Relaunch when Paused"), relaunchOnPaused);
				EditorGUILayout.EndToggleGroup();
			}
			catch { }
		}

		void OnFocus()
		{
			if (EditorPrefs.HasKey("AutoLaunch-Enabled"))
				enabled = EditorPrefs.GetBool("AutoLaunch-Enabled");
			if (EditorPrefs.HasKey("AutoLaunch-RelaunchDelay"))
				relaunchDelay = EditorPrefs.GetFloat("AutoLaunch-RelaunchDelay");
			if (EditorPrefs.HasKey("AutoLaunch-RelaunchOnPaused"))
				relaunchOnPaused = EditorPrefs.GetBool("AutoLaunch-RelaunchOnPaused");
		}

		void OnLostFocus()
		{
			EditorPrefs.SetBool("AutoLaunch-Enabled", enabled);
			EditorPrefs.SetFloat("AutoLaunch-RelaunchDelay", relaunchDelay);
			EditorPrefs.SetBool("AutoLaunch-RelaunchOnPaused", relaunchOnPaused);
		}

		void OnDestroy()
		{
			EditorPrefs.SetBool("AutoLaunch-Enabled", enabled);
			EditorPrefs.SetFloat("AutoLaunch-RelaunchDelay", relaunchDelay);
			EditorPrefs.SetBool("AutoLaunch-RelaunchOnPaused", relaunchOnPaused);
		}
	}
}

#endif // UNITY_EDITOR
