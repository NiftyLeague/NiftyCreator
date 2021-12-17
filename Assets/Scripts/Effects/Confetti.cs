using UnityEngine;

public class Confetti : MonoBehaviour
{
	public Vector3 velocity;
	public Vector3 accel;
	float life;


	void Update()
	{
		transform.position += velocity * Time.deltaTime;
		life += Time.deltaTime;
		velocity += accel * Time.deltaTime;
		if (life > 10f && !GetComponent<SpriteRenderer>().isVisible)
		{
			Destroy(gameObject);
		}
	}
}
