using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using LootLocker;

using TMPro;
using System.Linq.Expressions;

public class LeaderBoard : MonoBehaviour
{
    string leaderBoardKey = "GlobalHighScores";
    public TextMeshProUGUI[] playerNames;
    public TextMeshProUGUI[] playerScores;
    public TextMeshProUGUI namesText;
    public TextMeshProUGUI scoresText;
    void Start()
    {
        
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderBoardKey, (response) =>
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

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderBoardKey, 4, 0, (response) =>
        {

        if (response.success)
        {


            LootLockerLeaderboardMember[] members = response.items;

                


            namesText.text = "Names:";
            scoresText.text = "Scores:";
                try
                {
                    if (members[0].player.name != string.Empty)
                    { try { playerNames[0].text = "1. " + members[0].player.name; } catch {} }
                    else { try { playerNames[0].text = "1. " + members[0].player.id; } catch {} }

                    if (members[1].player.name != string.Empty)
                    { try { playerNames[1].text = "2. " + members[1].player.name; } catch {} }
                    else { try { playerNames[1].text = "2. " + members[1].player.id; } catch {} }

                    if (members[2].player.name != string.Empty)
                    { try { playerNames[2].text = "3. " + members[2].player.name; } catch {} }
                    else { try { playerNames[2].text = "3. " + members[2].player.id; } catch {} }

                    if (members[3].player.name != string.Empty)
                    { try { playerNames[3].text = "4. " + members[3].player.name; } catch {} }
                    else { try { playerNames[3].text = "4. " + members[3].player.id; } catch {} }

                    if (members[4].player.name != string.Empty)
                    { try { playerNames[4].text = "5. " + members[4].player.name; } catch {} }
                    else { try { playerNames[4].text = "5. " + members[4].player.id; } catch {} }
                }
                catch { }

                try
                {
                    playerScores[0].text = members[0].score.ToString("N0");
                    playerScores[1].text = members[1].score.ToString("N0");
                    playerScores[2].text = members[2].score.ToString("N0");
                    playerScores[3].text = members[3].score.ToString("N0");
                    playerScores[4].text = members[4].score.ToString("N0");
                }
                catch { }

                
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

    public void RefreshLeaderboard()
    {
        StartCoroutine(FetchTopHighscoresRoutine());
    }
}
