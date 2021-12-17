using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XRandom
{
	private static System.Random random = new System.Random((int)XUtils.Timestamp());


	public static bool NextBool()
	{
		return NextFloat() > 0.5f;
	}

	public static float NextFloat()
	{
		return (float)random.NextDouble();
	}

	public static float NextFloat(float min, float max)
	{
		float rand = (float)random.NextDouble();
		return min + rand * (max - min);
	}

	public static float NextFloat(Vector2 range)
	{
		float rand = (float)random.NextDouble();
		return range.x + rand * (range.y - range.x);
	}

	public static int NextInt(int max)
	{
		return random.Next(max);
	}

	public static int NextInt(int min, int max)
	{
		return random.Next(min, max);
	}

	public static T NextMember<T>(List<T> array)
	{
		if (array.Count > 0)
		{
			return array[NextInt(array.Count)];
		}
		return default(T);
	}

	public static T NextMember<T>(T[] array)
	{
		if (array.Length > 0)
		{
			return array[NextInt(array.Length)];
		}
		return default(T);
	}

	public static int NextMemberIndex<T>(T[] array)
	{
		return NextInt(array.Length);
	}

	public static int NextMemberIndex<T>(List<T> array)
	{
		return NextInt(array.Count);
	}


	public static int GetRandomIndex(float[] probabilities)
	{
		float sum = 0;
		foreach (var p in probabilities)
		{
			sum += p;
		}

		float rand = NextFloat() * sum;

		sum = 0f;
		for (int i = 0; i < probabilities.Length; i++)
		{
			sum += probabilities[i];
			if (rand <= sum)
			{
				return i;
			}
		}
		return 0;
	}
}
