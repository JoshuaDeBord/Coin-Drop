using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class PlayerData
{
    
    public int pointsAssign; // Cuurent Score
    public bool gainedSkins;
    public bool settingsChanged = false;



    public PlayerData(ScoreCounter scoreCounter, GameManager gameManager)
    {
        settingsChanged = gameManager.settingIsChanged;
        pointsAssign = gameManager.pointsAssign;
        gainedSkins = gameManager.gainedSkins;

        
    }

}
