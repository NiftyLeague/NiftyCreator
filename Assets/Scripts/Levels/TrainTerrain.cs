using Beebyte.Obfuscator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTerrain : MonoBehaviour
{
	public Transform[] cabins;
	public Transform[] tracks;
	public Transform[] tunnels;
	public Transform[] backgrounds;
	public float bgWidth;
	public float minX;
	public float moduleWidth;
	public float trackSpeed;
	public float cabinSpeed;
	public float bgSpeed;
	public Vector2 tunnelToggleTimes;

	private bool isTunnel = true;

	private void Start()
	{
		ToggleTunnel();
	}

	private void LateUpdate()
	{
		if (Terrain.GetCharacterParent())
		{
			Terrain.GetCharacterParent().Translate(new Vector3(cabinSpeed * Time.deltaTime, 0f, 0f));
		}
		for (int i = 0; i < 3; i++)
		{
			tracks[i].Translate(new Vector3(trackSpeed * Time.deltaTime, 0f, 0f));
			if (tracks[i].position.x <= minX)
			{
				tracks[i].Translate(new Vector3(moduleWidth * 3f, 0f, 0f));
			}

			tunnels[i].Translate(new Vector3(trackSpeed * Time.deltaTime, 0f, 0f));
			if (tunnels[i].position.x <= minX)
			{
				tunnels[i].Translate(new Vector3(moduleWidth * 3f, 0f, 0f));
				tunnels[i].gameObject.SetActive(isTunnel);
			}

			cabins[i].Translate(new Vector3(cabinSpeed * Time.deltaTime, 0f, 0f));
			if (cabins[i].position.x <= minX)
			{
				cabins[i].Translate(new Vector3(moduleWidth * 3f, 0f, 0f));
			}
		}

		for (int i = 0; i < 2; i++)
		{
			backgrounds[i].Translate(new Vector3(bgSpeed * Time.deltaTime, 0f, 0f));
			if (backgrounds[i].position.x <= -bgWidth)
			{
				backgrounds[i].Translate(new Vector3(bgWidth * 2f, 0f, 0f));
			}
		}
	}

	[SkipRename]
	private void ToggleTunnel()
	{
		Invoke(nameof(ToggleTunnel), isTunnel ? tunnelToggleTimes.x : tunnelToggleTimes.y);
		isTunnel = !isTunnel;
	}

}
