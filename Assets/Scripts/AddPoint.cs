using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoint : MonoBehaviour
{
    public int score;
    public ScoreCounter scoreCounter;
   

    private void OnTriggerExit(Collider other)
    {
        scoreCounter.score += score;
    }
}
