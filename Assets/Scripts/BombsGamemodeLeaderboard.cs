using LootLocker.Requests;
using System.Collections;
using TMPro;
using UnityEngine;

public class BombsGamemodeLeaderboard : MonoBehaviour
{
    string BombLeaderBoardKey = "BombKey";
    public TextMeshProUGUI[] BombPlayerNames;
    public TextMeshProUGUI[] BombPlayerScores;
    public TextMeshProUGUI BombNamesText;
    public TextMeshProUGUI BombScoresText;
    public TextMeshProUGUI BombPersonalScore;
    public TextMeshProUGUI BombPersonalName;
    public TextMeshProUGUI BombHighScoretext;
    public TextMeshProUGUI BombYouText;
    public GameObject BombAlignBox;
    public float BombYouTextOffsetNumber;
    public int BombRankOnleaderboard;

    void Start()
    {

        BombAlignBox.SetActive(false);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, BombLeaderBoardKey, (response) =>
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
        LootLockerSDKManager.GetMemberRank(BombLeaderBoardKey, playerID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player's highscore!");

                if (response.rank > 5)
                {
                    BombHighScoretext.text = "Your High Score:";
                    BombPersonalName.text = $"{response.rank}. {response.player.name}";
                    BombPersonalScore.text = response.score.ToString("N0");


                    BombPlayerNames[0].color = Color.black;
                    BombPlayerScores[0].color = Color.black;

                    BombPlayerNames[1].color = Color.black;
                    BombPlayerScores[1].color = Color.black;

                    BombPlayerNames[2].color = Color.black;
                    BombPlayerScores[2].color = Color.black;

                    BombPlayerNames[3].color = Color.black;
                    BombPlayerScores[3].color = Color.black;

                    BombPlayerNames[4].color = Color.black;
                    BombPlayerScores[4].color = Color.black;

                    BombYouText.gameObject.SetActive(false);
                }
                else
                {
                    BombRankOnleaderboard = response.rank;
                    BombHighScoretext.text = string.Empty;
                    BombPersonalScore.text = string.Empty;
                    BombPersonalName.text = string.Empty;
                    if (response.rank > 1)
                        BombYouText.gameObject.SetActive(true);
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
        LootLockerSDKManager.GetScoreList(BombLeaderBoardKey, 5, 0, (response) =>
        {

            if (response.success)
            {
                try
                {
                    done = true;
                    Debug.Log("Successfully retrieved the leaderboard ranks");

                    LootLockerLeaderboardMember[] members = response.items;

                    if (BombRankOnleaderboard == members[0].rank)
                    {
                        BombPlayerNames[0].color = Color.blue;
                        BombPlayerScores[0].color = Color.blue;
                        BombYouText.gameObject.transform.localPosition = new(BombAlignBox.transform.localPosition.x, BombPlayerNames[0].transform.localPosition.y + BombYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (BombRankOnleaderboard == members[1].rank)
                    {
                        BombPlayerNames[1].color = Color.blue;
                        BombPlayerScores[1].color = Color.blue;
                        BombYouText.gameObject.transform.localPosition = new(BombAlignBox.transform.localPosition.x, BombPlayerNames[1].transform.localPosition.y + BombYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (BombRankOnleaderboard == members[2].rank)
                    {
                        BombPlayerNames[2].color = Color.blue;
                        BombPlayerScores[2].color = Color.blue;
                        BombYouText.gameObject.transform.localPosition = new(BombAlignBox.transform.localPosition.x, BombPlayerNames[2].transform.localPosition.y + BombYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (BombRankOnleaderboard == members[3].rank)
                    {
                        BombPlayerNames[3].color = Color.blue;
                        BombPlayerScores[3].color = Color.blue;
                        BombYouText.gameObject.transform.localPosition = new(BombAlignBox.transform.localPosition.x, BombPlayerNames[3].transform.localPosition.y + BombYouTextOffsetNumber, transform.localPosition.z);
                    }
                    if (BombRankOnleaderboard == members[4].rank)
                    {
                        BombPlayerNames[4].color = Color.blue;
                        BombPlayerScores[4].color = Color.blue;
                        BombYouText.gameObject.transform.localPosition = new(BombAlignBox.transform.localPosition.x, BombPlayerNames[4].transform.localPosition.y + BombYouTextOffsetNumber, transform.localPosition.z);
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
        LootLockerSDKManager.GetScoreList(BombLeaderBoardKey, 5, 0, (System.Action<LootLockerGetScoreListResponse>)((response) =>
        {

            if (response.success)
            {
                Debug.Log("SUCCESSFULLY FETCHED LEADERBOARD INFORMATION FOR THE BOMBS GAMEMODE!");

                LootLockerLeaderboardMember[] members = response.items;




                BombNamesText.text = "Names:";
                BombScoresText.text = "Scores:";
                try
                {
                    if (members[0].player.name != string.Empty)
                    { try { BombPlayerNames[0].text = "1. " + members[0].player.name; } catch { } }
                    else { try { BombPlayerNames[0].text = "1. " + members[0].player.id; } catch { } }

                    if (members[1].player.name != string.Empty)
                    { try { BombPlayerNames[1].text = "2. " + members[1].player.name; } catch { } }
                    else { try { BombPlayerNames[1].text = "2. " + members[1].player.id; } catch { } }

                    if (members[2].player.name != string.Empty)
                    { try { BombPlayerNames[2].text = "3. " + members[2].player.name; } catch { } }
                    else { try { BombPlayerNames[2].text = "3. " + members[2].player.id; } catch { } }

                    if (members[3].player.name != string.Empty)
                    { try { BombPlayerNames[3].text = "4. " + members[3].player.name; } catch { } }
                    else { try { BombPlayerNames[3].text = "4. " + members[3].player.id; } catch { } }

                    if (members[4].player.name != string.Empty)
                    { try { BombPlayerNames[4].text = "5. " + members[4].player.name; } catch { } }
                    else { try { BombPlayerNames[4].text = "5. " + members[4].player.id; } catch { } }
                }
                catch { }

                try
                {
                    BombPlayerScores[0].text = members[0].score.ToString("N0");
                    BombPlayerScores[1].text = members[1].score.ToString("N0");
                    BombPlayerScores[2].text = members[2].score.ToString("N0");
                    BombPlayerScores[3].text = members[3].score.ToString("N0");
                    BombPlayerScores[4].text = members[4].score.ToString("N0");
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
                this.BombNamesText.text = "Failed To Load...";
                BombScoresText.text = "Try Again...";
                done = true;
            }
        }));
        yield return new WaitWhile(() => done == false);
    }

    public void RefreshLeaderboard()
    {
        StartCoroutine(FetchTopHighscoresRoutine());
    }
}
