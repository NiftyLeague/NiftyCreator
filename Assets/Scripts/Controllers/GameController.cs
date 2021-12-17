using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif


public enum GameState
{
	Playing,
	RoundFinished,
	JoinScreen
}


public class GameController : MonoBehaviour
{
	public static bool charactersBounceEachOther = false;
	public static bool weirdBounceTrajectories = false;
	public static bool onlyBounceBeforeRecover = true;
	public static bool allowTeamMode = false;
	public static int nextLevelIndex = 1;

	public static List<Player> activePlayers = new List<Player>();

	static List<Player> inactivePlayers;

	GameState state;

	public static Color overallWinnerColor = Color.red;

	public static GameState State { get { return instance.state; } }

	public Character characterPrefab;

	public Fly flyPrefab, activeFly;

	float flySpawnDelay;

	public SpriteRenderer offscreenDotPrefab;

	public Canvas scoreCanvas;

	public List<PlayerScoreDisplay> playerScoreDisplays;

	public PlayerScoreDisplay scoreDisplayPrefab;

	static GameController instance;

	public Color[] playerColors;

	List<Color> availableColors;

	public bool isJoinScreen;

	public static string[] levelNames = new string[] { "1BusStop", "2DownSmash", "3Moon", "4FinalFrogstination", "5Skyline", "6Finale" };
	public JoinCanvas[] joinCanvas;

	float finishDelay = 7.5f;

	public Text joinCountdownText, joinGameModeText;

	public static bool isTeamMode;

	public static bool playersCanDropIn;

	public static bool isShowDown;

	public static int levelNo;

	int redTeamScore, blueTeamScore;
	PlayerScoreDisplay redTeamScoreDisplay, blueTeamScoreDisplay;

	void Awake()
	{
		if (CustomizationManager.I == null)
		{
			SceneManager.LoadScene("Launcher");
			return;
		}
		//Camera.main.aspect = 16f / 9f;
#if PHOTON_UNITY_NETWORKING
		if (isJoinScreen)
		{
			isShowDown = false;
			state = GameState.JoinScreen;
			finishDelay = 5f;
			SetupForJoinScreen();

			inactivePlayers = null;
			activePlayers.Clear();
			levelNo = 0;
			playersCanDropIn = true;

		}
#endif //#if PHOTON_UNITY_NETWORKING
		playerScoreDisplays = new List<PlayerScoreDisplay>();
		instance = this;
		if (inactivePlayers == null)
		{
			inactivePlayers = new List<Player>();
			//Player p;
			//p = new Player(InputReader.Device.Gamepad1, playerColors[0], 0);
			//inactivePlayers.Add(p);
			//p = new LocaPlayerlPlayer(InputReader.Device.Gamepad2, playerColors[1], 1);
			//inactivePlayers.Add(p);
			//p = new Player(InputReader.Device.Gamepad3, playerColors[2], 2);
			//inactivePlayers.Add(p);
			//p = new Player(InputReader.Device.Gamepad4, playerColors[3], 3);
			//inactivePlayers.Add(p);
			//p = new Player(InputReader.Device.Keyboard1, playerColors[4], 4);
			//inactivePlayers.Add(p);
			//p = new Player(InputReader.Device.Keyboard2, playerColors[5], 5);
			//inactivePlayers.Add(p);

		}
		else
		{
			int i = 0;
			if (isTeamMode)
			{
				foreach (var player in activePlayers)
				{
					player.SetScore(0);
					player.SetSpawnDelay(0.5f + 0.2f * i);
					i++;
				}


				var psd = Instantiate(scoreDisplayPrefab, scoreCanvas.transform) as PlayerScoreDisplay;
				psd.color = Color.red;
				psd.text.color = Color.red;
				redTeamScoreDisplay = psd;
				foreach (var p in activePlayers)
					if (p.Team == Team.Red)
						psd.player = p;

				playerScoreDisplays.Add(psd);

				psd = Instantiate(scoreDisplayPrefab, scoreCanvas.transform) as PlayerScoreDisplay;
				psd.color = Color.blue;
				psd.text.color = Color.blue;
				blueTeamScoreDisplay = psd;
				foreach (var p in activePlayers)
					if (p.Team == Team.Blue)
						psd.player = p;
				playerScoreDisplays.Add(psd);

			}
			else
			{
				foreach (var player in activePlayers)
				{
					player.SetScore(0);
					player.SetSpawnDelay(0.5f + 0.2f * i);
					i++;
					var psd = Instantiate(scoreDisplayPrefab, scoreCanvas.transform) as PlayerScoreDisplay;
					psd.player = player;
					psd.color = player.Color;
					psd.text.color = player.Color;
					playerScoreDisplays.Add(psd);

				}
			}
		}

		if (isShowDown)
			foreach (var psd in playerScoreDisplays)
			{
				psd.gameObject.SetActive(false);
			}

		InputReader.GetInput(combinedInput);
	}

