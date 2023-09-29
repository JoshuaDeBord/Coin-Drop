using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AddPoint : MonoBehaviour
{
    public int score;
    public ScoreCounter scoreCounter;
    public static bool coinEnter = true;
    public static bool coinEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (coinEntered == false)
        {
            coinEnter = true;
            coinEntered = true;
            scoreCounter.score += score;
            Thread.Sleep(400);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
