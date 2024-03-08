using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using LootLocker.Requests;

public class AddPoint : MonoBehaviour
{
    public int scoreToAdd;
    public ScoreCounter scoreCounter;
    public static bool coinEnter = false;
    public static bool coinEntered = false;
public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (coinEntered == false || gameManager.rapidSpawn == true)
        {
            coinEnter = true;
            coinEntered = true;
            gameManager.SpawnedInObjects.Remove(other.gameObject);
            Destroy(other.gameObject);
            
            gameManager.pointsAssign += scoreToAdd;

            Debug.Log("POINTS ARE ADDED!!");
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameManager.SavePlayer();
            Debug.Log("Saved Player");
        }
    }
}
