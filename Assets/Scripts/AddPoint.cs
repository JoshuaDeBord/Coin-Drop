using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AddPoint : MonoBehaviour
{
    public int score;
    public ScoreCounter scoreCounter;
    public static bool coinEnter = false;

    private void OnTriggerEnter(Collider other)
    {
        coinEnter = true;
        scoreCounter.score += score;
        Thread.Sleep(100);
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
