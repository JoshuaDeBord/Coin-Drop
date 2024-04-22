using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;


public class ShopPoints : MonoBehaviour
{
    private TextMeshProUGUI TPoints;
    public ScoreCounter ScoreLink;
    public GameManager gameManager;
    public int TotalPoints = 0;
    public string TPointsText;
    public GameModesController gameModeController;


    void Start()
    {
        TPoints = GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        TPointsText = TotalPoints.ToString("N0");

        if (gameModeController.chosenGamemode == 0)
        TotalPoints = gameManager.classicSavedPoints;
        

        TPoints.text = $"Total Points: \n{TPointsText}";    
        Debug.Log("Points assigned to shop");

    }

}
