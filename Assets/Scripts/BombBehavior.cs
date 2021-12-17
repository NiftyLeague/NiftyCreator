using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : ThrowableBehavior
{
	public Vector2 dimentions;
	public float gravity;
	public float maxFallSpeed;
	public float lineVelocityScale;
	public Vector2 velocityDampRate;
	public float explosionRadius;
	public float explosionPower;
	public float explosionTimeout;

	private List<Character> charactersHit = new List<Character>();
	private bool exploded = false;


	public override void Initialize(Character owner, Vector2 velocity, Vector3 position)
	{
		if (velocity.normalized == Vector2.right || velocity.normalized == Vector2.left)
		{
			velocity += Vector2.up * 0.3f * velocity.magnitude;
		}
		base.Initialize(owner, velocity, position);
		Invoke(nameof(Explode), explosionTimeout);
		charactersHit.Clear();
	}


	internal override void RunMotion()
	{
		if (exploded)
		{
			return;
		}

		if (velocity.y > maxFallSpeed)
			velocity.y -= gravity * Time.deltaTime;

		Vector2 velocityT = velocity * Time.deltaTime;
		if (velocityT.x < 0f)
		{
			if (Physics2D.Raycast(transform.position - Vector3.right * dimentions.x, Vector2.left, Mathf.Abs(velocityT.x) + 0.25f, terrainLayer))
			{
				velocityT.x = 0f;
				velocity.x *= -1f;
				velocity *= velocityDampRate;
			}
		}
		if (velocityT.x > 0f)
		{
			if (Physics2D.Raycast(transform.position + Vector3.right * dimentions.x, Vector2.right, Mathf.Abs(velocityT.x) + 0.25f, terrainLayer))
			{
				velocityT.x = 0f;
				velocity.x *= -1f;
				velocity *= velocityDampRate;
			}
		}
		if (velocityT.y < 0f)
		{
			if (Physics2D.Raycast(transform.position - Vector3.up * dimentions.y, Vector2.down, Mathf.Abs(velocityT.y) + 0.25f, terrainLayer))
			{
				velocityT.y = 0f;
				velocity.y *= -1f;
				velocity *= velocityDampRate;
			}
		}
		if (velocityT.y > 0f)
		{
			if (Physics2D.Raycast(transform.position + Vector3.up * dimentions.y, Vector2.up, Mathf.Abs(velocityT.y) + 0.25f, terrainLayer))
			{
				velocityT.y = 0f;
				velocity.y *= -1f;
				velocity *= velocityDampRate;
			}
		}

		if (velocityT.x < 0)
			sr.flipX = true;
		else
			sr.flipX = false;

		transform.position += (Vector3)velocityT;

		if (Time.frameCount % 2 == 0 && velocityT.magnitude > 0.01f)
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
				//chr.GetSoftHit(velocity, owner, 10f, 0f, 0f);
				owner.SoftHit(chr, velocity, 10f, 0f, 0f);
				EffectsController.CreateTongueHitEffect(transform.position, 0.2f);
				charactersHit.Add(chr);
			}
		}
	}

	public override void PerformAction()
	{
		Explode();
	}


	private void Explode()
	{
		if (exploded)
		{
			return;
		}
		exploded = true;
		EffectsController.CreateExplosion01(transform.position + Vector3.up);
		sr.enabled = false;

		var cols = Physics2D.OverlapCircleAll(transform.position, explosionRadius, characterLayer);
		foreach (var col in cols)
		{
			var chr = col.GetComponent<Character>();
			if (chr != null)
			{
				Vector2 dir = (chr.transform.position - (transform.position + Vector3.down)).normalized;
				print(dir);
				dir = dir + Vector2.up * 0.5f;
				//chr.GetSoftHit(dir, owner, explosionPower, 0f, 0f);
				owner.SoftHit(chr, dir, explosionPower, 0f, 0f);
			}
		}

		Destroy(gameObject, 0.5f);
	}
}
