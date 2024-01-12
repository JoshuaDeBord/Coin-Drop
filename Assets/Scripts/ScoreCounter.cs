using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    public int pointsAssign = 0;

    public TextMeshProUGUI scoreLabel;
    public GameManager gameManager;



    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }


    void Update()
    {
        if (score >= 2147483647)
        {
            scoreLabel.text = "YOU WIN!!!!";
        }
        else
        {
            scoreLabel.text = score.ToString("N0");
        }

    }



}

