using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public GameObject MainMenuPanel, MainGamePanel, Settings, settingsOption1, ShopPanel, CreditsPanel, CreditsScreenButton, CreditsMMButton, CheatBox, CoinObject;
    public GameObject BackSettingsButton, MainMenuPlayButton;
    public static PlayerInput PI;
    public Cheats cheats;


    public MovingLeftAndRight MovingLAR;
    public ShopPoints ShopPoints;
    public int pointsAssign;
    public ScoreCounter scoreCounter;
    public bool inMainMenuBool = true;

    public bool isPaused = true;
    public bool dropButtonPressed = false;
    public bool inSettings = false;
    public bool inShop = false;
    public bool inCredits = false;


    public GameObject[] modelSelect;
    public Rigidbody[] rbCoin;


    public Color rainbowBallColor;
    public Light coinMainLight;

    public float rainbowColorNumber;

    public bool prideIsOn = false;

    void Start()
    {
        try { LoadPLayer(); }
        catch { }
        PI = gameObject.GetComponent<PlayerInput>();
        InvokeRepeating(nameof(GetRandomColor), 0f, 0.5f);
        StartCoroutine(StartGameDelay());
    }

    public IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(1);
        EventSystem.current.SetSelectedGameObject(MainMenuPlayButton);
        StopCoroutine(StartGameDelay());
    }

    void Update()
    {

        if (modelSelected == 1)
        {
            modelSelect[1].SetActive(false);
        }
        else modelSelect[0].SetActive(false);


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

        if (PI.currentActionMap.name == "UI")
        {
            Debug.Log("Action map is on UI");
        }
        else if (PI.currentActionMap.name == "Player")
        {
            Debug.Log("Action map is on Player");
        }


        if (prideIsOn == true)
        {
            
            

            coinMainLight.color = Color.Lerp(coinMainLight.color, Color.HSVToRGB(rainbowColorNumber, 1f, 1f),.02f);

        }
        else if (prideIsOn == false)
        {
           coinMainLight.color = Color.HSVToRGB(0.15f, 1f,.5f);
        }
        
    }

    public void GetRandomColor()
    {
        rainbowColorNumber = UnityEngine.Random.Range(0f, 1f);
        
    }

    public void GoToSettings()
    {
        
        if (inMainMenuBool == true)
        {
            MainMenuPanel.SetActive(false);
            Settings.SetActive(true);
        if (PI.currentControlScheme == "Keyboard&Mouse")
            {
                EventSystem.current.SetSelectedGameObject(CheatBox);
            }
        }
        else if (inMainMenuBool == false)
        {
            MainGamePanel.SetActive(false);
            Settings.SetActive(true);

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

    public void InShop()
    {
        inShop = true;
    }

    public void InCredits()
    {
        inCredits = true;
    }

    public void OpenSettingsConsole(InputAction.CallbackContext context)
    {

        OpenSettings();
        EventSystem.current.SetSelectedGameObject(settingsOption1);
    }
    public void OpenSettings()
    {
        Settings.SetActive(true);
        MainGamePanel.SetActive(false);
        inSettings = true;
    }

    public void GoBack()
    {
        if (inSettings == true && inMainMenuBool == false)
        {
            cheats.CheatBox.text = string.Empty;
            Settings.SetActive(false);
            MainGamePanel.SetActive(true);
            inSettings = false;
            PI.SwitchCurrentActionMap("Player");
            UnPauseGame();
            InMainGame();
            

        }
        if (inShop == true)
        {
            cheats.CheatBox.text = string.Empty;
            inShop = false;
            ShopPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(MainMenuPlayButton);
        }
        if  (inCredits == true)
        {
            inCredits = false;
            MainMenuPanel.SetActive(true);
            CreditsPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(CreditsMMButton);
        }
    }

    public void UnPauseGame()
    {
        MovingLAR = GameObject.Find("MovingLeftAndRightAndRespawnPoint").GetComponent<MovingLeftAndRight>();
        Time.timeScale = 1f;
        Debug.Log("Game Time is UnPaused");
        isPaused = false;
        inSettings = false;
        PI.SwitchCurrentActionMap("Player");
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
        //Time.timeScale = 0f;
        MovingLAR.moveLeft = false; MovingLAR.moveRight = false;
        Debug.Log("Game Time is Paused");
        CoinMain.SetActive(false);
        SphereGem.SetActive(false);
        PI.SwitchCurrentActionMap("UI");
    }

    public void DropCoinCall(InputAction.CallbackContext context)
    {
        if (MovingLAR.rightLeftPressed == true)
        {
            DropCoin();
        }

    }

    public void DropCoin()
    {

        if (modelSelected == 1 && modelSelect[0].gameObject.activeInHierarchy == false && dropButtonPressed == false)
        {

            
            Instantiate(modelSelect[0], CoinObject.gameObject.transform);

            Debug.Log("Coin Spawned");
        }
        if (modelSelected == 2 && modelSelect[1].gameObject.activeInHierarchy == false && dropButtonPressed == false)
        {
            

            Instantiate(modelSelect[1], CoinObject.gameObject.transform);
            Debug.Log("Coin Spawned");
        }
        dropButtonPressed = true;


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
            MainGamePanel.SetActive(false);
            Settings.SetActive(false);
            MainMenuPanel.SetActive(true);
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
        else
        {
            StartCoroutine(InsufficientFunds());
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

    public IEnumerator InsufficientFunds()
    {
        SphereText.SetText("Insufficient\nBalance");
        yield return new WaitForSeconds(2);
        if (sphereGained == true)
        {
            SphereText.SetText("TOGGLE");
            
        }
        else SphereText.SetText("200");
        StopCoroutine(InsufficientFunds());
    }

    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);

        
    }

    public void OpenMainMenu()
    {
        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}