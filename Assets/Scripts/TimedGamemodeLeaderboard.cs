using LootLocker.Requests;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimedGamemodeLeaderboard : MonoBehaviour
{
    string TimedLeaderBoardKey = "TimedKey";
    public TextMeshProUGUI[] TimedPlayerNames;
    public TextMeshProUGUI[] TimedPlayerScores;
    public TextMeshProUGUI TimedNamesText;
    public TextMeshProUGUI TimedScoresText;
    public TextMeshProUGUI TimedPersonalScore;
    public TextMeshProUGUI TimedPersonalName;
    public TextMeshProUGUI TimedHighScoretext;
    public TextMeshProUGUI TimedYouText;
    public GameObject TimedAlignBox;
    public float TimedYouTextOffsetNumber;
    public int TimedRankOnleaderboard;

    public PlayerManager playerManager;


    void Start()
    {

        TimedAlignBox.SetActive(false);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, TimedLeaderBoardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.LogWarning("Failed" + response.errorData);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
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

                if (response.rank > 5)
                {
                    TimedHighScoretext.text = "Your High Score:";
                    TimedPersonalName.text = $"{response.rank}. {response.player.name}";
                    TimedPersonalScore.text = response.score.ToString("N0");


                    TimedPlayerNames[0].color = Color.black;
                    TimedPlayerScores[0].color = Color.black;

                    TimedPlayerNames[1].color = Color.black;
                    TimedPlayerScores[1].color = Color.black;

                    TimedPlayerNames[2].color = Color.black;
                    TimedPlayerScores[2].color = Color.black;

                    TimedPlayerNames[3].color = Color.black;
                    TimedPlayerScores[3].color = Color.black;

                    TimedPlayerNames[4].color = Color.black;
                    TimedPlayerScores[4].color = Color.black;

                    TimedYouText.gameObject.SetActive(false);
                }
                else
                {
                    TimedRankOnleaderboard = response.rank;
                    TimedHighScoretext.text = string.Empty;
                    TimedPersonalScore.text = string.Empty;
                    TimedPersonalName.text = string.Empty;
                    if (response.rank > 1)
                        TimedYouText.gameObject.SetActive(true);
                    StartCoroutine(MatchLeaderboardRankToPlayer());
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

    public IEnumerator MatchLeaderboardRankToPlayer()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(TimedLeaderBoardKey, 5, 0, (response) =>
        {


            if (response.success)
            {
                try
                {
                    done = true;
                    Debug.Log("Successfully retrieved the leaderboard ranks");

                    LootLockerLeaderboardMember[] members = response.items;

                    if (TimedRankOnleaderboard == members[0].rank)
                    {
                        foreach (TextMeshProUGUI tmp in TimedPlayerNames) { tmp.color = Color.black; }
                        foreach (TextMeshProUGUI tmp in TimedPlayerScores) { tmp.color = Color.black; };
                        TimedPlayerNames[0].color = Color.blue;
                        TimedPlayerScores[0].color = Color.blue;
                        TimedYouText.gameObject.transform.localPosition = new(TimedAlignBox.transform.localPosition.x, TimedPlayerNames[0].transform.localPosition.y + TimedYouTextOffsetNumber, transform.localPosition.z);
                    }

                    if (TimedRankOnleaderboard == members[1].rank)
                    {
                        foreach (TextMeshProUGUI tmp in TimedPlayerNames) { tmp.color = Color.black; }
                        foreach (TextMeshProUGUI tmp in TimedPlayerScores) { tmp.color = Color.black; };
                        TimedPlayerNames[1].color = Color.blue;
                        TimedPlayerScores[1].color = Color.blue;
                        TimedYouText.gameObject.transform.localPosition = new(TimedAlignBox.transform.localPosition.x, TimedPlayerNames[1].transform.localPosition.y + TimedYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (TimedRankOnleaderboard == members[2].rank)
                    {
                        foreach (TextMeshProUGUI tmp in TimedPlayerNames) { tmp.color = Color.black; }
                        foreach (TextMeshProUGUI tmp in TimedPlayerScores) { tmp.color = Color.black; };
                        TimedPlayerNames[2].color = Color.blue;
                        TimedPlayerScores[2].color = Color.blue;
                        TimedYouText.gameObject.transform.localPosition = new(TimedAlignBox.transform.localPosition.x, TimedPlayerNames[2].transform.localPosition.y + TimedYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (TimedRankOnleaderboard == members[3].rank)
                    {
                        foreach (TextMeshProUGUI tmp in TimedPlayerNames) { tmp.color = Color.black; }
                        foreach (TextMeshProUGUI tmp in TimedPlayerScores) { tmp.color = Color.black; };
                        TimedPlayerNames[3].color = Color.blue;
                        TimedPlayerScores[3].color = Color.blue;
                        TimedYouText.gameObject.transform.localPosition = new(TimedAlignBox.transform.localPosition.x, TimedPlayerNames[3].transform.localPosition.y + TimedYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (TimedRankOnleaderboard == members[4].rank)
                    {
                        foreach (TextMeshProUGUI tmp in TimedPlayerNames) { tmp.color = Color.black; }
                        foreach (TextMeshProUGUI tmp in TimedPlayerScores) { tmp.color = Color.black; };
                        TimedPlayerNames[4].color = Color.blue;
                        TimedPlayerScores[4].color = Color.blue;
                        TimedYouText.gameObject.transform.localPosition = new(TimedAlignBox.transform.localPosition.x, TimedPlayerNames[4].transform.localPosition.y + TimedYouTextOffsetNumber, transform.localPosition.z);
                    }
                }
                catch { }
            }
            else
            {
                Debug.LogWarning("Failed to set the player's position on the leaderboard!");
                done = true;
            }

        });
        yield return new WaitWhile(() => done == false);
    }
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(TimedLeaderBoardKey, 5, 0, (response) =>
        {

            if (response.success)
            {
                Debug.Log("SUCCESSFULLY FETCHED LEADERBOARD INFORMATION FOR THE TIMED GAMEMODE!");
                playerManager.signInButton.SetActive(false);

                LootLockerLeaderboardMember[] members = response.items;




                TimedNamesText.text = "Names:";
                TimedScoresText.text = "Scores:";
                try
                {
                    if (members[0].player.name != string.Empty)
                    { try { TimedPlayerNames[0].text = "1. " + members[0].player.name; } catch { } }
                    else { try { TimedPlayerNames[0].text = "1. " + members[0].player.id; } catch { } }

                    if (members[1].player.name != string.Empty)
                    { try { TimedPlayerNames[1].text = "2. " + members[1].player.name; } catch { } }
                    else { try { TimedPlayerNames[1].text = "2. " + members[1].player.id; } catch { } }

                    if (members[2].player.name != string.Empty)
                    { try { TimedPlayerNames[2].text = "3. " + members[2].player.name; } catch { } }
                    else { try { TimedPlayerNames[2].text = "3. " + members[2].player.id; } catch { } }

                    if (members[3].player.name != string.Empty)
                    { try { TimedPlayerNames[3].text = "4. " + members[3].player.name; } catch { } }
                    else { try { TimedPlayerNames[3].text = "4. " + members[3].player.id; } catch { } }

                    if (members[4].player.name != string.Empty)
                    { try { TimedPlayerNames[4].text = "5. " + members[4].player.name; } catch { } }
                    else { try { TimedPlayerNames[4].text = "5. " + members[4].player.id; } catch { } }
                }
                catch { }

                try
                {
                    TimedPlayerScores[0].text = members[0].score.ToString("N0");
                    TimedPlayerScores[1].text = members[1].score.ToString("N0");
                    TimedPlayerScores[2].text = members[2].score.ToString("N0");
                    TimedPlayerScores[3].text = members[3].score.ToString("N0");
                    TimedPlayerScores[4].text = members[4].score.ToString("N0");
                }
                catch
                {

                }


                done = true;

                StartCoroutine(GetPlayerHighScore());
            }
            else
            {
                Debug.LogWarning("Failed" + response.errorData);
                TimedNamesText.text = "Failed To Load...";
                TimedScoresText.text = "Try Again...";
                foreach (TextMeshProUGUI tmp in TimedPlayerNames)
                {
                    tmp.text = string.Empty;
                }
                foreach (TextMeshProUGUI tmp in TimedPlayerScores)
                {
                    tmp.text = string.Empty;
                }
                TimedYouText.gameObject.SetActive(false);
                playerManager.signInButton.SetActive(true);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void RefreshLeaderboard()
    {
        StartCoroutine(FetchTopHighscoresRoutine());
    }

    
}
