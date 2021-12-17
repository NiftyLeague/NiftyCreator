using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SpriteRasterizer : MonoBehaviour
{
	private static SpriteRasterizer I;

	public CharacterCustomizer cc;
	public Camera renderCamera;
	public bool Busy { get { return jobs.Count != 0; } }

	private static Dictionary<string, List<Sprite>> spriteCache = new Dictionary<string, List<Sprite>>();
	private static string cachePath;
	private static Queue<RasterizationJob> jobs = new Queue<RasterizationJob>();
	private static int width, height;
	private static int batchSize = 10;


	[Serializable]
	public class Sprites
	{
		public string hash;
		public Sprite[] sprites;

		public Sprites(string hash, Sprite[] sprites)
		{
			this.hash = hash;
			this.sprites = sprites;
		}
	}

	private void Awake()
	{
		I = this;
		width = renderCamera.targetTexture.width;
		height = renderCamera.targetTexture.height;
		cachePath = Path.Combine(Application.persistentDataPath, "cache");
	}

	private void Update()
	{
		if (jobs.Count > 0 && jobs.Peek().state == RasterizationJob.State.Created)
		{
			jobs.Peek().Start();
		}

		if (renderCamera.enabled && jobs.Count == 0)
		{
			renderCamera.enabled = false;
		}
	}

	public Texture2D CaptureTexture2D(bool cleanPalletePixels)
	{
		Rect rect = new Rect(0, 0, width, height);
		var currentRT = RenderTexture.active;
		RenderTexture.active = renderCamera.targetTexture;
		renderCamera.Render();
		Texture2D image = new Texture2D(width, height);
		image.filterMode = FilterMode.Point;
		image.ReadPixels(rect, 0, 0);
		image.Apply();
		RenderTexture.active = currentRT;
		return cleanPalletePixels ? CleanupPalette(image) : image;
	}

	public Sprite CreateSprite(Texture2D texture, string name)
	{
		Rect rect = new Rect(0, 0, texture.width, texture.height);
		Sprite s = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 16);
		s.name = name;
		return s;
	}

	public Texture2D RasterizeFrame(int frame)
	{
		cc.SetFrameAbsolute(frame);
		return CaptureTexture2D(false);
	}

	public void RasterizeAllFrames(Dictionary<string, Trait> traits, string traitHash, Action<List<Sprite>, string> onRasterizationComplete)
	{
		if (spriteCache.ContainsKey(traitHash))
		{
			print("using memory cache for " + traitHash);
			onRasterizationComplete(spriteCache[traitHash], traitHash);
			return;
		}
		renderCamera.enabled = true;
#if UNITY_WEBGL && !UNITY_EDITOR
		bool useCache = false;
#else
		bool useCache = false;
#endif
		jobs.Enqueue(new RasterizationJob(traits, traitHash, OnRasterizationJobComplete, onRasterizationComplete, useCache, false));
	}

	private void OnRasterizationJobComplete(RasterizationJob job)
	{
		jobs.Dequeue();
		if (!spriteCache.ContainsKey(job.hash))
		{
			spriteCache.Add(job.hash, job.result);
		}
		job.callback(job.result, job.hash);

	}

	private T[] SubArray<T>(T[] data, int index, int length)
	{
		T[] result = new T[length];
		Array.Copy(data, index, result, 0, length);
		return result;
	}

	private Texture2D CleanupPalette(Texture2D texture)
	{
		int w = texture.width;
		int h = texture.height;
		Color32[] pix = texture.GetPixels32();

		int i = h - 1;
		int j = 0;
		bool dirty = false;

		while (pix[i * w + j].a > 0f)
		{
			while (pix[i * w + j].a > 0f)
			{
				texture.SetPixel(j, i, Color.clear);
				j++;
				dirty = true;
			}
			i--;
			j = 0;
		}
		if (dirty)
		{
			texture.Apply();
		}
		return texture;
	}


	private class RasterizationJob
	{
		Dictionary<string, Trait> traits;
		public string hash;
		public string fileName;
		public string error;
		public State state;
		private bool useCache;
		private bool savePng;
		public List<Sprite> result;
		public Action<List<Sprite>, string> callback;

		private Action<RasterizationJob> onComplete;
		private MemoryStream ms = new MemoryStream();

		public RasterizationJob(Dictionary<string, Trait> traits, string hash, Action<RasterizationJob> onComplete, Action<List<Sprite>, string> callback, bool useCache, bool savePng)
		{
			this.traits = traits;
			this.hash = hash;
			this.fileName = $"{Accessories.version}{hash}";
			this.onComplete = onComplete;
			this.callback = callback;
			this.useCache = useCache;
			this.savePng = savePng;
			state = State.Created;
			error = null;
			result = new List<Sprite>();
		}

		public void Start()
		{
			if (spriteCache.ContainsKey(hash))
			{
				print("using memory cache for " + hash);
				result = spriteCache[hash];
				Finish();
			}
			else
			{
				I.StartCoroutine(Rasterize());
			}
		}

		public void Finish()
		{
			bool success = string.IsNullOrEmpty(error) && result.Count == AnimationTags.totalFrameCount;
			state = success ? State.Succeeded : State.Failed;
			onComplete(this);
		}

		private IEnumerator Rasterize()
		{
			if (state != State.Created)
			{
				error = $"Rasterize called on Job in '{state}' state";
				state = State.Failed;
				Finish();
				yield break;
			}
			state = State.InProgress;
			if (useCache)
			{
				yield return I.StartCoroutine(LoadFromCache());
				if (result.Count == AnimationTags.totalFrameCount)
				{
					print("Used file cache for " + hash);
					Finish();
					yield break;
				}
				ms.Write(BitConverter.GetBytes(AnimationTags.totalFrameCount), 0, 4);
			}
			result = new List<Sprite>();
			CustomizationManager.I.SetFromTraitsForRasterization(traits);
			for (int i = 0; i < AnimationTags.totalFrameCount; i++)
			{
				RasterizeFrame(i);
				if (i % batchSize == 0)
				{
					yield return new WaitForEndOfFrame();
				}
			}
			Finish();
			if (useCache)
			{
				try
				{
					string path = Path.Combine(cachePath, fileName);
					Task.Run(() =>
					{
						XUtils.SaveBytes(ms.ToArray(), path, true);
					});
					ms.Close();
				}
				catch (Exception e)
				{
					Debug.LogWarning("Exception while writing cache " + e.Message);
				}
			}
		}

		private void RasterizeFrame(int frame)
		{
			I.cc.SetFrameAbsolute(frame);
			Texture2D tex = I.CaptureTexture2D(true);
			result.Add(I.CreateSprite(tex, $"{frame:000}_{hash}"));
			if (useCache)
			{
				byte[] raw = tex.GetRawTextureData();
				ms.Write(BitConverter.GetBytes(raw.Length), 0, 4);
				ms.Write(raw, 0, raw.Length);
			}
			else if (savePng)
			{
				SavePng(tex, $"{frame:0000}.png");
			}
		}


		private IEnumerator LoadFromCache()
		{
			byte[] bytes = null;
			try
			{
				string path = Path.Combine(cachePath, fileName);
				bytes = XUtils.LoadBytes(path, true);
			}
			catch (Exception)
			{
				Directory.CreateDirectory(cachePath);
			}

			if (bytes == null || bytes.Length == 0)
			{
				yield break;
			}

			yield return I.StartCoroutine(TexturesFromCachedBytes(bytes));
		}


		private IEnumerator TexturesFromCachedBytes(byte[] bytes)
		{
			int index = 0;
			int numFrames = 0;
			try
			{
				numFrames = BitConverter.ToInt32(bytes, index);
			}
			catch { yield break; }

			if (numFrames != AnimationTags.totalFrameCount) { yield break; }

			index += 4;
			for (int i = 0; i < numFrames; i++)
			{
				try
				{
					int frameSize = BitConverter.ToInt32(bytes, index);
					index += 4;
					Texture2D tex = new Texture2D(width, height);
					tex.filterMode = FilterMode.Point;
					tex.LoadRawTextureData(I.SubArray(bytes, index, frameSize));
					index += frameSize;
					result.Add(I.CreateSprite(tex, $"{i:000}_{hash}"));
				}
				catch { yield break; }
				if (i % batchSize == 0)
				{
					yield return new WaitForEndOfFrame();
				}
			}
		}


		private void SavePng(Texture2D texture, string pngName)
		{
			string path = Path.Combine(Application.persistentDataPath, fileName);
			Directory.CreateDirectory(path);
			File.WriteAllBytes(Path.Combine(path, pngName), texture.EncodeToPNG());
		}


		public enum State
		{
			Created,
			InProgress,
			Succeeded,
			Failed
		}
	}
}
