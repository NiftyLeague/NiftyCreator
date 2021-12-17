using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehavior : MonoBehaviour
{
	private int characterLayer;
	private Collider2D[] colliders;

	private void Awake()
	{
		characterLayer = 1 << LayerMask.NameToLayer("Character");
		colliders = GetComponents<Collider2D>();
	}

	private void Update()
	{
		foreach (var c in colliders)
		{
			c.IsTouchingLayers(Physics2D.AllLayers);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Character ch = collision.gameObject.GetComponent<Character>();
		if (ch)
		{
			ch.Die(EffectsController.Side.Bottom);
		}
	}
}
