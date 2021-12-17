
using System;
using UnityEngine;

public class CharacterEvents : MonoBehaviour
{
	public EventAction onEventDispatched;
	public Character character;

	private void Awake()
	{
		character = GetComponent<Character>();
	}

	public void Hit(Character victim, Vector2 hitDir, float power)
	{
		OnEventDispatched(CharacterEventType.HitInit, victim, hitDir, power, victim.hitsTaken);
	}

	public void SoftHit(Character victim, Vector2 hitDir, float power, float victimTimeBump, float attackerTimeBump)
	{
		OnEventDispatched(CharacterEventType.SoftHitInit, victim, hitDir, power, victimTimeBump, attackerTimeBump);
	}


	private void OnEventDispatched(CharacterEventType type, params object[] args)
	{
		if (onEventDispatched != null)
		{
			onEventDispatched(type, args);
		}
	}
}

public delegate void EventAction(CharacterEventType type, params object[] args);


public enum CharacterEventType
{
	None,
	HitInit,
	SoftHitInit,
}
