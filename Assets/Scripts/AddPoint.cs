using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

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
        if (coinEntered == false)
        {
            coinEnter = true;
            coinEntered = true;
            
            gameManager.pointsAssign += scoreToAdd;
            Debug.Log("POINTS ARE ADDED!!");
            Thread.Sleep(400);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameManager.SavePlayer();
            Debug.Log("Saved Player");
        }
    }
}
