
using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

public class ArtifactGenerator : MonoBehaviour
{
	public int defaultFrame;
	public int defaultScale;
	public float timeout;
	public Rect exportRect;
	public SpriteRenderer gradientBackground;
	public SpriteRenderer imageBackground;
	public SpriteRenderer overlay;
	public Color overlayDark;
	public ParticleSystem particle;
	public Sprite pastelSprite;
	public Sprite gradientSprite;
	public Sprite ditheredGradientSprite;
	public Sprite[] imageBackgroundSprites;

#if UNITY_STANDALONE || UNITY_EDITOR

	private float startTime;
	private System.Random rand;

	private void Start()
	{
		string[] args = System.Environment.GetCommandLineArgs();
		if (!HasArg(args, "-export-artifact"))
		{
			DestroyImmediate(gameObject);
			return;
		}

		startTime = Time.realtimeSinceStartup;
		try
		{
			DoExport(args);
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
			Debug.Log(e.StackTrace);
			Exit(1);
		}
	}




	private void DoExport(string[] args)
	{
		Debug.Log("Initializing artifact exporter with the following args");
		foreach (var arg in args)
		{
			Debug.Log(arg);
		}
		ReadArgsAndExportArtifact(args);
	}

	private void ReadArgsAndExportArtifact(string[] args)
	{
		string traits = GetArgValue(args, "-traits").Replace('\'', '"');
		string name = GetArgValue(args, "-name");
		int frame = Mathf.Clamp(int.Parse(GetArgValue(args, "-frame", defaultFrame.ToString())), 0, AnimationTags.totalFrameCount);
		int scale = Mathf.Clamp(int.Parse(GetArgValue(args, "-scale", defaultScale.ToString())), 1, 10);
		int rarity = Mathf.Clamp(int.Parse(GetArgValue(args, "-rarity", "0")), (int)BackgroundRarity.Pastel, (int)BackgroundRarity.Animated);
		int token = int.Parse(GetArgValue(args, "-token", "0"));
		StartCoroutine(ExportArtifact(traits, frame, scale, name, (BackgroundRarity)rarity, token));
	}


	private IEnumerator ExportArtifact(string traits, int frame, int scale, string name, BackgroundRarity rarity, int token)
	{
		while (Time.realtimeSinceStartup - startTime < timeout && !CustomizationManager.IsInitialized())
		{
			yield return new WaitForEndOfFrame();
		}
		if (!CustomizationManager.IsInitialized())
		{
			throw new Exception($"CustomizationManager did not initialize in {timeout} seconds.");
		}
		Debug.Log($"Starting the rasterization at {Time.realtimeSinceStartup} seconds after startup");
		Debug.Log($"Frame: {frame}\nScale: {scale}\nName: {name}\nTraits: {traits}");

		SetBackground(rarity, token);
		CustomizationManager.I.SetFromTraitsString(traits);
		if (rarity == BackgroundRarity.Animated)
		{
			Application.targetFrameRate = 50;
			StartCoroutine(ExportAnimation(frame, 1, name));
		}
		else
		{
			ExportTexture(CustomizationManager.I.GenerateTexture2D(frame), scale, name);
			Exit(0);
		}

	}

	private IEnumerator ExportAnimation(int frame, int scale, string name)
	{
		const int framesToExport = 250;
		int frameNum = 0;
		while (frameNum++ < framesToExport)
		{
			yield return new WaitForFixedUpdate();
			ExportTexture(CustomizationManager.I.GenerateTexture2D(frame), scale, $"{name}\\{frameNum:00}");
		}
		yield return new WaitForFixedUpdate();
		Exit(0);
	}

