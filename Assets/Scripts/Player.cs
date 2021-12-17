using UnityEngine;
#if PHOTON_UNITY_NETWORKING
using PhotonPlayer = Photon.Realtime.Player;
#endif

public class Player
{
#if PHOTON_UNITY_NETWORKING
	public PhotonPlayer networkPlayer;
#endif
	public NiftyBro bro = null;
	public InputState Input => input;
	public Team Team => team;
	public Color Color { get { return bro != null ? (Color)bro.primaryColor : Color.white; } }
	public int Score => score;
	public int RoundWins => roundWins;
	public SpriteRenderer OffscreenDot => offscreenDot;
	public int SortPriority => sortPriority;
	public Character Character => character;
	public float SpawnDelay => spawnDelay;

	private InputState input = new InputState();
	private Team team;
	private int score;
	private int roundWins;
	private SpriteRenderer offscreenDot;
	private int sortPriority;
	private Character character;
	private float spawnDelay = 0f;
	private InputReader.Device inputDevice;

#if PHOTON_UNITY_NETWORKING
	public Player(PhotonPlayer networkPlayer, int sortPriority, InputReader.Device inputDevice = InputReader.Device.None)
	{
		this.networkPlayer = networkPlayer;
		this.inputDevice = inputDevice;
		this.sortPriority = sortPriority;
	}
#endif

	public InputState ReadInput()
	{
		if (inputDevice == InputReader.Device.None)
		{
			InputReader.GetInput(input);
		}
		else
		{
			InputReader.GetInput(inputDevice, input);
		}
		return input;
	}

	public void SetBro(NiftyBro bro)
	{
		this.bro = bro;
		character.type = bro.type;
		bro.Rasterize(res => { Debug.Log("Rasterization complete"); });
	}

	public void ClearInput()
	{
		InputReader.ClearInputState(input);
	}

	public void SetTeam(Team team)
	{
		this.team = team;
	}

	public void SetScore(int score)
	{
		this.score = score;
	}

	public void SetSpawnDelay(float spawnDelay)
	{
		this.spawnDelay = spawnDelay;
	}

	public void SetOffscreenDot(SpriteRenderer offscreenDot)
	{
		this.offscreenDot = offscreenDot;
	}

	public void SetCharacter(Character character)
	{
		this.character = character;
		character.GetComponent<InputView>().SetInputStateRef(ref input);
	}

	public void SetRoundWins(int roundWins)
	{
		this.roundWins = roundWins;
	}
}


public enum Team
{
	Blue,
	Red
}