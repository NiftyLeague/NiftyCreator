using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenController : MonoBehaviour
{
	public PlayerScoreDisplay scoreDisplayPrefab;

	public Canvas scoreCanvas;

	public Text roundText;

	bool isLastRound
	{
		get
		{
			return GameController.levelNo >= GameController.levelNames.Length;
		}
	}


	List<PlayerScoreDisplay> playerScoreDisplays;

	PlayerScoreDisplay redScoreDisplay;
	PlayerScoreDisplay blueScoreDisplay;

	float updateScoresDelay = 1f;
	float continueDelay = 5f;


	void Awake()
	{
		roundText.text = "ROUND " + GameController.levelNo + " OF " + GameController.levelNames.Length;
		playerScoreDisplays = new List<PlayerScoreDisplay>();

		if (GameController.isShowDown)
			updateScoresDelay = 0.05f;

		foreach (var player in GameController.activePlayers)
		{
			if (!GameController.isTeamMode || (player.Team == Team.Red && redScoreDisplay == null) || (player.Team == Team.Blue && blueScoreDisplay == null))
			{
				var psd = Instantiate(scoreDisplayPrefab, scoreCanvas.transform) as PlayerScoreDisplay;
				psd.player = player;
				psd.color = player.Color;
				psd.text.color = player.Color;
				playerScoreDisplays.Add(psd);
				psd.color = player.Color;
				psd.text.text = player.RoundWins.ToString();
				psd.useRoundWins = true;
				if (GameController.isTeamMode)
				{
					if (player.Team == Team.Red)
					{
						redScoreDisplay = psd;
					}
					else
					{
						blueScoreDisplay = psd;
					}
				}
			}
		}

		SortScoreboard();
		ArrangeScoreboard(true);
	}


	void SortScoreboard()
	{
		playerScoreDisplays.Sort((x, y) => (y.player.RoundWins * 100 + y.player.SortPriority) - (x.player.RoundWins * 100 + x.player.SortPriority));
	}


	void ArrangeScoreboard(bool instant)
	{
		for (int i = 0; i < playerScoreDisplays.Count; i++)
		{
			var p = playerScoreDisplays[i].transform.localPosition;
			if (instant)
				p.y = Mathf.Lerp(p.y, i * -2f, 1f);
			else
				p.y = Mathf.Lerp(p.y, i * -2f, Time.deltaTime * 3f);
			p.z = 0f;
			playerScoreDisplays[i].transform.localPosition = p;
		}
	}
	bool haveUpdatedScores;


	void Update()
	{
		if (updateScoresDelay > 0f)
		{
			updateScoresDelay -= Time.deltaTime;
			return;
		}
		if (!haveUpdatedScores)
		{
			haveUpdatedScores = true;
			if (GameController.isTeamMode)
			{
				foreach (var player in GameController.activePlayers)
				{
					if (player.Team == GameController.lastWinningPlayer.Team)
					{
						player.SetRoundWins(player.RoundWins + 1);
					}
				}
			}
			foreach (var psd in playerScoreDisplays)
			{
				if (GameController.isTeamMode)
				{
					if (psd.player.Team == GameController.lastWinningPlayer.Team)
					{
						if (GameController.isShowDown)
							psd.TemorarilyDisplay("WINNER!", 10f);
						else
							psd.TemorarilyDisplay(psd.player.RoundWins.ToString());
					}
				}
				else
				{
					if (psd.player == GameController.lastWinningPlayer)
					{
						psd.player.SetRoundWins(psd.player.RoundWins + 1);
						if (GameController.isShowDown)
							psd.TemorarilyDisplay("WINNER!", 10f);
						else
							psd.TemorarilyDisplay(psd.player.RoundWins.ToString());
					}
				}
			}

			SortScoreboard();

			if (isLastRound && GameController.AreAnyPlayersTiedForVictory())
			{
				roundText.text = "SHOWDOWN ! ! ! ";
				GameController.isShowDown = true;
			}


		}
		ArrangeScoreboard(false);
		if (isLastRound && GameController.AreAnyPlayersTiedForVictory())
		{
			roundText.color = Time.time % 0.2 < 0.1f ? Color.white : Color.black;
		}

		continueDelay -= Time.deltaTime;
		if (continueDelay <= 0f)
		{
			if (isLastRound && GameController.AreAnyPlayersTiedForVictory())
			{
				GameController.SetupPlayersForShowdown();
				UnityEngine.SceneManagement.SceneManager.LoadScene("7Showdown");
			}
			else if (isLastRound)
			{
				GameController.overallWinnerColor = playerScoreDisplays[0].color;
				UnityEngine.SceneManagement.SceneManager.LoadScene("Outro");
			}
			else
			{
				if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Test" || true)
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene(GameController.nextLevelIndex);
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene(GameController.levelNames[GameController.levelNo]);
				}
			}
		}
	}
}