	private void SetBackground(BackgroundRarity rarity, int token)
	{
		Vector4 hsva = gradientBackground.material.GetVector("_HSVAAdjust");
		ParticleSystemRenderer particleRenderer = particle.GetComponent<ParticleSystemRenderer>();
		Vector4 particleHsva = particleRenderer.material.GetVector("_HSVAAdjust");

		//rand = new System.Random((int)XUtils.TimestampMilliseconds());
		rand = new System.Random();

		switch (rarity)
		{
		case BackgroundRarity.Pastel:
			gradientBackground.sprite = pastelSprite;
			break;
		case BackgroundRarity.Gradient:
			gradientBackground.sprite = ditheredGradientSprite;
			break;
		case BackgroundRarity.ScreenGrab:
			imageBackground.sprite = XRandom.NextMember(imageBackgroundSprites);
			imageBackground.gameObject.SetActive(true);
			particle.gameObject.SetActive(false);
			gradientBackground.gameObject.SetActive(false);
			break;
		case BackgroundRarity.Animated:
			gradientBackground.sprite = gradientSprite;
			break;
		}

		overlay.color = rarity == BackgroundRarity.ScreenGrab ? overlayDark : overlay.color;

		if (rarity != BackgroundRarity.ScreenGrab)
		{
			gradientBackground.gameObject.SetActive(true);
			hsva.x = (float)rand.NextDouble();
			if (rarity == BackgroundRarity.Pastel)
			{
				hsva.y = (float)rand.NextDouble() * -0.25f;
			}
			else if (rarity == BackgroundRarity.Gradient)
			{
				hsva.y = (float)rand.NextDouble() * 0.3f - 0.15f;
			}
			gradientBackground.flipY = rand.NextDouble() > 0.5;
			gradientBackground.material.SetVector("_HSVAAdjust", hsva);

			if (rarity == BackgroundRarity.Animated)
			{
				particleHsva.x = hsva.x + (XRandom.NextBool() ? 0.15f : -0.15f);
				particleHsva.y = hsva.y;
				particleRenderer.material.SetVector("_HSVAAdjust", particleHsva);
				particle.gameObject.SetActive(true);
				particle.Play();
			}
			else
			{
				particle.gameObject.SetActive(false);
			}
		}
	}

	private void ExportTexture(Texture2D texture, int scale, string name)
	{
		Texture2D tex = ScaleTexture(CropTexture(texture), scale);
		string path = Path.Combine("output", name + ".png");
		string dirPath = new FileInfo(path).Directory.FullName;
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}
		File.WriteAllBytes(path, tex.EncodeToPNG());
		Debug.Log($"Artifact exported to {path}");
	}


	private Texture2D CropTexture(Texture2D texture)
	{

		Color[] cs = texture.GetPixels((int)exportRect.x, (int)exportRect.y, (int)exportRect.width, (int)exportRect.height);
		Texture2D cropped = new Texture2D((int)exportRect.width, (int)exportRect.height);
		cropped.filterMode = FilterMode.Point;
		cropped.SetPixels(cs);
		cropped.Apply();
		return cropped;
	}


	private Texture2D ScaleTexture(Texture2D texture2D, int scale)
	{
		int targetX = texture2D.width * scale;
		int targetY = texture2D.height * scale;
		RenderTexture rt = new RenderTexture(targetX, targetY, 24);
		RenderTexture.active = rt;
		Graphics.Blit(texture2D, rt);
		Texture2D result = new Texture2D(targetX, targetY);
		result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
		result.Apply();
		return result;
	}


	private bool HasArg(string[] args, string arg)
	{
		return args.FirstOrDefault(arg => arg == "-export-artifact") != null;
	}


	private string GetArgValue(string[] args, string arg, string defult = null)
	{
		int argValueIndex = Array.FindIndex(args, a => a == arg) + 1;
		return (argValueIndex > 0 && args.Length > argValueIndex) ? args[argValueIndex] : defult;
	}

	private void Exit(int exitCode)
	{
		Application.Quit(exitCode);
	}

	private enum BackgroundRarity
	{
		Pastel = 0,
		Gradient,
		ScreenGrab,
		Animated,
	}
#endif // #if UNITY_STANDALONE || UNITY_EDITOR
}
