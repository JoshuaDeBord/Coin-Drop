using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using LootLocker.Requests;
using UnityEngine.SocialPlatforms.Impl;

public class AddPoint : MonoBehaviour
{
    public int scoreToAdd;
    public ScoreCounter scoreCounter;
    public static bool coinEnter = false;
    public static bool coinEntered = false;
    public GameManager gameManager;
    public LeaderBoard leaderBoard;
    public Cheats cheats;
    public Collider colObj;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        leaderBoard = GameObject.Find("LeaderBoard").GetComponent<LeaderBoard>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (coinEntered == false || gameManager.rapidSpawn == true)
        {

            coinEnter = true;
            coinEntered = true;


            Destroy(other.gameObject);
            StartCoroutine(WaitClearList(other.gameObject, 0.25f));

            gameManager.pointsAssign += scoreToAdd;

            if (gameManager.totalHighScore < gameManager.pointsAssign)
            {

                gameManager.totalHighScore = gameManager.pointsAssign;

            }
            if (gameManager.isCheatsUsed == false)
            {
                StartCoroutine(leaderBoard.SubmitScoreRoutine(gameManager.totalHighScore));
            }
            Debug.Log("POINTS ARE ADDED!!");
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameManager.SavePlayer();
            Debug.Log("Saved Player");
        }
    }

    public IEnumerator WaitClearList(GameObject obj, float waitTime)
    {
        yield return new WaitForSeconds(0.25f);
        gameManager.SpawnedInObjects.Remove(obj.gameObject);
    }
}
