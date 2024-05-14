using LootLocker.Requests;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamemodeFinishController : MonoBehaviour
{
    public int numberOf5 = 0;
    public int numberOf10 = 0;
    public int numberOf15 = 0;
    public int numberOf20 = 0;
    public int finalScore = 0;
    public int rankOnLeaderboard = 0;

    const string TimedLeaderBoardKey = "TimedKey";
    const string BombLeaderBoardKey = "BombKey";

    public TextMeshProUGUI information;

    public GameObject informationScreen;
    public GameObject restartScreenButton;
    
    public GameModesController gameModeController;
    public GameManager gameManager;
    public BombsGamemodeController bombsGamemodeController;
    public Timer timer;
    public RespawnCoin respawnCoin;

    public ParticleSystem[] finishParticles;

    public string FinalInformation()
    {
        return $"Final Score: {finalScore}" +
        $"\nRank on Leaderboard: {rankOnLeaderboard} " +
        $"\n# of 5  =  {numberOf5}" +
        $"\n# of 10 = {numberOf10}" +
        $"\n# of 15 = {numberOf15}" +
        $"\n# of 20 = {numberOf20}";

    }

    private void Update()
    {
        if (gameModeController.chosenGamemode == 1 && gameModeController.timerStarted == false || gameModeController.chosenGamemode == 2 && informationScreen.activeInHierarchy == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
        }
    }

    public void OpenBombsFinishBoard()
    {
        StartCoroutine(OpenBombsBoard());
    }
    public void OpenTimedFinishBoard()
    {
        StartCoroutine(OpenTimedBoard());

    }

    public IEnumerator OpenTimedBoard()
    {
        EventSystem.current.SetSelectedGameObject(restartScreenButton.gameObject);
        yield return GetPlayerTimedHighScore();
    }

    public IEnumerator OpenBombsBoard()
    {
        EventSystem.current.SetSelectedGameObject(restartScreenButton.gameObject);
        yield return GetPlayerBombHighScore();
    }


    public void RestartGamemode()
    {
        if (gameModeController.chosenGamemode == 2)
        {
            bombsGamemodeController.livesLeft = 3;
            bombsGamemodeController.livesLeftText.text = "3     left";
            informationScreen.SetActive(false);
            numberOf5 = new();
            numberOf10 = new();
            numberOf15 = new();
            numberOf20 = new();
            rankOnLeaderboard = new();
            finalScore = new();
            information.text = FinalInformation();
            gameManager.bombsSavedPoints = 0;
            timer.leftButton.gameObject.SetActive(true);
            timer.rightButton.gameObject.SetActive(true);
            timer.startbutton.gameObject.SetActive(true);
            respawnCoin.RestartGame();
            bombsGamemodeController.StartGamemode();
            StartCoroutine(SelectStartButton());
        }
        else if (gameModeController.chosenGamemode == 1)
        {
            informationScreen.SetActive(false);
            numberOf5 = new();
            numberOf10 = new();
            numberOf15 = new();
            numberOf20 = new();
            rankOnLeaderboard = new();
            finalScore = new();
            information.text = FinalInformation();
            gameManager.timedSavedPoints = 0;
            timer.countdownTMP.text = "1:00";
            timer.seconds = 60;
            timer.controller.timerStarted = false;
            timer.leftButton.gameObject.SetActive(true);
            timer.rightButton.gameObject.SetActive(true);
            timer.startbutton.gameObject.SetActive(true);
            respawnCoin.RestartGame();
            
            StartCoroutine(SelectStartButton());
        }
    }

    public IEnumerator SelectStartButton()
    {
        yield return new WaitForSeconds(0.2f);
        gameManager.PI.SwitchCurrentActionMap("Player");
        EventSystem.current.SetSelectedGameObject(gameModeController.startButton.gameObject);
    }
    public void Add5()
    {
        if (gameModeController.chosenGamemode > 0)
            numberOf5++;
    }
    public void Add10()
    {
        if (gameModeController.chosenGamemode > 0)
            numberOf10++;
    }
    public void Add15()
    {
        if (gameModeController.chosenGamemode > 0)
            numberOf15++;
    }
    public void Add20()
    {
        if (gameModeController.chosenGamemode > 0)
            numberOf20++;
    }

    public IEnumerator GetPlayerBombHighScore()
    {
        bool done = false;

        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.GetMemberRank(BombLeaderBoardKey, playerID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player's highscore!");

                rankOnLeaderboard = response.rank;
                finalScore = gameManager.bombsSavedPoints;
                information.text = FinalInformation();
                informationScreen.SetActive(true);

                foreach (ParticleSystem particle in finishParticles)
                {
                    particle.Play();
                }
            }


            else
            {
                Debug.LogWarning("Failed to retrieve player's rank!");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator GetPlayerTimedHighScore()
    {
        bool done = false;
        
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.GetMemberRank(TimedLeaderBoardKey, playerID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player's highscore!");

                rankOnLeaderboard = response.rank;
                finalScore = gameManager.timedSavedPoints;
                information.text = FinalInformation();
                informationScreen.SetActive(true);
                

                foreach (ParticleSystem particle in finishParticles)
                {
                    particle.Play();
                }
            }


            else
            {
                Debug.LogWarning("Failed to retrieve player's rank!");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}