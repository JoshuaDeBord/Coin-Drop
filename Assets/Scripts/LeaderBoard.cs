using LootLocker.Requests;
using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    string leaderBoardKey = "GlobalHighScores";
    public TextMeshProUGUI[] playerNames;
    public TextMeshProUGUI[] playerScores;
    public TextMeshProUGUI namesText;
    public TextMeshProUGUI scoresText;
    public TextMeshProUGUI personalScore;
    public TextMeshProUGUI personalName;
    public TextMeshProUGUI highScoretext;
    public TextMeshProUGUI youText;
    private GameObject alignBox;
    public float youTextOffsetNumber;

    public int rankOnleaderboard;

    void Start()
    {
        alignBox = GameObject.Find("YoutextAlignBox");
        alignBox.SetActive(false);
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

    public IEnumerator GetPlayerHighScore()
    {
        bool done = false;

        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.GetMemberRank(leaderBoardKey, playerID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player's highscore!");

                if (response.rank > 5)
                {
                    highScoretext.text = "Your High Score:";
                    personalName.text = $"{response.rank}. {response.player.name}";
                    personalScore.text = response.score.ToString("N0");


                    playerNames[0].color = Color.black;
                    playerScores[0].color = Color.black;

                    playerNames[1].color = Color.black;
                    playerScores[1].color = Color.black;

                    playerNames[2].color = Color.black;
                    playerScores[2].color = Color.black;

                    playerNames[3].color = Color.black;
                    playerScores[3].color = Color.black;

                    playerNames[4].color = Color.black;
                    playerScores[4].color = Color.black;

                    youText.gameObject.SetActive(false);
                }
                else
                {
                    rankOnleaderboard = response.rank;
                    highScoretext.text = string.Empty;
                    personalScore.text = string.Empty;
                    personalName.text = string.Empty;
                    youText.gameObject.SetActive(true);
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
        LootLockerSDKManager.GetScoreList(leaderBoardKey, 5, 0, (response) =>
        {

            if (response.success)
            {
                done = true;
                Debug.Log("Successfully retrieved the leaderboard ranks");

                LootLockerLeaderboardMember[] members = response.items;

                if (rankOnleaderboard == members[0].rank)
                {
                    playerNames[0].color = Color.blue;
                    playerScores[0].color = Color.blue;
                    youText.gameObject.transform.localPosition = new(alignBox.transform.localPosition.x, playerNames[0].transform.localPosition.y + youTextOffsetNumber, transform.localPosition.z);
                }
                if (rankOnleaderboard == members[1].rank)
                {
                    playerNames[1].color = Color.blue;
                    playerScores[1].color = Color.blue;
                    youText.gameObject.transform.localPosition = new(alignBox.transform.localPosition.x, playerNames[1].transform.localPosition.y + youTextOffsetNumber, transform.localPosition.z);
                }
                if (rankOnleaderboard == members[2].rank)
                {
                    playerNames[2].color = Color.blue;
                    playerScores[2].color = Color.blue;
                    youText.gameObject.transform.localPosition = new(alignBox.transform.localPosition.x, playerNames[2].transform.localPosition.y + youTextOffsetNumber, transform.localPosition.z);
                }
                if (rankOnleaderboard == members[3].rank)
                {
                    playerNames[3].color = Color.blue;
                    playerScores[3].color = Color.blue;
                    youText.gameObject.transform.localPosition = new(alignBox.transform.localPosition.x, playerNames[3].transform.localPosition.y + youTextOffsetNumber, transform.localPosition.z);
                }
                if (rankOnleaderboard == members[4].rank)
                {
                    playerNames[4].color = Color.blue;
                    playerScores[4].color = Color.blue;
                    youText.gameObject.transform.localPosition = new(alignBox.transform.localPosition.x, playerNames[4].transform.localPosition.y + youTextOffsetNumber, transform.localPosition.z);
                }
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
        LootLockerSDKManager.GetScoreList(leaderBoardKey, 5, 0, (response) =>
        {

            if (response.success)
            {


                LootLockerLeaderboardMember[] members = response.items;




                namesText.text = "Names:";
                scoresText.text = "Scores:";
                try
                {
                    if (members[0].player.name != string.Empty)
                    { try { playerNames[0].text = "1. " + members[0].player.name; } catch { } }
                    else { try { playerNames[0].text = "1. " + members[0].player.id; } catch { } }

                    if (members[1].player.name != string.Empty)
                    { try { playerNames[1].text = "2. " + members[1].player.name; } catch { } }
                    else { try { playerNames[1].text = "2. " + members[1].player.id; } catch { } }

                    if (members[2].player.name != string.Empty)
                    { try { playerNames[2].text = "3. " + members[2].player.name; } catch { } }
                    else { try { playerNames[2].text = "3. " + members[2].player.id; } catch { } }

                    if (members[3].player.name != string.Empty)
                    { try { playerNames[3].text = "4. " + members[3].player.name; } catch { } }
                    else { try { playerNames[3].text = "4. " + members[3].player.id; } catch { } }

                    if (members[4].player.name != string.Empty)
                    { try { playerNames[4].text = "5. " + members[4].player.name; } catch { } }
                    else { try { playerNames[4].text = "5. " + members[4].player.id; } catch { } }
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
                catch
                {

                }


                done = true;

                StartCoroutine(GetPlayerHighScore());
            }
            else
            {
                Debug.LogWarning("Failed" + response.errorData);
                namesText.text = "Failed To Load...";
                scoresText.text = "Try Again...";
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
