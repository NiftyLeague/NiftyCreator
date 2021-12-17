using UnityEngine;

public class SimpleAnim : MonoBehaviour
{
	public float animSpeed;
	public Sprite[] frames;

	public bool playOnce;
	public bool disableAfterPlayOnce = false;
	public bool stayAfterPlayOnce = false;
	public bool pingPong;

	int frame = -1;
	float counter;
	private SpriteRenderer sr;
	private bool ping = true;


	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	public void Play()
	{
		enabled = true;
		counter = 0f;
		frame = -1;
		ping = true;
	}


	public void Disable()
	{
		enabled = false;
		counter = 0f;
		frame = -1;
		ping = true;
		if (sr == null)
		{
			sr = GetComponent<SpriteRenderer>();
		}
		sr.sprite = null;
	}

	public void Play(Sprite[] frames, bool playOnce)
	{
		this.frames = frames;
		this.playOnce = playOnce;
		Play();
	}

	void FixedUpdate()
	{
		counter += Time.deltaTime;
		if (counter > animSpeed)
		{
			counter -= animSpeed;
			frame += ping ? 1 : -1;

			if (pingPong)
			{
				if (ping && frame >= frames.Length)
				{
					ping = false;
					frame = frames.Length - 1;
				}
				else if (!ping && frame <= 0)
				{
					ping = true;
					frame = 0;
				}
			}

			sr.sprite = frames[frame % frames.Length];
			if (playOnce && frame >= frames.Length)
			{
				if (disableAfterPlayOnce)
				{
					enabled = false;
					sr.sprite = stayAfterPlayOnce ? frames[frames.Length - 1] : null;
				}
				else
				{
					Destroy(gameObject);
				}
			}
		}
	}
}
