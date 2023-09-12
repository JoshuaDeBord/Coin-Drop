using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoint : MonoBehaviour
{
    public int score;
    public ScoreCounter scoreCounter;
   

    private void OnTriggerEnter(Collider other)
    {
        scoreCounter.score += score;
    }
}
