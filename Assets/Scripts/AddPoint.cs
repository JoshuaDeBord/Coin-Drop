using System.Collections;
using UnityEngine;

public class AddPoint : MonoBehaviour
{
    public int scoreToAdd;
    public ScoreCounter scoreCounter;
    public static bool coinEnter = false;
    public static bool coinEntered = false;
    private GameManager gameManager;
    private ClassicGamemodeLeaderboard ClassicLeaderboard;
    private TimedGamemodeLeaderboard TimedLeaderboard;
    private BombsGamemodeLeaderboard BombLeaderboard;
    private GameModesController GameModeController;
    private GamemodeFinishController gmFController;
    private BombsGamemodeController bombController;
    private RespawnCoin respawnCoin;
    public Cheats cheats;
    public Collider colObj;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ClassicLeaderboard = GameObject.Find("LeaderBoard").GetComponent<ClassicGamemodeLeaderboard>();
        TimedLeaderboard = GameObject.Find("LeaderBoard").GetComponent<TimedGamemodeLeaderboard>();
        BombLeaderboard = GameObject.Find("LeaderBoard").GetComponent<BombsGamemodeLeaderboard>();
        GameModeController = GameObject.Find("GameModesController").GetComponent<GameModesController>();
        respawnCoin = GameObject.Find("MovingLeftAndRightAndRespawnPoint").GetComponent<RespawnCoin>();
        gmFController = ClassicLeaderboard.gameObject.gameObject.GetComponent<GamemodeFinishController>();
        bombController = GameModeController.gameObject.GetComponent<BombsGamemodeController>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin Is Dropped")
        {


            if (coinEntered == false || gameManager.rapidSpawn == true)
            {

                coinEnter = true;
                coinEntered = true;


                Destroy(other.gameObject);
                StartCoroutine(WaitClearList(other.gameObject, 0.25f));

                if (GameModeController.chosenGamemode == 0)
                    gameManager.classicSavedPoints += scoreToAdd;

                if (GameModeController.chosenGamemode > 0)
                {
                    Debug.Log("YESSSSSS");
                    gameManager.timedSavedPoints += scoreToAdd;

                    respawnCoin.RestartGame();
                    if (scoreToAdd == 5)
                    {
                        gmFController.Add5();
                        Debug.Log("Added 5 points");
                    }
                    else if (scoreToAdd == 10)
                    {
                        gmFController.Add10();
                        Debug.Log("Added 10 points");
                    }
                    else if (scoreToAdd == 15)
                    {
                        gmFController.Add15();
                        Debug.Log("Added 15 points");
                    }
                    else if (scoreToAdd == 20)
                    {
                        gmFController.Add20();
                        Debug.Log("Added 20 points");
                    }
                }

                if (GameModeController.chosenGamemode == 2)
                {
                    gameManager.bombsSavedPoints += scoreToAdd;
                    bombController.StartGamemode();
                }



                if (gameManager.isCheatsUsed == false)
                {
                    if (GameModeController.chosenGamemode == 0)
                        StartCoroutine(ClassicLeaderboard.SubmitScoreRoutine(gameManager.classicSavedPoints));
                    else if (GameModeController.chosenGamemode == 1)
                    {
                        StartCoroutine(TimedLeaderboard.SubmitScoreRoutine(gameManager.timedSavedPoints));
                    }
                    else if (GameModeController.chosenGamemode == 2)
                    {
                        StartCoroutine(BombLeaderboard.SubmitScoreRoutine(gameManager.bombsSavedPoints));
                    }
                }
                Debug.Log("POINTS ARE ADDED!!");
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameManager.SavePlayer();
                Debug.Log("Saved Player");
            }
        }
    }

    public IEnumerator WaitClearList(GameObject obj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameManager.SpawnedInObjects.Remove(obj.gameObject);
    }
}
