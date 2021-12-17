using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
	enum AnimState
	{
		Idle,
		Running,
		Jumping,
		Skidding,
		BouncedInAir,
		ChargeAttack,
		Attacking,
		AttackRecover,
		WallSlide,
		Tongue,
		Throw,
		Teleport,
		SpinDash,
		Scratch,
	}

	enum AttackDirection
	{
		Forward,
		Up,
		DiagonalUp,
		Down,
		DownForward
	}

	AttackDirection DetermineDirection(Vector2 dir)
	{
		if (dir.y == 1f && dir.x == 0f)
		{
			return AttackDirection.Up;
		}
		else if (dir.y > 0.45f && Mathf.Abs(dir.x) > 0.45f)
		{
			return AttackDirection.DiagonalUp;
		}
		else if (dir.y < -0.45f && Mathf.Abs(dir.x) > 0.45f)
		{
			return AttackDirection.DownForward;
		}
		else if (dir.y == -1f)
		{
			return AttackDirection.Down;
		}
		else
		{
			return AttackDirection.Forward;
		}
	}

	//Vector3 defaultOffset;
	//Vector3 jumpFromPosition;
	Vector3 spriteDefaultOffset;

	public Character character;
	public Color primaryColor { get { return character.player.Color; } }

	public SpriteRenderer rend;
	public LineRenderer tongueLine;
	public GameObject spawnLoadEffect;

	public SpriteRenderer tongueTip;
	float particleCounter;
	float lastSkidXPos;
	public float skidEffectDistance;
	public AudioClip[] flight;

	public AudioSource flightAudioSource;
	public float flightHeightPitchMod;
	public bool modFlightVolume;
	public float flightVelocityVolumeMod;

	public float smokeRingDistance;
	public float lineVelocityScale;
	Vector2 lastSmokeRingPos;

	int frame;
	float frameCounter;

	AnimState animState;

	float trailCounter;
	float trailFaderCounter;
	public float trailDelay;

	int variationRandomizer;

	float transitionTime;

	int lastDirection = 0;
	float lastDirectionChangeTime = 0f;
	float lastLastDirectionChangeTime = 0f;
	float idleTime = 0f;


	float t
	{
		get
		{
			return character.t;
		}
	}


	void Start()
	{
		spriteDefaultOffset = rend.transform.localPosition;
		rend.flipX = character.FacingDirection < 0;
		spawnLoadEffect.SetActive(GetFrameAbsolute(0) == null);
	}

	void LateUpdate()
	{
		UpdateSpawnEffect();
		var newAnimState = DetermineAnimState();
		rend.transform.localRotation = Quaternion.identity;
		rend.transform.localPosition = spriteDefaultOffset;
		//rend.transform.localScale = new Vector3(character.FacingDirection, 1f, 1f);
		if (character.FacingDirection != lastDirection)
		{
			lastDirection = character.FacingDirection;
			lastLastDirectionChangeTime = lastDirectionChangeTime;
			lastDirectionChangeTime = Time.time;
			rend.flipX = character.FacingDirection < 0;
		}


		if (animState != newAnimState)
		{
			frame = 0;
			frameCounter = 0f;

			if (animState == AnimState.Skidding)
			{
				transitionTime = 0.05f;
				SetFrame(AnimationTagType.SkidRecover);
			}
			else if (newAnimState == AnimState.Jumping && animState == AnimState.WallSlide)
			{
				transitionTime = 0.05f;
				SetFrame(AnimationTagType.WallslideJump);
			}
			variationRandomizer = Random.Range(0, 10);

			if (newAnimState != AnimState.Tongue)
			{
				tongueLine.enabled = false;
				tongueTip.gameObject.SetActive(false);
			}
			if (newAnimState == AnimState.Idle && animState != AnimState.Idle)
			{
				idleTime = 0f;
			}
		}

		animState = newAnimState;

		if (transitionTime > 0f)
		{
			if (animState != AnimState.BouncedInAir)
			{
				transitionTime -= t;
				return;
			}
			else transitionTime = 0f;
		}


		switch (animState)
		{
		case AnimState.Idle:
			AnimateIdle();
			break;
		case AnimState.Running:
			AnimateRun();
			break;
		case AnimState.Jumping:
			AnimateJumping();
			break;
		case AnimState.Skidding:
			AnimateSkid();
			break;
		case AnimState.BouncedInAir:
			AnimateBouncedFlying();
			break;
		case AnimState.ChargeAttack:
			AnimateChargeAttack();
			break;
		case AnimState.Attacking:
			AnimateAttack();
			break;
		case AnimState.AttackRecover:
			AnimateAttackRecover();
			break;
		case AnimState.WallSlide:
			AnimateWallSlide();
			break;
		case AnimState.Tongue:
			AnimateTongue();
			break;
		case AnimState.Throw:
			AnimateThrow();
			break;
		case AnimState.Teleport:
			AnimateTeleport();
			break;
		case AnimState.SpinDash:
			AnimateSpinDash();
			break;
		case AnimState.Scratch:
			AnimateScratch();
			break;
		default:
			break;
		}

		if (character.IngestedFly && (character.state != CharacterState.Special || character.tongueState != TongueState.HitFlyBurping))
		{
			RunTrailSilhouettePoweredUp();
		}

		RunFlightAudio();
	}

	private void UpdateSpawnEffect()
	{
		if (spawnLoadEffect.activeSelf && GetFrameAbsolute(0) != null)
		{
			spawnLoadEffect.SetActive(false);
		}
		else if (!spawnLoadEffect.activeSelf && GetFrameAbsolute(0) == null)
		{
			spawnLoadEffect.SetActive(true);
		}
	}

	void AnimateRun()
	{
		if (frame > 8 && Time.time - lastDirectionChangeTime < 0.05f && lastDirectionChangeTime - lastLastDirectionChangeTime > 0.25f)
		{
			SetFrame(AnimationTagType.RunDirChange, 0);
		}
		else if (frame > 8 && Time.time - lastDirectionChangeTime < 0.1f && lastDirectionChangeTime - lastLastDirectionChangeTime > 0.25f)
		{
			SetFrame(AnimationTagType.RunDirChange, 1);
		}
		else
		{
			int frameBefore = frame;
			RunAnimation(AnimationTagType.Run, 0.03f);
			if (frame != frameBefore && frame % 4 == 1)
			{
				SoundController.PlaySoundEffect("Footstep", 0.1f, transform.position);
				EffectsController.CreateDustPuff(transform.position, character.FacingDirection);
			}
		}
	}


	void AnimateIdle()
	{
		frameCounter += t;
		idleTime += t;
		if (GameController.HasInstance && GameController.State == GameState.RoundFinished && character.IsWinningPlayer())
		{
			if (frameCounter < 0.3f)
			{
				SetFrame(AnimationTagType.StandRight, 0);
			}
			else if (frameCounter < 0.4f)
			{
				SetFrame(AnimationTagType.WinTransition, 0);
			}
			else if (frameCounter < 0.5f)
			{
				SetFrame(AnimationTagType.WinTransition, 1);
			}
			else
			{
				if (frameCounter % 2f < 0.9f)
				{
					SetFrame(AnimationTagType.Win, 2);
				}
				else if (frameCounter % 2f < 1f)
				{
					SetFrame(AnimationTagType.Win, 1);
				}
				else if (frameCounter % 2f < 1.9f)
				{
					SetFrame(AnimationTagType.Win, 0);
				}
				else
				{
					SetFrame(AnimationTagType.Win, 1);
				}
			}
		}
		else if (!character.IsActive)
		{
			SetFrame(AnimationTagType.StandRight, 0);
			idleTime = 0f;
			frameCounter = 0f;
		}
		else
		{
			switch (character.type)
			{
			case CharacterType.Alien:
				if (idleTime < 3f)
				{
					RunAnimation(AnimationTagType.StandRight, 0.05f);
				}
				else if (idleTime < 3.1f)
				{
					RunAnimation(AnimationTagType.AlienTeaStart, 0.1f);
				}
				else if (idleTime % 10f < 6f)
				{
					RunAnimation(AnimationTagType.AlienTea, 0.25f);
				}
				else if (idleTime % 10f < 7.5f)
				{
					RunAnimation(AnimationTagType.AlienTeaTurned, 0.25f);
				}
				else if (idleTime % 10f < 9f)
				{
					RunAnimation(AnimationTagType.AlienTea, 0.25f);
				}
				else
				{
					RunAnimation(AnimationTagType.AlienTeaSip, 0.25f);
				}
				break;
			case CharacterType.Ape:
				if (idleTime % 5f < 3f || idleTime > 10f)
				{
					RunAnimation(AnimationTagType.StandRight, 0.05f);
				}
				else if (idleTime % 5f < 3.1f)
				{
					RunAnimation(AnimationTagType.ApeScratchHeadStart, 0.05f, true);
				}
				else if (idleTime % 5f < 3.95f)
				{
					RunAnimation(AnimationTagType.ApeScratchHeadLookFwd, 0.25f);
				}
				else if (idleTime % 5f < 4.9f)
				{
					RunAnimation(AnimationTagType.ApeScratchHeadLookSide, 0.25f);
				}
				else
				{
					RunAnimation(AnimationTagType.ApeScratchHeadStart, -0.05f, true);
				}
				break;
			case CharacterType.Cat:
				if (character.pounceState == PounceState.Active)
				{
					RunAnimation(AnimationTagType.CatIdleExcited, 0.2f);
					break;
				}
				if (frameCounter < 1f)
				{
					SetFrame(AnimationTagType.StandRight, 0);
				}
				else
				{
					if (frameCounter < 1.05f)
					{
						SetFrame(AnimationTagType.IdleTransition, 0);
					}
					else
					{
						if (frameCounter < 12f)
						{
							if (frameCounter % 5f < 4.05f)
							{
								SetFrame(AnimationTagType.IdleLoop, 0);
							}
							else if (frameCounter % 5f < 4.1f)
							{
								SetFrame(AnimationTagType.IdleLoop, 1);
							}
							else if (frameCounter % 5f < 4.95f)
							{
								SetFrame(AnimationTagType.IdleLoop, 2);
							}
							else
							{
								SetFrame(AnimationTagType.IdleLoop, 3);
							}
						}
						else
						{
							SetFrame(AnimationTagType.IdleLoop, 1);
						}
					}
				}
				break;
			case CharacterType.Doge:
				if (idleTime % 5f < 3f || idleTime > 10f)
				{
					RunAnimation(AnimationTagType.StandRight, 0.05f);

				}
				else if (idleTime % 5f < 3.05f)
				{
					RunAnimation(AnimationTagType.DogeSwoleTransition, 0.1f, true);
				}
				else if (idleTime % 5f < 4.95f)
				{
					RunAnimation(AnimationTagType.DogeSwole, 0.15f);
				}
				else
				{
					RunAnimation(AnimationTagType.DogeSwoleTransition, -0.1f, true);
				}
				break;
			case CharacterType.Frog:
				if (idleTime < 3f)
				{
					RunAnimation(AnimationTagType.StandRight, 0.05f);
				}
				else if (idleTime < 3.05f)
				{
					RunAnimation(AnimationTagType.FrogSit, 0.1f, true);
				}
				else if (idleTime % 6f < 5.6f)
				{
					SetFrame(AnimationTagType.FrogCroak, 0);
				}
				else if (idleTime % 6f < 5.7f)
				{
					SetFrame(AnimationTagType.FrogCroak, 1);
				}
				else if (idleTime % 6f < 5.9f)
				{
					SetFrame(AnimationTagType.FrogCroak, 2);
				}
				else
				{
					SetFrame(AnimationTagType.FrogCroak, 1);
				}
				break;
			case CharacterType.Human:
				if (idleTime < 3f)
				{
					RunAnimation(AnimationTagType.StandRight, 0.05f);
				}
				else if (idleTime < 3.05f)
				{
					RunAnimation(AnimationTagType.HumanSquatStart, 0.1f, true);
				}
				else
				{
					RunAnimation(AnimationTagType.HumanSquatLoop, 0.5f);
				}
				break;
			}
		}
	}

	void AnimateChargeAttack()
	{
		var ad = DetermineDirection(character.attackDir);

		if (ad == AttackDirection.Up)
			RunAnimation(AnimationTagType.BatUpCharge, Mathf.Lerp(0.2f, 0.03f, character.attackChargeM));
		else if (ad == AttackDirection.DiagonalUp)
		{
			RunAnimation(AnimationTagType.BatDiagCharge, Mathf.Lerp(0.2f, 0.03f, character.attackChargeM));
		}
		else if (ad == AttackDirection.Down)
		{
			RunAnimation(AnimationTagType.BatDownCharge, Mathf.Lerp(0.2f, 0.03f, character.attackChargeM));
		}
		else if (ad == AttackDirection.DownForward)
		{
			RunAnimation(AnimationTagType.BatDiagDown, Mathf.Lerp(0.2f, 0.03f, character.attackChargeM));
		}
		else
		{
			RunAnimation(AnimationTagType.BatCharge, Mathf.Lerp(0.2f, 0.03f, character.attackChargeM));
		}
	}

	bool wasBurp;
	void AnimateTongue()
	{
		if (character.tongueState == TongueState.HitFlyBurping)
		{
			if (!wasBurp)
			{
				frame = 0;
				frameCounter = 0f;
				wasBurp = true;
			}

			if (frameCounter < 0.4f)
			{
				RunAnimation(AnimationTagType.BurpStart, 0.08f, true);
			}
			{
				RunAnimation(AnimationTagType.BurpLoop, 0.1f);
			}
		}
		else if (character.tongueState == TongueState.HitEnemyTongueStunned)
		{
			wasBurp = false;
			tongueTip.gameObject.SetActive(false);
			tongueLine.enabled = false;
			RunAnimation(AnimationTagType.FrogBlush, 0.05f);
		}
		else if (!character.OnGround && character.tongueDir.y < 0)
		{
			wasBurp = false;
			if (character.velocity.y >= 0)
			{
				RunAnimation(AnimationTagType.FrogTongueJumpDown, 0.1f, true);
			}
			else
			{
				RunAnimation(AnimationTagType.FrogTongueJumpUp, 0.1f, true);
			}
			tongueLine.enabled = true;
			tongueTip.gameObject.SetActive(true);
			tongueLine.SetPosition(1, Vector3.up * 1.85f + (Vector3)character.tongueDir * character.tongueDistance + Vector3.forward * 1.1f);
			tongueTip.transform.localPosition = (Vector3)(character.tongueOrigin + character.tongueDir * character.tongueDistance) + Vector3.forward;
			tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, character.tongueDir));

			if (character.tongueDir.y < 0f)
				tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, -Vector2.Angle(Vector2.right, character.tongueDir));
			else
				tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, character.tongueDir));
		}
		else
		{
			wasBurp = false;
			if (character.tongueState == TongueState.RetractingHitEnemyTongue)
			{
				RunAnimation(AnimationTagType.FrogTongueStunned, 0.05f);
			}
			else
			{
				RunAnimation(AnimationTagType.FrogTongue, 0.1f, true);
			}

			tongueLine.enabled = true;
			tongueTip.gameObject.SetActive(true);
			tongueLine.SetPosition(1, Vector3.up * 1.85f + (Vector3)character.tongueDir * character.tongueDistance + Vector3.forward * 1.1f);
			tongueTip.transform.localPosition = (Vector3)(character.tongueOrigin + character.tongueDir * character.tongueDistance) + Vector3.forward;
			tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, character.tongueDir));

			if (character.tongueDir.y < 0f)
				tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, -Vector2.Angle(Vector2.right, character.tongueDir));
			else
				tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, character.tongueDir));
		}

		if (character.wasBouncingBeforeSpecial)
		{
			RunTrailSilhouette();
		}
	}

	private void AnimateScratch()
	{
		switch (character.pounceState)
		{
		case PounceState.Charging:
			RunAnimation(AnimationTagType.CatWiggle, Mathf.Lerp(0.2f, 0.05f, character.pounceCharged / character.pounceChargeTime.y));
			break;
		}
	}

	private void AnimateSpinDash()
	{
		switch (character.spinDashState)
		{
		case SpinDashState.Starting:
			RunAnimation(AnimationTagType.DogeCoinTransition, 0.05f, true);
			break;
		case SpinDashState.Charging:
			RunAnimation(AnimationTagType.DogeCoinPowerup, 0.04f);
			break;
		case SpinDashState.Spinning:
			RunAnimation(AnimationTagType.DogeCoinSpin, 0.04f);
			break;
		case SpinDashState.Ending:
			RunAnimation(AnimationTagType.DogeCoinTransition, -0.05f, true);
			break;
		}
	}

	private void AnimateTeleport()
	{
		switch (character.teleportState)
		{
		case TeleportState.Teleporting:
			RunAnimation(AnimationTagType.Empty, 0.04f, true);
			break;
		}
	}

	private void AnimateThrow()
	{
		var ad = DetermineDirection(character.specialAttackDir);
		if (character.throwState == ThrowState.Charging)
		{
			if (ad == AttackDirection.Up)
			{
				RunAnimation(AnimationTagType.ThrowUpCharge, 0.1f, true);
			}
			else if (ad == AttackDirection.DiagonalUp)
			{
				RunAnimation(AnimationTagType.ThrowUpRightCharge, 0.1f, true);
			}
			else if (ad == AttackDirection.Down)
			{
				RunAnimation(character.OnGround ? AnimationTagType.ThrowDownCharge : AnimationTagType.ThrowDownAirCharge, 0.1f, true);
			}
			else if (ad == AttackDirection.DownForward)
			{
				RunAnimation(character.OnGround ? AnimationTagType.ThrowDownRightCharge : AnimationTagType.ThrowDownRightAirCharge, 0.1f, true);
			}
			else
			{
				RunAnimation(AnimationTagType.ThrowCharge, 0.1f, true);
			}
		}
		else
		{
			if (ad == AttackDirection.Up)
			{
				RunAnimation(AnimationTagType.ThrowUp, 0.04f, true);
			}
			else if (ad == AttackDirection.DiagonalUp)
			{
				RunAnimation(AnimationTagType.ThrowUpRight, 0.04f, true);
			}
			else if (ad == AttackDirection.Down)
			{
				RunAnimation(AnimationTagType.ThrowDownAir, 0.04f, true);
			}
			else if (ad == AttackDirection.DownForward)
			{
				RunAnimation(AnimationTagType.ThrowDownRightAir, 0.04f, true);
			}
			else
			{
				RunAnimation(AnimationTagType.Throw, 0.04f, true);
			}
		}
	}

	void AnimateAttack()
	{
		var ad = DetermineDirection(character.attackDir);

		if (ad == AttackDirection.Up)
			RunAnimation(AnimationTagType.Bat, 0.05f, true);
		else if (ad == AttackDirection.DiagonalUp)
		{
			RunAnimation(AnimationTagType.BatDiag, 0.05f, true);
		}
		else if (ad == AttackDirection.Down)
		{
			RunAnimation(AnimationTagType.BatDown, 0.05f, true);
		}
		else if (ad == AttackDirection.DownForward)
		{
			RunAnimation(AnimationTagType.BatDiagDown, 0.05f, true);
		}
		else
		{
			RunAnimation(AnimationTagType.Bat, 0.05f, true);
		}

	}

	void AnimateAttackRecover()
	{
		var ad = DetermineDirection(character.attackDir);

		if (ad == AttackDirection.Up)
			RunAnimation(AnimationTagType.BatUpRecover, 0.05f, true);
		else if (ad == AttackDirection.DiagonalUp)
		{
			RunAnimation(AnimationTagType.BatDiagRecover, 0.05f, true);
		}
		else if (ad == AttackDirection.Down)
		{
			RunAnimation(AnimationTagType.BatDownRecover, 0.05f, true);
		}
		else if (ad == AttackDirection.DownForward)
		{
			RunAnimation(AnimationTagType.BatDiagDownRecover, 0.05f, true);
		}
		else
		{
			RunAnimation(AnimationTagType.BatRecover, 0.05f, true);
		}
	}

	bool havePlayedLaunchSound;


	void AnimateBouncedFlying()
	{

		if (character.TimeBumpActive && character.timeBumpTimeScale == 0)
		{
			RunAnimation(AnimationTagType.Impact, 0.05f, false, true);
			havePlayedLaunchSound = false;
		}
		else
		if (!character.hasBounceDodged)
		{
			if (!havePlayedLaunchSound)
			{
				int hits = Mathf.Clamp(character.hitsTaken, 0, 5);
				int level = 0;
				if (hits >= 5)
					level = 3;
				else if (hits >= 3)
					level = 2;
				else if (hits >= 1)
					level = 1;
				havePlayedLaunchSound = true;
				if (level > 2)
				{
					SoundController.PlaySoundEffect("Launch" + level.ToString(), 0.5f, transform.position);
					SoundController.PlaySoundEffect("LaunchVoice" + level.ToString(), 0.5f, transform.position);
				}
			}

			if (!character.canBounceDodge)
			{



				float a = Vector3.Angle(Vector3.up, character.velocity);
				if (character.velocity.x > 0f)
					a = 360f - a;

				rend.transform.localRotation = Quaternion.Euler(0, 0, a);
				var p = spriteDefaultOffset;
				p.y = 1f;
				rend.transform.localPosition = p;

				if (character.hitsTaken > 4)
				{
					RunAnimation(AnimationTagType.HitComet, 1f / 25f);
				}
				else if (character.hitsTaken > 2)
				{
					RunAnimation(AnimationTagType.HitFly, 1f / 25f);
				}
				else
				{
					if (variationRandomizer % 2 == 0)
					{
						RunAnimation(AnimationTagType.HitRotate, 0.075f);
					}
					else
					{
						RunAnimation(AnimationTagType.HitSpin, 1f / 25f);
					}

				}

				if (character.hitsTaken > 2)
				{
					particleCounter += t;
					if (particleCounter > trailDelay)
					{
						particleCounter -= trailDelay;
						EffectsController.CreateStarParticles(character.Center, character.velocity * 0.1f, 0.5f, 1, new Color[] { Color.white, Color.black, character.player.Color });
					}
				}
			}
			else
			{

				float a = Vector3.Angle(Vector3.up, character.velocity);
				if (character.velocity.x > 0f)
					a = 360f - a;
				rend.transform.localRotation = Quaternion.Euler(0, 0, a);
				RunAnimation(AnimationTagType.HitRecovered, 0.05f);
			}
		}
		else
		{
			//if (flightAudioSource.isPlaying)
			//    flightAudioSource.Stop();
			RunAnimation(AnimationTagType.JumpSomersault, 0.04f);

		}

		if (character.hitsTaken > 0)
		{
			RunTrailSilhouette();
			if (!character.hasBounceDodged)
			{
				if (Vector2.Distance(lastSmokeRingPos, character.Center) > smokeRingDistance && character.velocity.magnitude > character.maxRunSpeed && character.hitsTaken > 2)
				{
					lastSmokeRingPos = character.Center;
					EffectsController.CreateSmokeRing(character.Center, rend.transform.rotation, character.player.Color);
				}

				trailCounter -= t;
				if (trailCounter < 0f)
				{
					trailCounter += trailDelay;

					if (character.lastHitByPlayer != null)
					{





						for (int i = 0; i < character.hitsTaken; i++)
						{
							Color col = Color.white;
							Color lerpFrom = character.player.Color;// Random.value < character.hitsTaken * 0.02f ? character.lastHitByPlayer.color : character.player.color;
							if (Random.value < 0.5f)
								col = Color.Lerp(lerpFrom, Color.black, Random.value * 0.3f);
							else
								col = Color.Lerp(lerpFrom, Color.white, Random.value * 0.8f);
							//if (Random.value < 0.2f)
							if (character.velocity.magnitude > character.maxRunSpeed)
								EffectsController.CreateLineParticle(character.Center + (Vector3)Random.insideUnitCircle * 0.5f + Vector3.forward, rend.transform.rotation, col, character.velocity * 0f, 0.3f + character.velocity.magnitude * lineVelocityScale, 0.25f + 0.5f * character.velocity.magnitude / character.maxRunSpeed);
						}
						//EffectsController.CreateSmokePuff(character.Center + (Vector3)Random.insideUnitCircle * 0.25f + Vector3.forward, col);
					}
				}
			}

		}
	}

	void RunTrailSilhouette()
	{
		trailFaderCounter -= Time.deltaTime;
		if (trailFaderCounter <= 0f)
		{
			trailNumber++;
			trailFaderCounter += trailDelay * 2f;
			EffectsController.CreateTrailFader(this, 0.1f + character.hitsTaken * 0.15f, Color.Lerp(character.lastHitByPlayer.Color, Color.white, Mathf.PingPong(trailNumber * 0.2f, 1f)));
		}
	}

	void RunTrailSilhouettePoweredUp()
	{
		trailFaderCounter -= Time.deltaTime;
		if (trailFaderCounter <= 0f)
		{
			trailNumber++;
			trailFaderCounter += trailDelay * 7f;
			if (trailNumber % 3 == 0)
			{
				rend.color = Color.white;
			}
			else if (trailNumber % 3 == 1)
			{
				rend.color = Color.black;
			}
			else
			{
				//customizer.SetColor(character.player.Color);
				rend.color = Color.white;
			}
			//EffectsController.CreateTrailFader(this, 0.05f, new Color(Random.value, Random.value, Random.value), true);
		}
	}

	float flightOrigin;
	void RunFlightAudio()
	{
		if (character.state == CharacterState.Bouncing)
		{
			if (character.TimeBumpActive && character.timeBumpTimeScale == 0)
			{
				if (flightAudioSource.isPlaying)
				{
					flightAudioSource.Stop();
				}
				flightOrigin = character.transform.position.y;
			}
			else
			if (!character.hasBounceDodged)
			{
				flightAudioSource.volume = 0.15f;
				if (!flightAudioSource.isPlaying)
				{
					if (character.hitsTaken > 4)
						flightAudioSource.clip = flight[2];
					else if (character.hitsTaken > 2)
						flightAudioSource.clip = flight[1];
					else if (character.hitsTaken > 0)
						flightAudioSource.clip = flight[0];
					flightAudioSource.Play();
				}
				flightAudioSource.pitch = 1f + (character.transform.position.y - flightOrigin) * flightHeightPitchMod;
				if (modFlightVolume)
					flightAudioSource.volume = character.velocity.magnitude * flightVelocityVolumeMod;

			}
			else if (flightAudioSource.isPlaying)
			{
				flightAudioSource.Stop();
			}
		}
		else
		{
			flightAudioSource.volume = Mathf.MoveTowards(flightAudioSource.volume, 0f, Time.deltaTime * 2f);
		}
	}
	int trailNumber;
	void AnimateJumping()
	{
		if (character.velocity.y < character.gravityGraceThreshold && character.player != null && character.player.Input != null && character.player.Input.aButton && character.gravityGraceTimeLeft > 0f)
		{
			float m = 1f - character.gravityGraceTimeLeft / character.gravityGraceTime;
			SetFrame(AnimationTagType.JumpSomersault, (int)(m * AnimationTags.JumpSomersault.frameCount));
		}
		else if (character.Velocity.y > 0f)
		{
			if (frame < AnimationTags.JumpLaunch.frameCount)
			{
				SetFrame(AnimationTagType.JumpLaunch);
				frameCounter += t;
				if (frameCounter > 0.05f)
				{
					frame++;
					frameCounter = 0f;
				}
			}
			else
			{
				//transform.localPosition = defaultOffset;
				RunAnimation(AnimationTagType.JumpUp, 0.05f);
			}
		}
		else
		{
			RunAnimation(AnimationTagType.JumpDownArmsOut, 0.05f);
		}
	}

	void AnimateSkid()
	{
		if (Mathf.Abs(transform.position.x - lastSkidXPos) > skidEffectDistance && (Mathf.Abs(character.velocity.x) > character.maxRunSpeed))
		{
			lastSkidXPos = transform.position.x;
			EffectsController.CreateJumpPuffSkew(transform.position, character.FacingDirection);
		}

		if (frame == 0)
		{
			SetFrame(AnimationTagType.SkidLand);
			frameCounter += t;
			if (frameCounter > 0.075f)
			{
				frame++;
				frameCounter = 0f;
			}
		}
		else
		{

			RunAnimation(AnimationTagType.Skid, 0.05f);
		}

		if (character.hitsTaken > 0)
			RunTrailSilhouette();
	}

	internal void RunAnimation(AnimationTagType tag, float frameDelay, bool clamp = false, bool ignoreCharacterTimescale = false)
	{
		frameCounter += ignoreCharacterTimescale ? Time.deltaTime : t;

		if (frameCounter > frameDelay || frame < 0)
		{
			frame++;
			frameCounter -= frameDelay;
		}
		RunFrame(tag, frame, clamp);
	}

	void AnimateWallSlide()
	{
		RunAnimation(AnimationTagType.Wallslide, 1f / 25f);
	}

	AnimState DetermineAnimState()
	{

		if (character.state == CharacterState.Bouncing)
		{
			if (character.OnGround)
			{
				return AnimState.Skidding;
			}
			else
			{
				return AnimState.BouncedInAir;
			}
		}
		else if (character.state == CharacterState.Attacking)
		{
			if (character.attackState == AttackState.Charging)
			{
				return AnimState.ChargeAttack;
			}
			else if (character.attackState == AttackState.Attacking)
			{
				return AnimState.Attacking;
			}
			else if (character.attackState == AttackState.Recovering)
			{
				return AnimState.AttackRecover;
			}
		}
		else if (character.state == CharacterState.Special && character.type == CharacterType.Frog)
		{
			return AnimState.Tongue;
		}
		else if (character.state == CharacterState.Special && (character.type == CharacterType.Ape || character.type == CharacterType.Human))
		{
			return AnimState.Throw;
		}
		else if (character.state == CharacterState.Special && character.type == CharacterType.Alien)
		{
			return AnimState.Teleport;
		}
		else if (character.state == CharacterState.Special && character.type == CharacterType.Doge)
		{
			return AnimState.SpinDash;
		}
		else if (character.state == CharacterState.Special && character.type == CharacterType.Cat)
		{
			return AnimState.Scratch;
		}
		else if (character.OnGround && Mathf.Abs(character.Velocity.x) > 0f)
		{
			return AnimState.Running;
		}
		else if (!character.OnGround)
		{
			if (character.WallSliding)
			{
				return AnimState.WallSlide;
			}
			else
			{
				return AnimState.Jumping;
			}
		}

		return AnimState.Idle;
	}

	public Sprite GetFrame(AnimationTagType tag, int frame)
	{
		int absFrame = AnimationTags.tags[tag].frames[frame];
		return GetFrameAbsolute(absFrame);
	}

	public Sprite GetFrameAbsolute(int frame)
	{
		return character.player != null && character.player.bro != null && character.player.bro.sprites != null &&
			frame < character.player.bro.sprites.Count ? character.player.bro.sprites[frame] : null;
	}

	public void SetFrame(AnimationTagType tag)
	{
		rend.sprite = GetFrame(tag, 0);
	}

	public void SetFrame(AnimationTagType tag, int frame)
	{
		rend.sprite = GetFrame(tag, frame);
	}

	public void SetFrameAbsolute(int frame)
	{
		rend.sprite = GetFrameAbsolute(frame);
	}

	public void RunFrame(AnimationTagType tag, int frame, bool clamp)
	{
		int frameCount = AnimationTags.tags[tag].frameCount;
		if (clamp)
		{
			SetFrame(tag, Mathf.Clamp(frame, 0, frameCount - 1));
		}
		else
		{
			SetFrame(tag, frame % frameCount);
		}
	}
}
