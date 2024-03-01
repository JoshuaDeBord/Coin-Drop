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
[System.Serializable]

public class GameManager : MonoBehaviour
{


    public GameObject MainMenuPanel, MainGamePanel, SettingsPanel, settingsOption1, ShopPanel, CreditsPanel,
        CreditsScreenButton, CreditsMMButton, CheatBox, SpawnedListObject, shopItem;
    public GameObject BackSettingsButton, MainMenuPlayButton, cupCover;
    public TextMeshProUGUI noSaveFileFoundText;
    public Button dropButton;
    public static PlayerInput PI;
    public Cheats cheats;

    public PlayerData playerData;
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
    public bool settingIsChanged = false;

    public Transform[] SpawnedObjects;
    public Vector3 movingLARObjectV3;

    public GameObject[] modelSelect, modelSelectedInScene;
    public Rigidbody[] rbCoin;


    public Color rainbowBallColor;
    public Light coinMainLight;

    public float rainbowColorNumber;
    public int objectsInScene, spawnedObjectsMax;

    public bool prideIsOn = false;
    public bool rapidSpawn = false;
    public bool isFloorCovered = false;

    public List<GameObject> SpawnedInObjects;

    private void Awake()
    {
        
    }

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

        objectsInScene = GameObject.FindGameObjectsWithTag("Coin Is Dropped").Length;
        


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



        if (objectsInScene > spawnedObjectsMax)
        {
            Destroy(SpawnedInObjects.First());
            SpawnedInObjects.RemoveAt(0);
            
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

        if (inMainMenuBool == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
        }
        

    }

    public IEnumerator SpinSafeArea()
    {
        while (cheats.spinScreen == true)
        {
            cheats.safeAreaObject.transform.Rotate(5 * Time.deltaTime, 5 * Time.deltaTime, 5 * Time.deltaTime);
            yield return new WaitForSeconds(0.3f);
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
            SettingsPanel.SetActive(true);
            if (PI.currentControlScheme == "Keyboard&Mouse")
            {
                EventSystem.current.SetSelectedGameObject(CheatBox);
            }
        }
        else if (inMainMenuBool == false)
        {
            MainGamePanel.SetActive(false);
            SettingsPanel.SetActive(true);

        }
    }

    public void InMainMenu(RespawnCoin respawnCoin)
    {
        
        inMainMenuBool = true;


        StartCoroutine(respawnCoin.ClearObjectsFromLists());
        
    }

    public void InMainGame()
    {
        inMainMenuBool = false;
    }

    public void InShop()
    {
        
        inShop = true;
        EventSystem.current.SetSelectedGameObject(shopItem);
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
        SettingsPanel.SetActive(true);
        MainGamePanel.SetActive(false);
        inSettings = true;
    }

    public void GoBack()
    {
        if (inSettings == true && inMainMenuBool == false)
        {
            cheats.CheatBox.text = string.Empty;
            SettingsPanel.SetActive(false);
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
        Time.timeScale = 0f;
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
        if (SpawnedInObjects.Count != 0)
        {
            SpawnedInObjects.Clear();
        }
        foreach (GameObject obj in SpawnedInObjects)
        {
            Destroy(obj);
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
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
                SpawnedInObjects.Add(SpawnedCoin);
            }
            else if (modelSelected == 2)
            {
                GameObject SpawnedSphere = Instantiate(modelSelect[1], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
                SpawnedInObjects.Add(SpawnedSphere);

            }
            yield return new WaitForSeconds(0.1f);

        }

    }


    public void DropCoin()
    {
        
        if (dropButton.interactable == true && modelSelected == 1 && dropButtonPressed == false && rapidSpawn == false)
        {
            dropButtonPressed = true;
            modelSelectedInScene[0].SetActive(false);

            GameObject SpawnedCoin = Instantiate(modelSelect[0], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
            SpawnedInObjects.Add(SpawnedCoin);

            Debug.Log("Coin Spawned");
        }
        else if (rapidSpawn == true && modelSelected == 1)
        {
            Debug.Log("Coin Spawned");
            StartCoroutine(RapidSpawn());
        }


        if (dropButton.interactable == true && modelSelected == 2 && dropButtonPressed == false && rapidSpawn == false)
        {
            dropButtonPressed = true;
            modelSelectedInScene[1].SetActive(false);

            GameObject SpawnedSphere = Instantiate(modelSelect[1], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
            SpawnedInObjects.Add(SpawnedSphere);

            Debug.Log("Sphere Spawned");
        }
        else if (rapidSpawn == true && modelSelected == 2)
        {
            Debug.Log("Coin Spawned");
            StartCoroutine(RapidSpawn());
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
        if (File.Exists(path) || settingIsChanged == true)
        {
            PlayerSettingsPreferences.SetMasterVolume(0);
            PlayerSettingsPreferences.SetSFXVolume(-30);
            PlayerSettingsPreferences.SetMusicVolume(-30);
            PlayerSettingsPreferences.updated = false;
            File.Delete(path);
           
            
            SceneManager.LoadScene("CoinDrop");


        }
        else if (!File.Exists(path) || settingIsChanged == false)
        {
            StartCoroutine(ResetButtonNoSaveFileFound());
        }
    }

    public IEnumerator ResetButtonNoSaveFileFound()
    {
        noSaveFileFoundText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noSaveFileFoundText.gameObject.SetActive(false);
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

    public IEnumerator PrideLoopColor()
    {
        while(true)
        {
            if (prideIsOn == true && modelSelected == 1)
            {
                foreach (GameObject obj in SpawnedInObjects)
                {
                    obj.GetComponent<Light>().color = Color.Lerp(coinMainLight.color, Color.HSVToRGB(rainbowColorNumber, 1f, 1f), .02f);
                }
            }
            else if (prideIsOn == false)
            {
                foreach (GameObject obj in SpawnedInObjects)
                    obj.GetComponent<Light>().color = Color.HSVToRGB(0.15f, 1f, .5f);
            }
            yield return new WaitForSeconds(0.01f);
        }
        
        
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