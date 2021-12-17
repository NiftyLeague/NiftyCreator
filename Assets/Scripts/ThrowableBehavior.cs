
using System;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{
	public SpriteRenderer sr;
	internal Character owner;
	internal Vector2 velocity;
	internal int terrainLayer, characterLayer;

	private void Awake()
	{
		terrainLayer = 1 << LayerMask.NameToLayer("Ground");
		characterLayer = 1 << LayerMask.NameToLayer("Character");
	}

	public virtual void Initialize(Character owner, Vector2 velocity, Vector3 position)
	{
		this.owner = owner;
		this.velocity = velocity;
		transform.position = position;
	}

	private void FixedUpdate()
	{
		RunMotion();

		if (transform.position.x < Terrain.LeftKillPoint || transform.position.x > Terrain.RightKillPoint || transform.position.y > Terrain.TopKillPoint || transform.position.y < Terrain.BotKillPoint)
		{
			Destroy(gameObject);
		}
	}

	internal virtual void RunMotion()
	{
	}

	public virtual void PerformAction()
	{
	}
}
