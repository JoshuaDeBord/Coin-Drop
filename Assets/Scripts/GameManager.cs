using System;
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



    public GameObject[] modelSelect;
    public Rigidbody[] rbCoin;


    void Start()
    {
        try { LoadPLayer(); }
        catch { }

    }

    void Update()
    {
        if (inMainMenuBool == true)
        {
            BackSettingsButton.SetActive(false);
        }
        else BackSettingsButton.SetActive(true);


        scoreCounter.score = pointsAssign;


        if (gainedSkins[0] == true)
        {

            sphereGained = true;
            SphereText.text = "TOGGLE";
            SphereText.fontSize = 82.9f;
        }

        if (modelSelected == 2)
        {
            modelSelect[1].SetActive(false);
            modelSelect[0].SetActive(true);
        }
        else if (modelSelected == 1)
        {
            modelSelect[1].SetActive(true);
            modelSelect[1].SetActive(false);
        }
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
        if (modelSelected == 1)
        {
            CoinMain.SetActive(true);
        }
        else if (modelSelected == 2)
        {
            SphereGem.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MovingLAR.moveLeft = false; MovingLAR.moveRight = false;
        Debug.Log("Game Time is Paused");
        CoinMain.SetActive(false);
        SphereGem.SetActive(false);
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
            rbCoin[1].constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rbCoin[1].useGravity = true;

        }



    }

    public bool[] gainedSkins;
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


            gainedSkins = data.gainedSkins;
        }
        catch { }
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



    public bool sphereGained = false;
    public int modelSelected = 1;
    public GameObject CoinMain, SphereGem;
    public TextMeshProUGUI SphereText;
    public GameObject checkmarkcoin, checkmarksphere, sphereequipt, coinequipt;
    public void ShopBuyItemSphere()
    {
        if (sphereGained == false && ShopPoints.TotalPoints >= 200)
        {
            pointsAssign -= 200;
            Debug.Log("Pentagon has been bought from the shop. 200 points reducted");
            sphereGained = true;
            SphereText.text = "TOGGLE";
            SphereText.fontSize = 82.9f;
            gainedSkins[0] = true;

            SavePlayer();
        }
        else if (sphereGained == true)
        {
            modelSelected = 2;
            checkmarksphere.SetActive(true);
            checkmarkcoin.SetActive(false);
            sphereequipt.SetActive(true);
            coinequipt.SetActive(false);
        }

    }


    public void CoinSelect()
    {
        modelSelected = 1;
        checkmarksphere.SetActive(false);
        checkmarkcoin.SetActive(true);
        coinequipt.SetActive(true);
        sphereequipt.SetActive(false);
        SavePlayer();
    }
}
