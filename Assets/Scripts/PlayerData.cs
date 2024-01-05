using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class PlayerData
{
    
    public int pointsAssign; // Cuurent Score
    public int[] gainedSkins;



    public PlayerData(ScoreCounter scoreCounter, GameManager gameManager)
    {
        
        pointsAssign = gameManager.pointsAssign;
        gainedSkins = gameManager.gainedSkins;

        
    }

}
