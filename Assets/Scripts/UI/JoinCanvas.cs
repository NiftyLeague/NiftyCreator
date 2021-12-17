
using UnityEngine;
using UnityEngine.UI;

#if PHOTON_UNITY_NETWORKING
using Hashtable = ExitGames.Client.Photon.Hashtable;
#endif


public class JoinCanvas : MonoBehaviour
{
	public enum State
	{
		Searching,
		ChooseCharacter,
		Ready
	}

	public float colorUpdateSpeed;
	public Transform effectPos;
	public Canvas statusCanvas;
	public Canvas chooseCharacterCanvas;
	public Canvas backPromptCanvas;
	public GameObject teamChangeColorObject;
	public Text pingText;
	public Text statusText;
	public Text changeCharacterText;
	public Image changeCharacterImage;
	public Text[] texts;
	public Player player;
	public State state;
	public Color disabledColor;

	public Canvas buttons;
	public Image aButton, bButton, xButton, yButton, leftButton, rightButton, upButton, downButton;


	InputState input = new InputState();
	float colorLerpAmount;
	bool hasMultipleCharacters = true;
	private int playerIndex = 0;
	private int broIndex = 0;

#if PHOTON_UNITY_NETWORKING
	void Update()
	{
		foreach (var text in texts)
		{
			if (!hasMultipleCharacters && text == changeCharacterText || statusText.text == "READY!")
			{
				continue;
			}
			text.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time * colorUpdateSpeed, 1f));
		}

		if (buttons.enabled != NetworkController.debug || pingText.enabled != NetworkController.debug)
		{
			buttons.enabled = NetworkController.debug;
			pingText.enabled = NetworkController.debug;
		}

		if (HasAssignedPlayer())
		{
			if (player.networkPlayer.IsLocal)
			{
				UpdateUnreadyPlayer();
			}
			else
			{
				UpdateUnreadyNetworkPlayer();
			}

			if (player.Character)
			{
				UpdatePlayerBro();
			}

			if (pingText.enabled && player.networkPlayer.CustomProperties.ContainsKey("ping"))
			{
				pingText.text = $"PING: {player.networkPlayer.CustomProperties["ping"]}ms";
			}
			if (player.Input != null && buttons.enabled)
			{
				UpdatePlayerInputVisuals();
			}
		}
	}

	private void UpdatePlayerInputVisuals()
	{
		aButton.enabled = player.Input.aButton;
		bButton.enabled = player.Input.bButton;
		xButton.enabled = player.Input.xButton;
		yButton.enabled = player.Input.yButton;
		upButton.enabled = player.Input.up;
		downButton.enabled = player.Input.down;
		leftButton.enabled = player.Input.left;
		rightButton.enabled = player.Input.right;
	}

	private void UpdateUnreadyNetworkPlayer()
	{
		if (player.networkPlayer.CustomProperties.ContainsKey("ls"))
		{
			State s = (State)player.networkPlayer.CustomProperties["ls"];
			if (state != s)
			{
				switch (s)
				{
				case State.Searching:
					UnassignPlayer();
					break;
				case State.ChooseCharacter:
					player.Character.SetActive(false);
					chooseCharacterCanvas.enabled = false;
					backPromptCanvas.enabled = false;
					statusCanvas.enabled = true;
					statusText.text = "GETTING READY";

					player.Character.transform.position = Terrain.GetSpawnPoint(playerIndex);
					break;
				case State.Ready:
					player.Character.SetActive(true);
					chooseCharacterCanvas.enabled = false;
					backPromptCanvas.enabled = false;
					statusText.text = "READY!";
					statusText.color = Color.white;
					break;
				}
				state = s;
			}
		}
	}

	private void UpdateUnreadyPlayer()
	{
		switch (state)
		{
		case State.ChooseCharacter:
			InputReader.GetInput(input);
			if (GameController.isTeamMode)
			{
				/*if (input.PressedB)
				{
					if (assignedPlayer.Team == Team.Red)
					{
						assignedPlayer.SetTeam(Team.Blue);
					}
					else
					{
						assignedPlayer.SetTeam(Team.Red);
					}

					colorLerpAmount = Random.Range(0f, 0.7f);
					if (assignedPlayer.Team == Team.Red)
					{
						color = Color.Lerp(Color.red, Color.white, colorLerpAmount);
					}
					else
					{
						color = Color.Lerp(Color.blue, Color.white, colorLerpAmount);
					}

					assignedPlayer.SetColor(color);
					EffectsController.CreateSpawnEffects(effectPos.position, color);
					SoundController.PlaySoundEffect("CharacterSpawn", 0.3f, effectPos.position);
				}
				if (input.left)
				{
					colorLerpAmount = Mathf.MoveTowards(colorLerpAmount, 0f, Time.deltaTime * 0.5f);
				}
				else if (input.right)
				{
					colorLerpAmount = Mathf.MoveTowards(colorLerpAmount, 0.7f, Time.deltaTime * 0.5f);
				}
				color = Color.Lerp(assignedPlayer.Team == Team.Red ? Color.red : Color.blue, Color.white, colorLerpAmount);
				assignedPlayer.SetColor(color);*/
			}
			else
			{
				if (hasMultipleCharacters && input.PressedB)
				{
					EffectsController.CreateSpawnEffects(effectPos.position, player.Color);
					SoundController.PlaySoundEffect("CharacterSpawn", 0.3f, effectPos.position);
					player.Character.transform.position = Terrain.GetSpawnPoint(playerIndex);
					SetBroIndex((broIndex + 1) % NiftyUsers.me.bros.Count);
				}
			}

			if (input.ReleasedA)
			{
				SetState(State.Ready);
				player.Character.SetActive(true);
				chooseCharacterCanvas.enabled = false;
				backPromptCanvas.enabled = true;
			}
			break;
		case State.Ready:
			InputReader.GetInput(input);
			if (input.PressedY)
			{
				player.Character.SetActive(false);
				player.ClearInput();
				SetState(State.ChooseCharacter);
				chooseCharacterCanvas.enabled = true;
				backPromptCanvas.enabled = false;
				player.Character.transform.position = Terrain.GetSpawnPoint(playerIndex);
			}
			break;
		}
	}
	public void AssignPlayer(Player player, int playerIndex)
	{
		this.playerIndex = playerIndex;
		SetState(State.ChooseCharacter);
		statusCanvas.enabled = !player.networkPlayer.IsLocal;
		chooseCharacterCanvas.enabled = player.networkPlayer.IsLocal;
		buttons.enabled = true;
		this.player = player;
		statusText.text = "GETTING READY";

		if (GameController.isTeamMode)
		{
			/*teamChangeColorObject.SetActive(true);
			changeCharacterText.text = "CHANGE TEAM";

			player.SetTeam(Random.value < 0.5f ? Team.Red : Team.Blue);
			if (player.Team == Team.Red)
			{
				color = Color.Lerp(Color.red, Color.white, Random.Range(0f, 0.7f));
			}
			else
			{
				color = Color.Lerp(Color.blue, Color.white, Random.Range(0f, 0.7f));
			}*/
		}
		else
		{
			teamChangeColorObject.SetActive(false);
			changeCharacterText.text = "CHANGE CHARACTER";
		}

		EffectsController.CreateSpawnEffects(effectPos.position, this.player.Color);
		SoundController.PlaySoundEffect("CharacterSpawn", 0.3f, effectPos.position);
		if (player.networkPlayer.IsLocal && NiftyUsers.me.bros.Count <= 1)
		{
			hasMultipleCharacters = false;
			changeCharacterImage.color = disabledColor;
			changeCharacterText.color = disabledColor;
		}
		else
		{
			hasMultipleCharacters = true;
		}

		if (player.networkPlayer.IsLocal)
		{
			this.player.ReadInput();
		}
	}

	public void UpdatePlayerBro(bool force = false)
	{
		int broIndex = (int)player.networkPlayer.CustomProperties["broIdx"];
		if (broIndex != this.broIndex || force)
		{
			string[] bros = player.networkPlayer.CustomProperties["bros"] as string[];
			SetBro(NiftyUsers.GetNiftyBro(bros[broIndex]));
			this.broIndex = broIndex;
		}
	}

	private void SetBro(NiftyBro bro)
	{
		player.SetBro(bro);
		//spawn effect
	}

	public void UnassignPlayer()
	{
		//GameController.ReturnPlayer(assignedPlayer);
		if (player.Character != null)
		{
			Destroy(player.Character.gameObject);
		}
		SetState(State.Searching);
		statusCanvas.enabled = true;
		chooseCharacterCanvas.enabled = false;
		backPromptCanvas.enabled = false;
		player = null;
		statusText.text = "SEARCHING";
		buttons.enabled = false;
	}

	public bool HasAssignedPlayer()
	{
		return player != null && player.networkPlayer != null;
	}

	private void SetState(State state)
	{
		if (player != null && player.networkPlayer != null && state != this.state)
		{
			player.networkPlayer.SetCustomProperties(new Hashtable { { "ls", (int)state } });
		}
		this.state = state;
	}

	private void SetBroIndex(int newIndex)
	{
		if (player != null && player.networkPlayer != null && broIndex != newIndex)
		{
			player.networkPlayer.SetCustomProperties(new Hashtable { { "broIdx", newIndex } });
		}
	}
#endif //#if PHOTON_UNITY_NETWORKING
}