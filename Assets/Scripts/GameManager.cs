using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public GameObject MainMenuPanel, MainGamePanel, Settings, settingsOption1, ShopPanel, CreditsPanel, 
        CreditsScreenButton, CreditsMMButton, CheatBox, SpawnedListObject;
    public GameObject BackSettingsButton, MainMenuPlayButton, cupCover;
    public static PlayerInput PI;
    public Cheats cheats;


    public MovingLeftAndRight MovingLAR;
    public ShopPoints ShopPoints;
    public RespawnCoin RespawnCoin;
    public int pointsAssign;
    public ScoreCounter scoreCounter;
    public bool inMainMenuBool = true;

    public bool isPaused = true;
    public bool dropButtonPressed = false;
    public bool inSettings = false;
    public bool inShop = false;
    public bool inCredits = false;
    public bool dropButtonHeldDown = false;
    public bool gainedSkins = false;

    public Transform[] SpawnedObjects;
    public Vector3 movingLARObjectV3;

    public GameObject[] modelSelect, modelSelectedInScene;
    public Rigidbody[] rbCoin;


    public Color rainbowBallColor;
    public Light coinMainLight;

    public float rainbowColorNumber;
    public int coinsInSceneCount, spheresInSceneCount, spawnedObjectsMax;

    public bool prideIsOn = false;
    public bool rapidSpawn = false;
    public bool isFloorCovered = false;

    public List<GameObject> SpawnedInCoins, SpawnedInSpheres;

    void Start()
    {
        try { LoadPLayer(); }
        catch { }
        PI = gameObject.GetComponent<PlayerInput>();
        InvokeRepeating(nameof(GetRandomColor), 0f, 0.5f);
#if UNITY_WSA
        StartCoroutine(StartGameDelay());
#endif
    }

    public IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(1);
        EventSystem.current.SetSelectedGameObject(MainMenuPlayButton);
        StopCoroutine(StartGameDelay());
    }

    void Update()
    {
        movingLARObjectV3 = MovingLAR.gameObject.transform.position;

        coinsInSceneCount = GameObject.FindGameObjectsWithTag("Dropped Coin").Length;
        spheresInSceneCount = coinsInSceneCount;


        if (modelSelected == 1)
        {
            modelSelectedInScene[1].SetActive(false);
        }
        else if (modelSelected == 2)
        {
            modelSelectedInScene[0].SetActive(false);
        }

        
        
        if (inMainMenuBool == true)
        {
            BackSettingsButton.SetActive(false);
        }
        else BackSettingsButton.SetActive(true);

        if (coinsInSceneCount >= spawnedObjectsMax)
        {
            Destroy(SpawnedInCoins.First());
            SpawnedInCoins.RemoveAt(0);
            
        }
        if (spheresInSceneCount >= spawnedObjectsMax)
        {
            Destroy(SpawnedInSpheres.First());
            SpawnedInSpheres.RemoveAt(0);

        }


        scoreCounter.score = pointsAssign;


        if (gainedSkins == true)
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



            coinMainLight.color = Color.Lerp(coinMainLight.color, Color.HSVToRGB(rainbowColorNumber, 1f, 1f), .02f);

        }
        else if (prideIsOn == false)
        {
            coinMainLight.color = Color.HSVToRGB(0.15f, 1f, .5f);
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
        if (inCredits == true)
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

    public void DropButtonPressed()
    {
        dropButtonHeldDown = true;
    }
    public void DropButtonUnPressed()
    {
        dropButtonHeldDown = false;
    }
    public void DropCoinCall(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            dropButtonHeldDown = true;
            DropCoin();
        }
        else { dropButtonHeldDown = false; }

    }

    public void ClearSpawned()
    {
        if (SpawnedInCoins.Count > 0)
        {
            SpawnedInCoins.Clear();
        }
        if (SpawnedInSpheres.Count > 0)
        {
            SpawnedInSpheres.Clear();

        }
    }
    public IEnumerator RapidSpawn()
    {
        while (dropButtonHeldDown == true)
        {

            Debug.Log("Rapid Spawn Initiated");

            if (modelSelected == 1)
            {
                Debug.Log("CoinSpawned!");
                GameObject SpawnedCoin = Instantiate(modelSelect[0], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
                SpawnedInCoins.Add(SpawnedCoin);
            }
            else if (modelSelected == 2)
            {
                GameObject SpawnedSphere = Instantiate(modelSelect[1], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
                SpawnedInSpheres.Add(SpawnedSphere);

            }
            yield return new WaitForSeconds(0.1f);

        }

    }


    public void DropCoin()
    {

        if (modelSelected == 1 && dropButtonPressed == false && rapidSpawn == false)
        {


            GameObject SpawnedCoin = Instantiate(modelSelect[0], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
            SpawnedInCoins.Add(SpawnedCoin);

            Debug.Log("Coin Spawned");
        }
        else if (rapidSpawn == true && modelSelected == 1)
        {
            Debug.Log("Coin Spawned");
            StartCoroutine(RapidSpawn());
        }


        if (modelSelected == 2 && dropButtonPressed == false && rapidSpawn == false)
        {


            GameObject SpawnedSphere = Instantiate(modelSelect[1], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
            SpawnedInSpheres.Add(SpawnedSphere);

            Debug.Log("Coin Spawned");
        }
        else if (rapidSpawn == true && modelSelected == 2)
        {
            Debug.Log("Coin Spawned");
            StartCoroutine(RapidSpawn());
        }

        if (rapidSpawn == false)
        {
            dropButtonPressed = true;
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
            gainedSkins = true;

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