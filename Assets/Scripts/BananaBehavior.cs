using System;
using System.Collections.Generic;
using UnityEngine;


public class BananaBehavior : ThrowableBehavior
{
	public float returnSpeed;
	public float returnVelocityBoost = 1f;
	public float lineVelocityScale;


	private float rotation = 0;
	private bool isReturning = false;
	private Vector2 returnVelocity;
	private float originalSpeed;
	private List<Character> charactersHit = new List<Character>();


	public override void Initialize(Character owner, Vector2 velocity, Vector3 position)
	{
		base.Initialize(owner, velocity, position);
		returnVelocity = -velocity;
		originalSpeed = velocity.magnitude;
		charactersHit.Clear();
	}

	internal override void RunMotion()
	{
		if (isReturning)
		{
			if (owner)
			{
				Vector3 dist = (owner.transform.position + Vector3.up) - transform.position;
				if (dist.magnitude < 1.5f)
				{
					Destroy(gameObject);
				}
				returnVelocity = dist.normalized * originalSpeed;
			}
			velocity = Vector2.MoveTowards(velocity, returnVelocity * returnVelocityBoost, returnSpeed * Time.deltaTime);
		}

		Vector2 velocityT = velocity * Time.deltaTime;
		rotation += Time.deltaTime;
		if (velocityT.x < 0f)
		{
			if (Physics2D.Raycast(transform.position, Vector2.left, Mathf.Abs(velocityT.x) + 0.25f, terrainLayer))
			{
				velocityT.x = 0f;
				velocity.x *= -1f;
				HitTerrain();
			}
		}
		if (velocityT.x > 0f)
		{
			if (Physics2D.Raycast(transform.position, Vector2.right, Mathf.Abs(velocityT.x) + 0.25f, terrainLayer))
			{
				velocityT.x = 0f;
				velocity.x *= -1f;
				HitTerrain();
			}
		}
		if (velocityT.y < 0f)
		{
			if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Abs(velocityT.y) + 0.25f, terrainLayer))
			{
				velocityT.y = 0f;
				velocity.y *= -1f;
				HitTerrain();
			}
		}
		if (velocityT.y > 0f)
		{
			if (Physics2D.Raycast(transform.position, Vector2.up, Mathf.Abs(velocityT.y) + 0.25f, terrainLayer))
			{
				velocityT.y = 0f;
				velocity.y *= -1f;
				HitTerrain();
			}
		}

		if (velocityT.x < 0)
			sr.flipX = true;
		else
			sr.flipX = false;

		transform.position += (Vector3)velocityT;
		sr.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.RoundToInt(rotation * 30f) * 90f * (velocityT.x < 0 ? 1f : -1f));
		if (Time.frameCount % 2 == 0)
		{
			EffectsController.CreateLineParticle(transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 0.5f + Vector3.forward,
				Quaternion.Euler(0f, 0f, Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg - 90f),
				Color.white, velocity * 0f, velocity.magnitude * lineVelocityScale, 0.75f);
		}

		var cols = Physics2D.OverlapCircleAll(transform.position, 1.5f, characterLayer);
		foreach (var col in cols)
		{
			var chr = col.GetComponent<Character>();
			if (chr != null && chr != owner && !charactersHit.Contains(chr))
			{
				//chr.GetSoftHit(velocity, owner, 20f, 0.25f, 0f);
				owner.SoftHit(chr, velocity, 20f, 0.25f, 0f);
				EffectsController.CreateTongueHitEffect(transform.position, 0.2f);
				charactersHit.Add(chr);
			}
		}
	}

	private void HitTerrain()
	{
		Destroy(gameObject);
	}

	public override void PerformAction()
	{
		isReturning = true;
		charactersHit.Clear();
	}
}
