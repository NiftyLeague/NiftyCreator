using Beebyte.Obfuscator;
using System;
using UnityEngine;



public class MarsTerrain : MonoBehaviour
{

	public Transform worm;
	public Vector2 wormAttackTimeout;
	public Vector2 wormCameraWobbleSpeed, wormCameraWobbleAmount;
	private WormState wormState = WormState.Idle;
	public Vector2 wormYRange;
	public Vector2 wormAttackSpeed;

	private Vector2 _wormCameraWobbleSpeed, _wormCameraWobbleAmount;
	private float wormDelay = 0f;
	private Vector3 wormPosition;

	private void Start()
	{
		wormState = WormState.Idle;
		Invoke(nameof(StartWormAttack), UnityEngine.Random.Range(wormAttackTimeout.x, wormAttackTimeout.y));
		wormPosition = worm.position;
		wormPosition.y = wormYRange.x;
		worm.position = wormPosition;
	}


	private void Update()
	{
		RunWormAttack();
	}

	[SkipRename]
	private void StartWormAttack()
	{
		if (wormState == WormState.Idle)
		{
			wormState = WormState.Starting;
			wormDelay = 1f;
		}
	}

	private void RunWormAttack()
	{
		switch (wormState)
		{
		case WormState.Idle:
			break;
		case WormState.Starting:
			_wormCameraWobbleAmount = Vector2.Lerp(_wormCameraWobbleAmount, wormCameraWobbleAmount, Time.deltaTime * 10f);
			_wormCameraWobbleSpeed = Vector2.Lerp(_wormCameraWobbleSpeed, wormCameraWobbleSpeed, Time.deltaTime * 10f);
			EffectsController.SetCameraWobble(_wormCameraWobbleSpeed, _wormCameraWobbleAmount);
			wormDelay -= Time.deltaTime;
			if (wormDelay < 0) {
				wormState = WormState.Attacking;
				wormDelay = 2f;
				EffectsController.ShakeCamera(Vector2.up, 3f);
			}
			break;
		case WormState.Attacking:
			wormPosition.y = Mathf.Lerp(wormPosition.y, wormYRange.y, Time.deltaTime * wormAttackSpeed.x);
			_wormCameraWobbleAmount = Vector2.Lerp(_wormCameraWobbleAmount, wormCameraWobbleAmount * 1.5f, Time.deltaTime * 20f);
			EffectsController.SetCameraWobble(_wormCameraWobbleSpeed, _wormCameraWobbleAmount);
			worm.position = wormPosition;
			wormDelay -= Time.deltaTime;
			if (wormDelay < 0)
			{
				wormState = WormState.Retracting;
				wormDelay = 4f;
			}
			break;
		case WormState.Retracting:
			wormPosition.y = Mathf.Lerp(wormPosition.y, wormYRange.x, Time.deltaTime * wormAttackSpeed.y);
			_wormCameraWobbleAmount = Vector2.Lerp(_wormCameraWobbleAmount, Vector2.zero, Time.deltaTime * wormAttackSpeed.y * 1.5f);
			_wormCameraWobbleSpeed = Vector2.Lerp(_wormCameraWobbleSpeed, Vector2.zero, Time.deltaTime * wormAttackSpeed.y * 1.5f);
			EffectsController.SetCameraWobble(_wormCameraWobbleSpeed, _wormCameraWobbleAmount);
			worm.position = wormPosition;
			wormDelay -= Time.deltaTime;
			if (wormDelay < 0)
			{
				wormState = WormState.Idle;
				Invoke(nameof(StartWormAttack), UnityEngine.Random.Range(wormAttackTimeout.x, wormAttackTimeout.y));
				EffectsController.SetCameraWobble(Vector2.zero, Vector2.zero);
			}
			break;
		}
	}

	private enum WormState
	{
		Idle,
		Starting,
		Attacking,
		Retracting,
	}

}
