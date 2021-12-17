using UnityEngine;
using System.Linq;

public class Terrain : MonoBehaviour
{

	static Terrain instance;

	public Transform characterParent;
	public Transform leftKillPoint;
	public Transform rightKillPoint;
	public Transform botKillPoint;
	public Transform topKillPoint;
	public Transform screenTop;

	public Transform flySpawn;

	public Transform[] spawnPoints;
	public float updateSpawnPointsTimeout = -1f;
	public float spawnPointMargin;
	private float lastSpawnPointUpdateTime = -10f;


	void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		if (updateSpawnPointsTimeout > 0 && Time.time - lastSpawnPointUpdateTime > updateSpawnPointsTimeout)
		{
			lastSpawnPointUpdateTime = Time.time;
			spawnPoints = GameObject.FindGameObjectsWithTag("Respawn").Where(p =>
			 {
				 return p.transform.position.x > leftKillPoint.transform.position.x + spawnPointMargin &&
						p.transform.position.x < rightKillPoint.transform.position.x - spawnPointMargin &&
						p.transform.position.y < topKillPoint.transform.position.y - spawnPointMargin &&
						p.transform.position.y > botKillPoint.transform.position.y + spawnPointMargin;
			 }).OrderBy(p => -p.transform.position.x).Select(p => p.transform).ToArray();
		}
	}

	public static Transform GetCharacterParent()
	{
		return instance.characterParent;
	}

	public static Vector3 GetFlySpawnPoint()
	{
		if (instance.flySpawn != null)
			return instance.flySpawn.position;
		return Vector3.zero;
	}

	public static Vector3 GetSpawnPoint(int index)
	{
		return instance.spawnPoints[index].position;
	}

	public static Vector3 GetSpawnPoint()
	{
		float[] closestCharacterDistance = new float[instance.spawnPoints.Length];
		int i = 0;
		foreach (var spawn in instance.spawnPoints)
		{
			closestCharacterDistance[i] = float.MaxValue;
			foreach (var player in GameController.activePlayers)
			{
				if (player.Character != null)
				{
					var dist = Vector2.Distance(player.Character.transform.position, spawn.position);
					if (dist < closestCharacterDistance[i])
					{
						closestCharacterDistance[i] = dist;
					}
				}
			}
			i++;
		}

		int furthestIndex = 0;
		float furthestDistance = float.MinValue;
		i = 0;
		foreach (var spawn in instance.spawnPoints)
		{
			if (closestCharacterDistance[i] == furthestDistance && Random.value < 0.5f)
			{
				furthestIndex = i;
				furthestDistance = closestCharacterDistance[i];
			}
			else
			if (closestCharacterDistance[i] > furthestDistance)
			{
				furthestIndex = i;
				furthestDistance = closestCharacterDistance[i];
			}
			i++;
		}

		return instance.spawnPoints[furthestIndex].position;
	}

	public static float LeftKillPoint
	{
		get
		{
			return instance.leftKillPoint.position.x;

		}
	}

	public static float RightKillPoint
	{
		get
		{
			return instance.rightKillPoint.position.x;

		}
	}

	public static float TopKillPoint
	{
		get
		{
			return instance.topKillPoint.position.y;

		}
	}

	public static float BotKillPoint
	{
		get
		{
			return instance.botKillPoint.position.y;

		}
	}

	public static float ScreenTop
	{
		get
		{
			return instance.screenTop.position.y;
		}
	}

}
