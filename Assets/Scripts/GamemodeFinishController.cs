using LootLocker.Requests;
using System.Collections;
using TMPro;
using UnityEngine;

public class GamemodeFinishController : MonoBehaviour
{
    public int numberOf5 = 0;
    public int numberOf10 = 0;
    public int numberOf15 = 0;
    public int numberOf20 = 0;
    public int finalScore = 0;
    public int rankOnLeaderboard = 0;

    string TimedLeaderBoardKey = "TimedKey";

    public TextMeshProUGUI information;

    public GameObject informationScreen;

    public GameModesController gameModeController;
    public GameManager gameManager;
    public Timer timer;

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

    public void OpenScoreBoard()
    {
        StartCoroutine(OpenBoard());

    }

    public IEnumerator OpenBoard()
    {
        yield return GetPlayerHighScore();
    }

    public void CloseScoreBoard()
    {

        numberOf5 = new();
        numberOf10 = new();
        numberOf15 = new();
        numberOf20 = new();
        rankOnLeaderboard = new();
        finalScore = new();
        information.text = FinalInformation();
        informationScreen.SetActive(false);
        Restart();
    }

    public void Restart()
    {
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

    public IEnumerator GetPlayerHighScore()
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

                foreach(ParticleSystem particle in finishParticles)
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