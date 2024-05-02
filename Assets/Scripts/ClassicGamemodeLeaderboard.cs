using LootLocker.Requests;
using System.Collections;
using TMPro;
using UnityEngine;

public class ClassicGamemodeLeaderboard : MonoBehaviour
{
    string ClassicLeaderBoardKey = "ClassicKey";
    public TextMeshProUGUI[] classicPlayerNames;
    public TextMeshProUGUI[] ClassicPlayerScores;
    public TextMeshProUGUI ClassicNamesText;
    public TextMeshProUGUI ClssicScoresText;
    public TextMeshProUGUI ClassicPersonalScore;
    public TextMeshProUGUI ClassicPersonalName;
    public TextMeshProUGUI ClassicHighScoretext;
    public TextMeshProUGUI ClassicYouText;
    public GameObject ClassicAlignBox;
    public float ClassicYouTextOffsetNumber;
    public int ClassicRankOnleaderboard;

    public PlayerManager playerManager;


    void Start()
    {

        ClassicAlignBox.SetActive(false);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, ClassicLeaderBoardKey, (response) =>
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
        LootLockerSDKManager.GetMemberRank(ClassicLeaderBoardKey, playerID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player's highscore!");
                

                if (response.rank > 5)
                {
                    ClassicHighScoretext.text = "Your High Score:";
                    ClassicPersonalName.text = $"{response.rank}. {response.player.name}";
                    ClassicPersonalScore.text = response.score.ToString("N0");


                    classicPlayerNames[0].color = Color.black;
                    ClassicPlayerScores[0].color = Color.black;

                    classicPlayerNames[1].color = Color.black;
                    ClassicPlayerScores[1].color = Color.black;

                    classicPlayerNames[2].color = Color.black;
                    ClassicPlayerScores[2].color = Color.black;

                    classicPlayerNames[3].color = Color.black;
                    ClassicPlayerScores[3].color = Color.black;

                    classicPlayerNames[4].color = Color.black;
                    ClassicPlayerScores[4].color = Color.black;

                    ClassicYouText.gameObject.SetActive(false);
                }
                else
                {
                    if (response.rank > 1)
                        ClassicYouText.gameObject.SetActive(true);
                    ClassicRankOnleaderboard = response.rank;
                    ClassicHighScoretext.text = string.Empty;
                    ClassicPersonalScore.text = string.Empty;
                    ClassicPersonalName.text = string.Empty;

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
        LootLockerSDKManager.GetScoreList(ClassicLeaderBoardKey, 5, 0, (response) =>
        {

            if (response.success)
            {
                done = true;
                Debug.Log("Successfully retrieved the leaderboard ranks");

                LootLockerLeaderboardMember[] members = response.items;

                if (ClassicRankOnleaderboard == members[0].rank)
                {
                    foreach (TextMeshProUGUI tmp in classicPlayerNames) { tmp.color = Color.black; }
                    foreach (TextMeshProUGUI tmp in ClassicPlayerScores) { tmp.color = Color.black; };
                    classicPlayerNames[0].color = Color.blue;
                    ClassicPlayerScores[0].color = Color.blue;
                    ClassicYouText.gameObject.transform.localPosition = new(ClassicAlignBox.transform.localPosition.x, classicPlayerNames[0].transform.localPosition.y + ClassicYouTextOffsetNumber, transform.localPosition.z);
                }
                if (ClassicRankOnleaderboard == members[1].rank)
                {
                    foreach (TextMeshProUGUI tmp in classicPlayerNames) { tmp.color = Color.black; }
                    foreach (TextMeshProUGUI tmp in ClassicPlayerScores) { tmp.color = Color.black; };
                    classicPlayerNames[1].color = Color.blue;
                    ClassicPlayerScores[1].color = Color.blue;
                    ClassicYouText.gameObject.transform.localPosition = new(ClassicAlignBox.transform.localPosition.x, classicPlayerNames[1].transform.localPosition.y + ClassicYouTextOffsetNumber, transform.localPosition.z);
                }
                if (ClassicRankOnleaderboard == members[2].rank)
                {
                    foreach (TextMeshProUGUI tmp in classicPlayerNames) { tmp.color = Color.black; }
                    foreach (TextMeshProUGUI tmp in ClassicPlayerScores) { tmp.color = Color.black; };
                    classicPlayerNames[2].color = Color.blue;
                    ClassicPlayerScores[2].color = Color.blue;
                    ClassicYouText.gameObject.transform.localPosition = new(ClassicAlignBox.transform.localPosition.x, classicPlayerNames[2].transform.localPosition.y + ClassicYouTextOffsetNumber, transform.localPosition.z);
                }
                if (ClassicRankOnleaderboard == members[3].rank)
                {
                    foreach (TextMeshProUGUI tmp in classicPlayerNames) { tmp.color = Color.black; }
                    foreach (TextMeshProUGUI tmp in ClassicPlayerScores) { tmp.color = Color.black; };
                    classicPlayerNames[3].color = Color.blue;
                    ClassicPlayerScores[3].color = Color.blue;
                    ClassicYouText.gameObject.transform.localPosition = new(ClassicAlignBox.transform.localPosition.x, classicPlayerNames[3].transform.localPosition.y + ClassicYouTextOffsetNumber, transform.localPosition.z);
                }
                if (ClassicRankOnleaderboard == members[4].rank)
                {
                    foreach (TextMeshProUGUI tmp in classicPlayerNames) { tmp.color = Color.black; }
                    foreach (TextMeshProUGUI tmp in ClassicPlayerScores) { tmp.color = Color.black; };
                    classicPlayerNames[4].color = Color.blue;
                    ClassicPlayerScores[4].color = Color.blue;
                    ClassicYouText.gameObject.transform.localPosition = new(ClassicAlignBox.transform.localPosition.x, classicPlayerNames[4].transform.localPosition.y + ClassicYouTextOffsetNumber, transform.localPosition.z);
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
        LootLockerSDKManager.GetScoreList(ClassicLeaderBoardKey, 5, 0, (response) =>
        {

            if (response.success)
            {
                Debug.Log("SUCCESSFULLY FETCHED LEADERBOARD INFORMATION FOR THE CLASSIC GAMEMODE!");
                playerManager.signInButton.SetActive(false);

                LootLockerLeaderboardMember[] members = response.items;




                ClassicNamesText.text = "Names:";
                ClssicScoresText.text = "Scores:";
                try
                {
                    if (members[0].player.name != string.Empty)
                    { try { classicPlayerNames[0].text = "1. " + members[0].player.name; } catch { } }
                    else { try { classicPlayerNames[0].text = "1. " + members[0].player.id; } catch { } }

                    if (members[1].player.name != string.Empty)
                    { try { classicPlayerNames[1].text = "2. " + members[1].player.name; } catch { } }
                    else { try { classicPlayerNames[1].text = "2. " + members[1].player.id; } catch { } }

                    if (members[2].player.name != string.Empty)
                    { try { classicPlayerNames[2].text = "3. " + members[2].player.name; } catch { } }
                    else { try { classicPlayerNames[2].text = "3. " + members[2].player.id; } catch { } }

                    if (members[3].player.name != string.Empty)
                    { try { classicPlayerNames[3].text = "4. " + members[3].player.name; } catch { } }
                    else { try { classicPlayerNames[3].text = "4. " + members[3].player.id; } catch { } }

                    if (members[4].player.name != string.Empty)
                    { try { classicPlayerNames[4].text = "5. " + members[4].player.name; } catch { } }
                    else { try { classicPlayerNames[4].text = "5. " + members[4].player.id; } catch { } }
                }
                catch { }

                try
                {
                    ClassicPlayerScores[0].text = members[0].score.ToString("N0");
                    ClassicPlayerScores[1].text = members[1].score.ToString("N0");
                    ClassicPlayerScores[2].text = members[2].score.ToString("N0");
                    ClassicPlayerScores[3].text = members[3].score.ToString("N0");
                    ClassicPlayerScores[4].text = members[4].score.ToString("N0");
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
                ClassicNamesText.text = "Failed To Load...";
                ClssicScoresText.text = "Try Again...";
                foreach (TextMeshProUGUI tmp in classicPlayerNames)
                {
                    tmp.text = string.Empty;
                }
                foreach (TextMeshProUGUI tmp in ClassicPlayerScores)
                {
                    tmp.text = string.Empty;
                }
                ClassicYouText.gameObject.SetActive(false);
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
