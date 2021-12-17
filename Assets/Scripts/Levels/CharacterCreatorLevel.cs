using System;
using UnityEngine;
using UnityEngine.U2D;
using GraphQlClient.Core;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreatorLevel : MonoBehaviour
{
	public static CharacterCreatorLevel I;

	public GraphApi graphApi;
	public PixelPerfectCamera ppCamera;
	public float cameraOffset = -2.15f;
	public Vector2Int cameraResolution = new Vector2Int(280, 210);
	public Vector2 rotationRates;

	public float zapProbability;
	public float cableElectricityProbability;
	public SpriteRenderer randomizeButton;
	public SpriteRenderer leftMintButton;
	public SpriteRenderer poseRotationButton;
	public Sprite randomizeSprite;
	public Sprite randomizeSpriteDown;
	public Sprite mintSprite;
	public Sprite mintSpriteDown;
	public Sprite poseDownSprite;
	public Sprite rotateDownSprite;
	public AudioSource clickSfx;
	public GameObject leftOverlay;
	public Text mintButton;
	public Text mintLever;

	[Header("TV Terminal")]
	public Transform tv;
	public Text tvText;
	public float tvTypeSpeed;
	public float tvCursorFlashSpeed;
	public Vector3 tvPositions;
	public float tvTweenDuration;
	public TweenEaseType tvTweenEaseType;

	[Header("Terminal")]
	public SpriteRenderer terminalGlow;
	public float terminalFlickerDelay;
	public float terminalFlickerProbability;
	public Vector2 terminalFlickerRange;

	[Header("Animators")]
	public SimpleAnim topElectric;
	public SimpleAnim leftZap;
	public SimpleAnim rightZap;
	public SimpleAnim electricRing;
	public SimpleAnim glow;
	public SimpleAnim terminalElectric;
	public SimpleAnim satoshi;
	public SimpleAnim lever;

	[Header("Satoshi Animations")]
	public Sprite[] satoshiType;
	public Sprite[] satoshiGlasses;
	public Sprite[] satoshiLookup;
	public Sprite[] satoshiThink;
	public Sprite[] satoshiStandLeft;
	public Sprite[] satoshiStandRight;
	public Sprite[] satoshiWalk;
	public Sprite[] satoshiPullLever;
	public Sprite[] satoshiWalkBack;
	public Sprite[] satoshiWatchStart;
	public Sprite[] satoshiWatchEnd;
	public Sprite[] satoshiFootTap;
	public Sprite[] satoshiWatchTap;
	public Sprite[] satoshiWait;

	[Header("Lever Animations")]
	public Sprite[] leverWiggle;
	public Sprite[] leverPull;
	public Sprite[] leverActive;
	public Sprite[] leverIdle;
	public SpriteRenderer darkBackground;
	public Vector2 glowSpeed;
	public SimpleAnim mintLogo;
	public float mintLogoFlashTimeout;

	[Header("Tokens")]
	public Transform token;
	public Transform binanceToken;
	public Transform ethereumToken;
	public float tokenFloatRate;
	public float tokenFloatDomain;

	[Header("Clouds")]
	public Cloud[] clouds;
	public Vector2 cloudXRange;
	public Vector2 cloudYRange;
	public Vector2 cloudSpeedRange;


	private SatoshiState satoshiState = SatoshiState.LookUp;
	private float lastSatoshiStateChange = 0f;
	private float satoshiStateTimeout = 0f;
	private bool wasTypingBeforeGlasses = false;
	private bool minting;
	private string traits;

	private Vector3 tokenPosition;
	private bool isPosing = true;

#if WEBGL_MOBILE_BUILD
	private static bool isPortrait = true;
#else
	private static bool isPortrait = false;
#endif
	private bool animateMintLogo = false;

	private Tween<float> tvTween = null;
	private string tvTextToShow;
	private string tvTextCurrent;
	private float tvTextChangeTime = 0f;
	private float tvYTarget = 0f;
	private bool isMintable = true;
	private bool skipUniquenessCheck = true;

	private static bool colorSkipTipDisplayed = false;
	private static bool itemsOnlyInPoseTipDisplayed = false;


#if !UNITY_STANDALONE || UNITY_EDITOR
	private void Awake()
	{
		I = this;
		SetPortrait(isPortrait);
#if WEBGL_MOBILE_BUILD
		poseRotationButton.gameObject.SetActive(false);
#endif
	}

	private void Start()
	{
		ResetScene();
		binanceToken.gameObject.SetActive(false);
		ethereumToken.gameObject.SetActive(true);
		InvokeRepeating(nameof(UpdateSatoshi), 0f, 0.1f);
		InvokeRepeating(nameof(UpdateTerminalGlow), 0f, terminalFlickerDelay);
		InvokeRepeating(nameof(UpdateMintLogo), 3f, mintLogoFlashTimeout);
		CustomizationManager.onInitialized += OnCustomizationInitialized;
		CustomizationManager.onSubmitTraits += OnSubmitTraits;
		CustomizationManager.onOptionBurst += OnOptionBurst;
		tokenPosition = token.localPosition;

		for (int i = 0; i < clouds.Length; i++)
		{
			Cloud cloud = clouds[i];
			cloud.sr.transform.localPosition = new Vector3(XRandom.NextFloat(cloudXRange), XRandom.NextFloat(cloudYRange), cloud.sr.transform.localPosition.z);
			cloud.speed = XRandom.NextFloat(cloudSpeedRange);
			cloud.sr.flipX = XRandom.NextBool();
		}
		tv.position = new Vector3(tv.position.x, tvPositions.z, tv.position.z);
		mintLever.raycastTarget = true;
		mintButton.raycastTarget = true;
	}

	private void OnOptionBurst()
	{
		if (!colorSkipTipDisplayed)
		{
			colorSkipTipDisplayed = true;
			int numColorSkipTipDisplay = PlayerPrefs.GetInt("NumColorSkipTipDisplay", 0);
			if (numColorSkipTipDisplay < 4)
			{
				DisplayTvTip("TO SKIP COLOR\nVARIATIONS, TAP ON\nTHE TRAIT TEXT", 15f);
				PlayerPrefs.SetInt("NumColorSkipTipDisplay", numColorSkipTipDisplay + 1);
			}
		}
	}

	private void OnSubmitTraits(string traits)
	{
		if (isMintable)
		{
			minting = true;
			this.traits = traits;
			CustomizationLauncher.SetBlockInput(true);
			if (!isPortrait)
			{
				satoshiState = SatoshiState.WalkRight;
				satoshi.Play(satoshiWalk, true);
				lever.Play(leverActive, true);
			}
			else
			{
				StartMintEffects();
				SubmitTraits();
			}
		}
		else
		{
			DisplayDuplicatedTip();
		}
		clickSfx.Play();
	}

	private void SetMintable(bool mintable)
	{
		if (mintable == isMintable && mintable == mintLever.raycastTarget)
		{
			return;
		}
		isMintable = mintable;

		if (mintable)
		{
			lever.Play(leverWiggle, false);
			CancelInvoke(nameof(DisplayDuplicatedTip));
			HideTv();
		}
		else
		{
			lever.Play(leverIdle, false);
			if (!skipUniquenessCheck)
			{
				Invoke(nameof(DisplayDuplicatedTip), 1f);
			}
			skipUniquenessCheck = false;
		}

		if (animateMintLogo != mintable)
		{
			animateMintLogo = mintable;
			UpdateMintLogo();
		}
	}

	private void SubmitTraits()
	{
		CustomizationLauncher.SubmitTraits(traits);
	}

	public static void EndMintEffects()
	{
		I.ResetScene();
		I.OnCustomizationInitialized();
		CustomizationManager.I.rotationRate = I.rotationRates.x;
	}

	private void ResetScene()
	{
		topElectric.Disable();
		topElectric.playOnce = true;

		leftZap.Disable();
		leftZap.playOnce = true;

		rightZap.Disable();
		rightZap.playOnce = true;

		electricRing.Disable();
		electricRing.gameObject.SetActive(false);

		glow.enabled = false;
		glow.animSpeed = glowSpeed.x;

		terminalElectric.enabled = false;
		//darkBackground.enabled = false;

		lever.Play(leverIdle, true);

		minting = false;

		satoshi.Play(satoshiType, false);
		satoshiState = SatoshiState.Type;

		randomizeButton.sprite = randomizeSprite;

		JSWrapper.DispatchEvent("OnMintEffectToggle", "false");
	}

	private void StartMintEffects()
	{
		return;
		electricRing.gameObject.SetActive(true);
		electricRing.Play();
		topElectric.playOnce = false;
		topElectric.Play();

		leftZap.playOnce = false;
		leftZap.Play();

		rightZap.playOnce = false;
		rightZap.Play();

		glow.animSpeed = glowSpeed.y;
		darkBackground.enabled = true;

		CustomizationManager.I.rotationRate = I.rotationRates.y;
		JSWrapper.DispatchEvent("OnMintEffectToggle", "true");
	}

	public static void SetUniqueness(bool isUnique)
	{
		I.SetMintable(isUnique);
	}

	private void DisplayDuplicatedTip()
	{
		DisplayTvTip("This combination\nhas already been\nminted", -1f);
	}

	public static void DisplayTvTip(string msg, float timeout)
	{
		I.DisplayTv(msg, I.tvPositions.y, timeout);
	}

	public static void DisplayTvOverlay(string msg, float timeout)
	{
		I.DisplayTv(msg, I.tvPositions.x, timeout);
	}
	private void DisplayTv(string msg, float y, float timeout)
	{
		CancelInvoke(nameof(DoHideTv));
		tvTextToShow = msg.Trim().ToUpper();
		tvTextCurrent = "";
		tvText.text = "";
		if (tvYTarget != y)
		{
			tvYTarget = y;
			tvTween = new Tween<float>(tv.position.y, y, tvTweenDuration, tvTweenEaseType);
			if (timeout > 0f)
			{
				Invoke(nameof(DoHideTv), 15f);
			}
		}
	}

	private void DoHideTv()
	{
		tvTween = new Tween<float>(tv.position.y, tvPositions.z, tvTweenDuration, tvTweenEaseType);
		tvYTarget = tvPositions.z;
	}

	public static void HideTv()
	{
		I.DoHideTv();
	}

	public static bool IsMinting()
	{
		return I.minting;
	}

	private void Update()
	{
		token.localPosition = tokenPosition + new Vector3(0f, (Mathf.Sin(Time.time * tokenFloatRate) * tokenFloatDomain) + (Mathf.Sin(Time.time * tokenFloatRate * 0.65f) * 0.8f * tokenFloatDomain), 0f);
		UpdateClouds();
		UpdateTV();

		if (Input.GetKeyDown(KeyCode.Backslash))
		{
			ScreenCapture.CaptureScreenshot($"Screenshots/{SceneManager.GetActiveScene().name}-{XUtils.Timestamp()}.png");
		}
	}

	private void UpdateTV()
	{
		if (tvTween != null && !tvTween.IsEnded())
		{
			float y = tvTween.Update(Time.deltaTime);
			tv.position = new Vector3(tv.position.x, y, tv.position.z);
		}
		else if (tvTextToShow != tvTextCurrent && Time.time - tvTextChangeTime > tvTypeSpeed)
		{
			tvTextChangeTime = Time.time;
			tvTextCurrent += tvTextToShow[tvTextCurrent.Length];
			tvText.text = tvTextCurrent;
		}
		else if (tv.position.y < tvPositions.z)
		{
			tvText.text = tvTextCurrent + (Time.time % (tvCursorFlashSpeed * 2f) > tvCursorFlashSpeed ? " _" : "");
		}
	}

	private void UpdateClouds()
	{
		for (int i = 0; i < clouds.Length; i++)
		{
			Cloud cloud = clouds[i];
			cloud.Move();
			if (cloud.sr.transform.localPosition.x > cloudXRange.y)
			{
				cloud.sr.transform.localPosition = new Vector3(cloudXRange.x, XRandom.NextFloat(cloudYRange), cloud.sr.transform.localPosition.z);
				cloud.speed = XRandom.NextFloat(cloudSpeedRange);
				cloud.sr.flipX = XRandom.NextBool();
			}
		}
	}

	private void UpdateMintLogo()
	{
		if (!minting && !mintLogo.enabled && animateMintLogo)
		{
			mintLogo.Play();
		}
	}

	public void OnCustomizationInitialized()
	{
		glow.enabled = true;
		CancelInvoke(nameof(UpdateElectricity));
		InvokeRepeating(nameof(UpdateElectricity), 0f, 0.1f);
		CustomizationManager.onChange -= OnCustomizationChange;
		CustomizationManager.onChange += OnCustomizationChange;
	}


	private void UpdateElectricity()
	{
		PlayAnimationWithProbability(topElectric, cableElectricityProbability);
		PlayAnimationWithProbability(leftZap, zapProbability);
		PlayAnimationWithProbability(rightZap, zapProbability);
	}

	private void PlayAnimationWithProbability(SimpleAnim anim, float prob)
	{
		if (!anim.enabled && XRandom.NextFloat() < prob)
		{
			anim.Play();
		}
	}

	private void OnCustomizationChange()
	{
		if (satoshiState != SatoshiState.Glasses && !minting)
		{
			if (satoshiState != SatoshiState.Type)
			{
				lastSatoshiStateChange = Time.time;
				satoshiState = SatoshiState.Type;
			}
			satoshi.Play(satoshiType, false);
			satoshiStateTimeout = XRandom.NextFloat(2f, 3f);
			if (!lever.enabled)
			{
				lever.Play(leverWiggle, false);
			}
		}
		// Doit();
	}

	private async void Doit()
	{
		GraphApi.Query getCharacters = graphApi.GetQueryByName("GetCharacters", GraphApi.Query.Type.Query);
		getCharacters.SetArgs(new { id = "0x6ac131d20eec5d396030cb6d8b3fd34476e74c78" });
		UnityWebRequest request = await graphApi.Post(getCharacters);
		if (request != null)
		{
			print(request.downloadHandler.text);
		}
	}

	private void UpdateTerminalGlow()
	{
		if (XRandom.NextFloat() < terminalFlickerProbability && satoshiState == SatoshiState.Type)
		{
			Color glowColor = terminalGlow.color;
			glowColor.a = XRandom.NextFloat(terminalFlickerRange);
			terminalGlow.color = glowColor;
		}
	}

	private void UpdateSatoshi()
	{
		switch (satoshiState)
		{
		case SatoshiState.LookUp:
			if (Time.time - lastSatoshiStateChange > satoshiStateTimeout)
			{
				if (XRandom.NextFloat() > 0.65f)
				{
					satoshiState = SatoshiState.Glasses;
					lastSatoshiStateChange = Time.time;
					satoshi.Play(satoshiGlasses, true);
					wasTypingBeforeGlasses = false;
				}
				else
				{
					satoshiState = SatoshiState.Think;
					lastSatoshiStateChange = Time.time;
					satoshi.Play(satoshiThink, true);
					satoshiStateTimeout = XRandom.NextFloat(5f, 10f);
				}
			}
			break;
		case SatoshiState.Type:
			if (CustomizationManager.IsInitialized() && Time.time - lastSatoshiStateChange > satoshiStateTimeout)
			{
				satoshiState = SatoshiState.LookUp;
				lastSatoshiStateChange = Time.time;
				satoshi.Play(satoshiLookup, false);
				satoshiStateTimeout = XRandom.NextFloat(3f, 10f);
			}
			else if (XRandom.NextFloat() < 0.025f && Time.time - lastSatoshiStateChange > satoshiStateTimeout / 2f)
			{
				satoshiState = SatoshiState.Glasses;
				satoshi.Play(satoshiGlasses, true);
				wasTypingBeforeGlasses = true;
			}
			if (!terminalElectric.enabled && XRandom.NextFloat() < 0.07f)
			{
				terminalElectric.Play();
			}
			break;
		case SatoshiState.Glasses:
			if (!satoshi.enabled)
			{
				if (wasTypingBeforeGlasses)
				{
					satoshiState = SatoshiState.Type;
					satoshi.Play(satoshiType, false);
					satoshiStateTimeout = XRandom.NextFloat(1f, 2f);
				}
				else
				{
					satoshiState = SatoshiState.LookUp;
					lastSatoshiStateChange = Time.time;
					satoshi.Play(satoshiLookup, false);
					satoshiStateTimeout = XRandom.NextFloat(3f, 10f);
				}
			}
			break;
		case SatoshiState.Think:
			if (Time.time - lastSatoshiStateChange > satoshiStateTimeout)
			{
				float rand = XRandom.NextFloat();
				if (rand < 0.1f)
				{
					satoshiState = SatoshiState.Stand;
					lastSatoshiStateChange = Time.time;
					satoshi.Play(satoshiStandLeft, false);
					satoshiStateTimeout = XRandom.NextFloat(5f, 8f);
				}
			}
			break;
		case SatoshiState.Stand:
			if (Time.time - lastSatoshiStateChange > satoshiStateTimeout)
			{
				satoshiState = SatoshiState.WatchStart;
				lastSatoshiStateChange = Time.time;
				satoshi.Play(satoshiWatchStart, true);
			}
			break;
		case SatoshiState.WatchStart:
			if (!satoshi.enabled)
			{
				satoshiState = SatoshiState.WatchFootTap;
				lastSatoshiStateChange = Time.time;
				satoshi.Play(satoshiFootTap, false);
				satoshiStateTimeout = XRandom.NextFloat(3f, 5f);
			}
			break;
		case SatoshiState.WatchFootTap:
			if (Time.time - lastSatoshiStateChange > satoshiStateTimeout)
			{
				satoshiState = SatoshiState.WatchTap;
				lastSatoshiStateChange = Time.time;
				satoshi.Play(satoshiWatchTap, false);
				satoshiStateTimeout = XRandom.NextFloat(0.5f, 1.5f);
			}
			break;
		case SatoshiState.WatchTap:
			if (Time.time - lastSatoshiStateChange > satoshiStateTimeout)
			{
				satoshiState = SatoshiState.WatchEnd;
				lastSatoshiStateChange = Time.time;
				satoshi.Play(satoshiWatchEnd, true);
			}
			break;
		case SatoshiState.WatchEnd:
			if (!satoshi.enabled)
			{
				satoshiState = SatoshiState.LookUp;
				lastSatoshiStateChange = Time.time;
				satoshi.Play(satoshiStandLeft, false);
				satoshiStateTimeout = XRandom.NextFloat(5f, 10f);
			}
			break;
		case SatoshiState.WalkRight:
			if (!satoshi.enabled)
			{
				satoshiState = SatoshiState.PullLever;
				satoshi.Play(satoshiPullLever, true);
				lever.Play(leverPull, true);
			}
			break;
		case SatoshiState.PullLever:
			if (!satoshi.enabled)
			{
				StartMintEffects();
				SubmitTraits();
				satoshi.Play(satoshiWalkBack, true);
				satoshiState = SatoshiState.WalkBack;
				animateMintLogo = false;
			}
			break;
		case SatoshiState.WalkBack:
			if (!satoshi.enabled)
			{
				satoshi.Play(satoshiWait, false);
			}
			break;
		}
	}

	public static void SetPoseRotate(bool isPosing)
	{
#if WEBGL_MOBILE_BUILD
		isPosing = true;
#endif
		if (I)
		{
			I.isPosing = isPosing;
			I.poseRotationButton.sprite = isPosing ? I.poseDownSprite : I.rotateDownSprite;
			if (!isPosing && !I.minting && !itemsOnlyInPoseTipDisplayed &&
				(CustomizationManager.TraitCategorySelected(CustomizationManager.Names.LeftItem) ||
				CustomizationManager.TraitCategorySelected(CustomizationManager.Names.RightItem))
			)
			{
				itemsOnlyInPoseTipDisplayed = true;
				int numItemOnlyInPoseDisplayed = PlayerPrefs.GetInt("NumItemOnlyInPoseDisplayed", 0);
				if (numItemOnlyInPoseDisplayed < 4)
				{
					DisplayTvTip("Handheld items are\nonly displayed in\n\"pose\" mode", 15f);
					PlayerPrefs.SetInt("NumItemOnlyInPoseDisplayed", numItemOnlyInPoseDisplayed + 1);
				}
			}
		}
	}

	public static void SetPortrait(bool isPortrait)
	{
		CharacterCreatorLevel.isPortrait = isPortrait;
		I.ppCamera.transform.position = new Vector3(isPortrait ? I.cameraOffset : 0f, I.ppCamera.transform.position.y, I.ppCamera.transform.position.z);
		I.ppCamera.refResolutionX = isPortrait ? I.cameraResolution.y : I.cameraResolution.x;
		I.leftOverlay.SetActive(false);
	}

	public void UI_RandomizePressed()
	{
		clickSfx.Play();
		randomizeButton.sprite = randomizeSpriteDown;
	}

	public void UI_RandomizeReleased()
	{
		randomizeButton.sprite = randomizeSprite;
		CustomizationManager.RandomizeTraits();
	}

	public void UI_TogglePoseRotate()
	{
		SetPoseRotate(!isPosing);
		clickSfx.Play();
		CustomizationManager.SetActionState(isPosing ? 0 : -1, false);
	}

	public void UI_LeftMintPressed()
	{
		if (isPortrait)
		{
			clickSfx.Play();
			leftMintButton.sprite = mintSpriteDown;
		}
	}

	public void UI_LeftMintReleased()
	{
		if (isPortrait)
		{
			leftMintButton.sprite = mintSprite;
			CustomizationManager.I.UI_SubmitTraitMap();
		}
	}

	public void UI_ResetPressed()
	{
		clickSfx.Play();
		CustomizationManager.ResetCharacter();
	}

	public void UI_SetPortrait(string isPortrait)
	{
		Debug.Log($"Setting the portrait mode to ({isPortrait})");

		isPortrait = isPortrait.ToLower();
		SetPortrait(isPortrait == "true");

		Debug.Log($"Portrait mode set to ({isPortrait})");
	}

	public void UI_TvTapped()
	{
		clickSfx.Play();
		if ((tvTween == null || tvTween.IsEnded()) && tv.position.y > (tvPositions.x + 0.1f))
		{
			CancelInvoke(nameof(HideTv));
			HideTv();
		}
	}

#endif //#if !UNITY_STANDALONE || UNITY_EDITOR

	[Serializable]
	public class Cloud
	{
		public SpriteRenderer sr;
		public float speed;


		public void Move()
		{
			sr.transform.localPosition += Vector3.right * speed * Time.deltaTime;
		}
	}

	enum SatoshiState
	{
		LookUp,
		Type,
		Glasses,
		Think,
		Stand,
		WatchStart,
		WatchFootTap,
		WatchTap,
		WatchEnd,
		WalkRight,
		PullLever,
		WalkBack,
		Waiting,
	}
}