	internal static void SetupPlayersForShowdown()
	{
		List<Player> winningPlayers = GetLeadingPlayers();
		activePlayers.Clear();
		foreach (var p in winningPlayers)
			activePlayers.Add(p);
		playersCanDropIn = false;

	}

	void Start()
	{
		//float aspect = ((float)Screen.width / Screen.height);
		//float screenWidth = 18f * aspect;
		//float adust = 16f / screenWidth;
		//Camera.main.orthographicSize = adust * 18f;
	}

	InputState input;
	InputState combinedInput = new InputState();
#if PHOTON_UNITY_NETWORKING
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			var sceneToLoad = nextLevelIndex;
			nextLevelIndex = (nextLevelIndex + 1) % 9;
			SceneManager.LoadScene(sceneToLoad);
		}

		if (state == GameState.JoinScreen)
		{
			for (int i = inactivePlayers.Count - 1; i >= 0; i--)
			{
				input = inactivePlayers[i].ReadInput();
				if (input.xButton)
				{
					for (int j = 0; j < joinCanvas.Length; j++)
					{
						if (!joinCanvas[j].HasAssignedPlayer())
						{
							joinCanvas[j].AssignPlayer(inactivePlayers[i], i);
							inactivePlayers.RemoveAt(i);
							j = joinCanvas.Length;
						}
					}
				}
			}


			int assignedPlayers = 0;
			for (int i = 0; i < joinCanvas.Length; i++)
			{
				if (joinCanvas[i].HasAssignedPlayer())
				{
					assignedPlayers++;
				}
			}

			bool playersAreReady = CheckReadyPlayers();
			if (assignedPlayers == 0)
			{
				InputReader.GetInput(combinedInput);
				if (combinedInput.start && !combinedInput.wasStart && allowTeamMode)
				{
					isTeamMode = !isTeamMode;
					joinGameModeText.text = isTeamMode ? "TEAM" : "FREE  FOR  ALL";
				}
			}
			else if (playersAreReady)
			{
				finishDelay -= Time.deltaTime;
				joinCountdownText.text = ((int)(finishDelay) + 1).ToString();
				if (finishDelay <= 0f)
					FinishRound();
			}
			else
			{
				joinCountdownText.text = "";
				finishDelay = 5f;
			}
		}
		else if (state == GameState.Playing)
		{
			if (activeFly == null && !isShowDown)
			{
				if (flySpawnDelay > 0f)
				{
					flySpawnDelay -= Time.deltaTime;
					if (flySpawnDelay <= 0f)
						activeFly = Instantiate(flyPrefab, Terrain.GetFlySpawnPoint(), Quaternion.identity);
				}
				else
				{
					flySpawnDelay = Random.Range(15f, 45f);
				}
			}

			for (int i = 0; i < activePlayers.Count; i++)
			{
				if (activePlayers[i].Character == null)
				{
					activePlayers[i].SetSpawnDelay(activePlayers[i].SpawnDelay - Time.deltaTime);
					if (activePlayers[i].SpawnDelay < 0f)
					{
						SpawnCharacter(activePlayers[i]);
					}
				}

				if (activePlayers[i].Character != null && activePlayers[i].Character.transform.position.y > Terrain.ScreenTop)
				{
					var spr = activePlayers[i].OffscreenDot;
					if (spr == null)
					{
						spr = Instantiate(offscreenDotPrefab);
						activePlayers[i].SetOffscreenDot(spr);
						spr.color = activePlayers[i].Color;
					}
					spr.enabled = true;
					spr.transform.position = new Vector3(activePlayers[i].Character.transform.position.x, Terrain.ScreenTop, -6f);
				}
				else
				{
					var spr = activePlayers[i].OffscreenDot;
					if (spr != null)
					{
						spr.enabled = false;
					}
				}
			}

			ArrangeScoreboards();

			if (!isShowDown)
			{
				for (int i = inactivePlayers.Count - 1; i >= 0; i--)
				{
					input = inactivePlayers[i].ReadInput();

					if (input.xButton)
					{
						//inactivePlayers[i].SetColor(playerColors[Random.Range(0, playerColors.Length)]);
						AddPlayer(inactivePlayers[i]);
						inactivePlayers.RemoveAt(i);
					}
				}

				if (Input.GetKeyDown(KeyCode.F2))
				{
					SpawnCharacter(null);
				}
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene("TitleScreen");
			}
		}
		else if (state == GameState.RoundFinished)
		{
			ArrangeScoreboards();

			for (int i = 0; i < activePlayers.Count; i++)
			{
				if (activePlayers[i].Character == null && winningPlayer == activePlayers[i])
				{
					activePlayers[i].SetSpawnDelay(activePlayers[i].SpawnDelay - Time.deltaTime);
					if (activePlayers[i].SpawnDelay < 0f)
					{
						SpawnCharacter(activePlayers[i]);
					}
				}
			}


			finishDelay -= Time.deltaTime;
			if (finishDelay < 0f)
				FinishRound();
		}

		if (Input.GetKeyDown(KeyCode.F6))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKeyDown(KeyCode.F12))
		{
			showGui = !showGui;
		}
	}

	void SetupForJoinScreen()
	{
		availableColors = new List<Color>();
		availableColors.AddRange(playerColors);
	}

	bool CheckReadyPlayers()
	{
		int readyPlayers = 0;
		if (GameController.isTeamMode)
		{
			bool redTeamHasPlayer = false, blueTeamHasPlayer = false;
			for (int i = 0; i < joinCanvas.Length; i++)
			{
				if (joinCanvas[i].HasAssignedPlayer())
				{
					if (joinCanvas[i].state == JoinCanvas.State.Ready)
					{
						readyPlayers++;
						if (joinCanvas[i].player.Team == Team.Blue)
							blueTeamHasPlayer = true;
						else if (joinCanvas[i].player.Team == Team.Red)
							redTeamHasPlayer = true;
					}
					else
					{
						readyPlayers = -100;
					}
				}
			}

			return redTeamHasPlayer && blueTeamHasPlayer && readyPlayers >= 2;
		}
		else
		{
			for (int i = 0; i < joinCanvas.Length; i++)
			{
				if (joinCanvas[i].HasAssignedPlayer())
				{
					if (joinCanvas[i].state == JoinCanvas.State.Ready)
						readyPlayers++;
					else
					{
						readyPlayers = -100;
					}
				}
			}
			return readyPlayers >= 4;
		}
	}

	void ArrangeScoreboards()
	{
		for (int i = 0; i < playerScoreDisplays.Count; i++)
		{
			var p = playerScoreDisplays[i].transform.localPosition;
			//print(p);
			p.y = Mathf.Lerp(p.y, i * -2f, Time.deltaTime * 3f);
			p.z = 0f;
			//print(p);
			playerScoreDisplays[i].transform.localPosition = p;


		}
	}

	void FinishRound()
	{
		if (state == GameState.JoinScreen)
		{
			levelNo = 0;
			foreach (var jc in joinCanvas)
			{
				if (jc.HasAssignedPlayer())
					activePlayers.Add(jc.player);

			}
			SceneManager.LoadScene(levelNames[0]);
		}
		else
		{
			levelNo++;
			//if (levelNo >= 5)
			{
				SceneManager.LoadScene("ScoreScreen");
			}
		}
	}
