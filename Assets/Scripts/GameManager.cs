using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public GameObject MainMenuPanel, MainGamePanel, Settings;
    public GameObject BackSettingsButton;

    public MovingLeftAndRight MovingLAR;
    public ShopPoints ShopPoints;
    public int pointsAssign;
    public ScoreCounter scoreCounter;
    public bool inMainMenuBool = true;

    public bool isPaused = true;
    public bool dropButtonPressed = false;


    public int[] gainedSkins;
    public GameObject[] modelSelect;
    public Rigidbody[] rbCoin;


    void Start()
    {
        try { LoadPLayer(); }
        catch {}
        
    }

    void Update()
    {
        if (inMainMenuBool == true)
        {
            BackSettingsButton.SetActive(false);
        }
        else BackSettingsButton.SetActive(true);

        
        scoreCounter.score = pointsAssign;

    }


    public void GoToSettings()
    {
        if (inMainMenuBool == true)
        {
            MainMenuPanel.SetActive(true);
            Settings.SetActive(false);

        }
        else if (inMainMenuBool == false)
        {
            MainGamePanel.SetActive(true);
            Settings.SetActive(false);

        }
    }

    public void InMainMenu()
    {
        inMainMenuBool = true;


    }

    public void InMainGame()
    {
        inMainMenuBool = false;
    }






    public void UnPauseGame()
    {
        MovingLAR = GameObject.Find("MovingLeftAndRightAndRespawnPoint").GetComponent<MovingLeftAndRight>();
        Time.timeScale = 1f;
        Debug.Log("Game Time is UnPaused");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MovingLAR.moveLeft = false; MovingLAR.moveRight = false;
        Debug.Log("Game Time is Paused");
    }

    public void DropCoin()
    {

        dropButtonPressed = true;
        rbCoin[0].useGravity = true;

        if (modelSelect[0].gameObject.activeInHierarchy == true)
        {

            rbCoin[0].constraints = RigidbodyConstraints.None;
            rbCoin[0].constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
            rbCoin[0].useGravity = true;
            Debug.Log("yes");
        }
        if (modelSelect[1].gameObject.activeInHierarchy == true)
        {
            rbCoin[1].constraints = RigidbodyConstraints.None;
            rbCoin[1].constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            rbCoin[1].useGravity = true;

        }



    }
    
    public void ShopBuyItemPentegon()
    {
        if (ShopPoints.TotalPoints >= 200)
        {
            pointsAssign -= 200;
            Debug.Log("Pentagon has been bought from the shop. 200 points reducted");
        }

    } 
    public void SavePlayer()
    {
        SaveSystem.SaveData(scoreCounter, this);
        
    }

    public void LoadPLayer()
    {
        try
        {
            PlayerData data = SaveSystem.LoadPlayer();

            
            gainedSkins = data.gainedSkins;

            pointsAssign = data.pointsAssign;
            Debug.Log($"loaded save Points: {pointsAssign}");
        }
        catch {}
    }

    public void Reset()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            File.Delete(path);
            SceneManager.LoadScene("CoinDrop");
        }
    }
}
