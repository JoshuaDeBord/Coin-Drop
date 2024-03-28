using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class PlayerData
{
    
    public int pointsAssign; // Cuurent Score
    public int highScore;
    public bool gainedSkins;
    public bool settingsChanged = false;
    public bool isCheatsEnabled = false;



    public PlayerData(ScoreCounter scoreCounter, GameManager gameManager)
    {
        settingsChanged = gameManager.settingIsChanged;
        pointsAssign = gameManager.pointsAssign;
        highScore = gameManager.totalHighScore;
        gainedSkins = gameManager.gainedSkins;
        isCheatsEnabled = gameManager.isCheatsUsed;
        
    }

}
