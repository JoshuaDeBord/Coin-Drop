using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using LootLocker;

public class PlayerManager : MonoBehaviour
{
    public ClassicGamemodeLeaderboard classicLeaderBoard;
    public TimedGamemodeLeaderboard timedLeaderBoard;
    public BombsGamemodeLeaderboard bombsLeaderBoard;
    public GameObject signInButton;
    public TMP_InputField playerNameInputField;
    protected string PlayerName;
    public string[] badWords;

    
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        PlayerName = playerNameInputField.text;
        foreach (string word in badWords)
        {
            if (PlayerName.ToLower().Contains(word.ToLower()))
            {
                Debug.Log("Player's Inputed name includes a bad word.");
                PlayerName = "*****";
                break;
            }
        }
        LootLockerSDKManager.SetPlayerName(PlayerName, (response) =>
        {
            if (response.success)
            {

                Debug.Log("Successfully set player name to " + playerNameInputField.text);
            }
            else
            {
                Debug.LogError("Could not set player name" + response.errorData);
            }
        });

    }


   
    public IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return classicLeaderBoard.FetchTopHighscoresRoutine();
        yield return timedLeaderBoard.FetchTopHighscoresRoutine();
        yield return bombsLeaderBoard.FetchTopHighscoresRoutine();
    }


    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                signInButton.SetActive(false);
                done = true;
            }
            else
            {
                signInButton.SetActive(true);
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public void AttemptSignIn()
    {
        StartCoroutine(SetupRoutine());
    }
}
