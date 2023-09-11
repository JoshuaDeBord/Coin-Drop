using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreLabel;

    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        scoreLabel.text = score.ToString();
    }
    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;
    }
}

