using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class PlayerData
{

    public int classicSavedPoints;
    public int timedSavedPoints;
    public int bombsSavedPoints;
    public int highScore;
    public bool gainedSkins;
    public bool settingsChanged = false;
    public bool isCheatsEnabled = false;



    public PlayerData(ScoreCounter scoreCounter, GameManager gameManager)
    {
        settingsChanged = gameManager.settingIsChanged;

        classicSavedPoints = gameManager.classicSavedPoints;
        

        highScore = gameManager.totalHighScore;

        gainedSkins = gameManager.gainedSkins;

        isCheatsEnabled = gameManager.isCheatsUsed;
        
    }

}