#endif //#if PHOTON_UNITY_NETWORKING

	void AddPlayer(Player player)
	{
		activePlayers.Add(player);

		var psd = Instantiate(scoreDisplayPrefab, scoreCanvas.transform) as PlayerScoreDisplay;
		psd.player = player;
		psd.color = player.Color;
		psd.text.color = player.Color;
		playerScoreDisplays.Add(psd);

	}


	void SpawnCharacter(Player player)
	{
		var point = Terrain.GetSpawnPoint();
		Character ch = null;
#if PHOTON_UNITY_NETWORKING
		if (PhotonNetwork.IsConnectedAndReady)
		{
			object[] instantiationData = { };
			ch = PhotonNetwork.InstantiateRoomObject(NetworkPrefabs.Character.name, point, Quaternion.identity, 0, instantiationData).GetComponent<Character>();
			print("Net clone!");
		}
		else
#endif
		{
			ch = Instantiate(characterPrefab, point, Quaternion.identity) as Character;
		}

		if (Terrain.GetCharacterParent())
		{
			ch.transform.SetParent(Terrain.GetCharacterParent());
		}


		if (player != null)
		{
			ch.SetPlayer(player);
			player.SetCharacter(ch);
			player.SetSpawnDelay(1f);

			EffectsController.CreateSpawnEffects(point + Vector3.up, player.Color);
			SoundController.PlaySoundEffect("CharacterSpawn", 0.4f, point);
		}
	}

	public static void SpawnCharacterJoinScreen(Player player)
	{
#if PHOTON_UNITY_NETWORKING
		for (int i = 0; i < instance.joinCanvas.Length; i++)
		{
			if (instance.joinCanvas[i].player == player)
			{
				object[] instantiationData = { };
				var ch = PhotonNetwork.InstantiateRoomObject(NetworkPrefabs.Character.name, Terrain.GetSpawnPoint(i), Quaternion.identity, 0, instantiationData).GetComponent<Character>();
				ch.SetPlayer(player);
				player.SetCharacter(ch);
				if (Terrain.GetCharacterParent())
				{
					ch.transform.SetParent(Terrain.GetCharacterParent());
				}
			}
		}
#endif
	}

	Player winningPlayer;

	public static Player lastWinningPlayer { get; private set; }
	public static bool HasInstance { get { return instance != null; } }

	public static Player GetWinningPlayer()
	{
		if (State == GameState.RoundFinished)
		{
			return instance.winningPlayer;
		}
		return null;
	}

	void SortScoreboard()
	{
		if (GameController.isTeamMode)
		{
			redTeamScoreDisplay.player.SetScore(redTeamScore);
			blueTeamScoreDisplay.player.SetScore(blueTeamScore);
		}
		instance.playerScoreDisplays.Sort((x, y) => (y.player.Score * 100 + y.player.SortPriority) - (x.player.Score * 100 + x.player.SortPriority));
	}


	internal static void RegisterKill(Player gotPoint, Player gotKilled, int hits)
	{
		if (State == GameState.RoundFinished)
			return;

		if (isShowDown)
		{
			bool wonRound = false;
			if (activePlayers.Contains(gotKilled))
			{
				activePlayers.Remove(gotKilled);
				if (gotKilled.OffscreenDot != null)
					GameObject.Destroy(gotKilled.OffscreenDot);
			}
			if (activePlayers.Count == 1)
			{
				wonRound = true;
				var winner = activePlayers[0];
				if (winner.Character != null)
					winner.Character.GetComponent<ScorePlum>().ShowText("WIN!", 5f);
				instance.winningPlayer = winner;
				lastWinningPlayer = winner;
			}
			else if (isTeamMode)
			{
				bool winnersContainRed = false;
				bool winnersContainBlue = false;
				foreach (var player in activePlayers)
				{
					if (player.Team == Team.Red)
						winnersContainRed = true;
					else winnersContainBlue = true;
				}

				if (!winnersContainBlue || !winnersContainRed)
				{
					wonRound = true;
					var winner = activePlayers[0];
					if (winner.Character != null)
						winner.Character.GetComponent<ScorePlum>().ShowText("WIN!", 5f);
					instance.winningPlayer = winner;
					lastWinningPlayer = winner;
				}

			}

			if (wonRound)
			{
				SoundController.PlaySoundEffect("VictorySting", 0.5f);
				SoundController.StopMusic();
				instance.state = GameState.RoundFinished;
				var ppc = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
				if (ppc)
				{
					ppc.enabled = false;
				}
			}
			return;
		}

		if (gotPoint != null)
		{
			if (hits <= 0)
				hits = 1;

			if (GameController.isTeamMode)
			{
				if (gotPoint.Team == Team.Blue)
					instance.blueTeamScore += hits;
				else
					instance.redTeamScore += hits;
			}
			else
			{
				gotPoint.SetScore(gotPoint.Score + hits);
			}

			bool wonRound = false;
			if (GameController.isTeamMode)
			{
				wonRound = ((gotPoint.Team == Team.Red && instance.redTeamScore >= 10) || (gotPoint.Team == Team.Blue && instance.blueTeamScore >= 10));
			}
			else
			{
				if (activePlayers.Count == 2)
					wonRound = gotPoint.Score >= 5;
				else
					wonRound = gotPoint.Score >= 10;
			}
			if (wonRound)
			{
				SoundController.PlaySoundEffect("VictorySting", 0.5f);
				SoundController.StopMusic();
				instance.state = GameState.RoundFinished;
				var ppc = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
				if (ppc)
				{
					ppc.enabled = false;
				}
				instance.GetPlayerScoreDisplay(gotPoint).TemorarilyDisplay("WINNER ! ! !", 5f);
				if (gotPoint.Character != null)
					gotPoint.Character.GetComponent<ScorePlum>().ShowText("WIN!", 5f);
				instance.winningPlayer = gotPoint;
				lastWinningPlayer = gotPoint;
			}
			else
			{
				instance.GetPlayerScoreDisplay(gotPoint).TemorarilyDisplay("+" + hits.ToString());
				if (gotPoint.Character != null)
					gotPoint.Character.GetComponent<ScorePlum>().ShowText("+" + hits.ToString());
			}
		}
		else if (gotKilled != null)
		{

			//if (isTeamMode)
			//{
			//    if (gotKilled.team == Team.Blue)
			//        instance.blueTeamScore--;
			//    else
			//        instance.redTeamScore--;
			//}
			//else
			//{
			//    gotKilled.score--;
			//}
			//instance.GetPlayerScoreDisplay(gotKilled).TemorarilyDisplay("-" + 1);
		}

		instance.SortScoreboard();


	}

	PlayerScoreDisplay GetPlayerScoreDisplay(Player player)
	{
		if (isTeamMode)
		{
			if (player.Team == Team.Red)
				return redTeamScoreDisplay;
			else
				return blueTeamScoreDisplay;
		}
		else
		{
			for (int i = 0; i < playerScoreDisplays.Count; i++)
			{
				if (playerScoreDisplays[i].player == player)
					return playerScoreDisplays[i];
			}
		}
		return null;
	}

	bool showGui;

	public void OnGUI()
	{
		if (showGui)
		{
			GUILayout.BeginArea(new Rect(0, 0, 400, 400));
			charactersBounceEachOther = GUILayout.Toggle(charactersBounceEachOther, "Characters Bounce Each Other");
			weirdBounceTrajectories = GUILayout.Toggle(weirdBounceTrajectories, "Weird Bounce Trajectories");
			onlyBounceBeforeRecover = GUILayout.Toggle(onlyBounceBeforeRecover, "Only Bounce Before Recover");
			GUILayout.EndArea();
		}
	}

	public static void ReturnPlayer(Player player)
	{
		activePlayers.Remove(player);
		inactivePlayers.Add(player);
	}


	public static List<Player> GetLeadingPlayers()
	{
		int topScore = -1;
		List<Player> tiedPlayers = new List<Player>();
		foreach (var player in activePlayers)
		{
			if (player.RoundWins == topScore)
			{
				tiedPlayers.Add(player);
			}
			else if (player.RoundWins > topScore)
			{
				topScore = player.RoundWins;
				tiedPlayers.Clear();
				tiedPlayers.Add(player);
			}
		}

		return tiedPlayers;
	}

	public static bool AreAnyPlayersTiedForVictory()
	{
		int topScore = -1;
		List<Player> tiedPlayers = new List<Player>();
		foreach (var player in activePlayers)
		{
			if (player.RoundWins == topScore)
			{
				tiedPlayers.Add(player);
			}
			else if (player.RoundWins > topScore)
			{
				topScore = player.RoundWins;
				tiedPlayers.Clear();
				tiedPlayers.Add(player);
			}
		}
		if (GameController.isTeamMode)
		{
			bool redIsTied = false, blueIsTied = false;
			foreach (var p in tiedPlayers)
			{
				if (p.Team == Team.Red)
					redIsTied = true;
				if (p.Team == Team.Blue)
					blueIsTied = true;
			}
			return redIsTied && blueIsTied;
		}
		else
			return tiedPlayers.Count > 1;

	}

}
