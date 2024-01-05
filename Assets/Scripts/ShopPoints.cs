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


    void Start()
    {
        TPoints = GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {


        TotalPoints = gameManager.pointsAssign;
        TPoints.text = TPointsText;

        TPointsText = $"Total Points:  {TotalPoints}";
        Debug.Log("Points assigned to shop");

    }

}
