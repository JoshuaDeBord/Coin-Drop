using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoint : MonoBehaviour
{
    public int score;
    public ScoreCounter scoreCounter;
    public bool coinEnter = false;

    private void OnTriggerEnter(Collider other)
    {
        coinEnter = true;
        scoreCounter.score += score;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
