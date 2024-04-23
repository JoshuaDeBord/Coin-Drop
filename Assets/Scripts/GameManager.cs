using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]

public class GameManager : MonoBehaviour
{


    public GameObject MainMenuPanel, MainGamePanel, SettingsPanel, LeaderboardPanel, settingsOption1, ShopPanel, CreditsPanel,
        CreditsScreenButton, CreditsMMButton, LeaderboardMMButton, CheatBox, SpawnedListObject, shopItem;
    public GameObject BackSettingsButton, MainMenuPlayButton, cupCover;
    public TextMeshProUGUI noSaveFileFoundText;
    public TMP_InputField nameInputField;
    public Button dropButton;
    public PlayerInput PI;
    public Cheats cheats;
    public TextMeshProUGUI cooldownNumber;
    public GameObject refreshButtonObject;

    public PlayerData playerData;
    public MovingLeftAndRight MovingLAR;
    public ShopPoints ShopPoints;
    public RespawnCoin RespawnCoin;
    public ClassicGamemodeLeaderboard Classicleaderboard;
    public TimedGamemodeLeaderboard TimedLeaderboard;
    public BombsGamemodeLeaderboard BombsLeaderboard;
    public GameModesController gameModesController;
    public Timer timedTimer;
    public GameObject TimedTimerSettingsPopup;
    public Timer timer;

    public CheatBoxEnable CheatBoxEnable;

    public int classicSavedPoints; //these points are saved
    public int timedSavedPoints; //this is not saved
    public int bombsSavedPoints; //this is not saved

    public int totalHighScore;
    public int Refreshcooldown = 5;

    public ScoreCounter scoreCounter;

    [Header("Bools")]
    public bool inMainMenuBool = true;
    public bool isRefreshCooldownActive = false;
    public bool isPaused = true;
    public bool dropButtonPressed = false;
    public bool inSettings = false;
    public bool inShop = false;
    public bool inCredits = false;
    public bool inLeaderboard = false;
    public bool dropButtonHeldDown = false;
    public bool gainedSkins = false;
    public bool settingIsChanged = false;
    public bool isCheatsUsed = false;
    public bool prideIsOn = false;
    public bool rapidSpawn = false;
    public bool isFloorCovered = false;

    public Transform[] SpawnedObjects;
    public Vector3 movingLARObjectV3;

    public GameObject[] modelSelect, modelSelectedInScene;
    public Rigidbody[] rbCoin;


    public Color rainbowBallColor;
    public Light coinMainLight;

    public float rainbowColorNumber;
    public int objectsInScene, spawnedObjectsMax;


    public Button enableCheats;
    public Image cheatsUnabledTextInLeaderboard;
    public TMP_InputField CheatCodeBox;
    public Animator refreshButtonAnimatior;

    public List<GameObject> SpawnedInObjects;

    private void Awake()
    {

    }

    private void Start()
    {
        string path = Application.persistentDataPath + "/player.save";
        Debug.Log(path);
        string playerID = PlayerPrefs.GetString("PlayerID");
        Debug.Log(playerID);
        try { LoadPLayer(); }
        catch { }
        PI = gameObject.GetComponent<PlayerInput>();
        InvokeRepeating(nameof(GetRandomColor), 0f, 0.5f);
#if UNITY_WSA
        StartCoroutine(StartGameDelay());
#endif

        if (isCheatsUsed == true)
        {
            CheatCodeBox.gameObject.SetActive(true);
            enableCheats.gameObject.SetActive(false);
            cheatsUnabledTextInLeaderboard.gameObject.SetActive(true);
        }
    }

    public IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(1);
        EventSystem.current.SetSelectedGameObject(MainMenuPlayButton);
        StopCoroutine(StartGameDelay());
    }


    private void Update()
    {
        movingLARObjectV3 = MovingLAR.gameObject.transform.position;

        objectsInScene = GameObject.FindGameObjectsWithTag("Coin Is Dropped").Length;

        if (GameObject.Find("CoinMain(Clone)") == true && rapidSpawn == false)
        {
            DropButtonPressed();
        }

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
            Time.timeScale = 1;
        }
        else
        {
            BackSettingsButton.SetActive(true);
        }

        if (objectsInScene > spawnedObjectsMax)
        {
            Destroy(SpawnedInObjects.First());
            SpawnedInObjects.RemoveAt(0);

        }

        if (gameModesController.chosenGamemode == 0)
            scoreCounter.score = classicSavedPoints;
        if (gameModesController.chosenGamemode == 1)
            scoreCounter.score = timedSavedPoints;
        if (gameModesController.chosenGamemode == 2)
            scoreCounter.score = bombsSavedPoints;





        if (gainedSkins == true)
        {

            sphereGained = true;
            SphereText.text = "TOGGLE";
            SphereText.fontSize = 82.9f;
        }

        /*if (PI.currentActionMap.name == "UI")
        {
            Debug.Log("Action map is on UI");
        }
        else if (PI.currentActionMap.name == "Player")
        {
            Debug.Log("Action map is on Player");
        }*/

        if (inMainMenuBool == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
        }

#if UNITY_ANDROID || UNITY_IOS
        if (LeaderboardPanel.activeInHierarchy == false && SettingsPanel.activeInHierarchy == false)
        {
            EventSystem.current.SetSelectedGameObject(null);

        }
#endif
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


        _ = StartCoroutine(respawnCoin.ClearObjectsFromLists());

    }

    public void InMainGame()
    {
        inMainMenuBool = false;
    }

    public void GoToLeaderboard()
    {
        Time.timeScale = 1f;
        inLeaderboard = true;
        LeaderboardPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
#if UNITY_WSA
        StartCoroutine(FlashInputFieldXSquare());
#endif
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

    public void UIRefreshLeaderboard()
    {
        if (LeaderboardPanel.activeInHierarchy == true)
        {
            RefreshLeaderBoard();
        }
    }

    public void RefreshLeaderBoard()
    {
        if (isRefreshCooldownActive == false)
        {
            foreach (TextMeshProUGUI tmp in Classicleaderboard.classicPlayerNames)
            {
                tmp.text = string.Empty;
            }
            foreach (TextMeshProUGUI tmp in Classicleaderboard.ClassicPlayerScores)
            {
                tmp.text = string.Empty;
            }
            Classicleaderboard.ClassicNamesText.text = "Loading...";
            Classicleaderboard.ClssicScoresText.text = "Loading...";
            Classicleaderboard.ClassicHighScoretext.text = string.Empty;
            Classicleaderboard.ClassicPersonalScore.text = string.Empty;
            Classicleaderboard.ClassicPersonalName.text = string.Empty;

            cooldownNumber.gameObject.SetActive(true);
            isRefreshCooldownActive = true;
            refreshButtonAnimatior.SetTrigger("refreshspin");
            StartCoroutine(RefreshCoolDown());

            StartCoroutine(Classicleaderboard.FetchTopHighscoresRoutine());
            StartCoroutine(TimedLeaderboard.FetchTopHighscoresRoutine());
            StartCoroutine(BombsLeaderboard.FetchTopHighscoresRoutine());
        }
    }
    IEnumerator RefreshCoolDown()
    {
        while (isRefreshCooldownActive == true)
        {
            yield return new WaitForSeconds(1);

            Refreshcooldown--;
            cooldownNumber.text = Refreshcooldown.ToString();

            if (Refreshcooldown == 0)
            {

                Refreshcooldown = 5;
                cooldownNumber.text = "5";
                isRefreshCooldownActive = false;
                cooldownNumber.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void OpenSettingsConsole(InputAction.CallbackContext context)
    {
        if (gameModesController.timerStarted == false)
        {
            OpenSettings();
            EventSystem.current.SetSelectedGameObject(settingsOption1);
            PauseGame();
            EventSystem.current.SetSelectedGameObject(GameObject.Find("HIGH GRAVITY BUTTON"));
        }
    }
    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
        MainGamePanel.SetActive(false);
        inSettings = true;
    }

    public IEnumerator OpenSettingsPopUp()
    {
        TimedTimerSettingsPopup.SetActive(true);
        yield return new WaitForSeconds(2);
        TimedTimerSettingsPopup.SetActive(false);
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
            TimedTimerSettingsPopup.SetActive(false);
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
        if (inLeaderboard == true)
        {
#if UNITY_WSA || UNITY_PS4
            StopCoroutine(FlashInputFieldXSquare());
#endif
            refreshButtonAnimatior.playbackTime = 0;
            refreshButtonAnimatior.StopPlayback();
            refreshButtonObject.transform.rotation = Quaternion.identity;

            inLeaderboard = false;
            LeaderboardPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(LeaderboardMMButton);

        }
    }

    public void OpenNameInputField()
    {
        if (LeaderboardPanel.activeInHierarchy == true)
        {
            nameInputField.ActivateInputField();
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
        if (gameModesController.chosenGamemode > 0 && gameModesController.timerStarted == false)
        {
            timer.StartTimer(60);
        }
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

            modelSelectedInScene[0].SetActive(false);

            GameObject SpawnedCoin = Instantiate(modelSelect[0], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
            SpawnedInObjects.Add(SpawnedCoin);

            Debug.Log("Coin Spawned");
            dropButtonPressed = true;
        }
        else if (rapidSpawn == true && modelSelected == 1)
        {
            Debug.Log("Coin Spawned");
            _ = StartCoroutine(RapidSpawn());
        }


        if (dropButton.interactable == true && modelSelected == 2 && dropButtonPressed == false && rapidSpawn == false)
        {

            modelSelectedInScene[1].SetActive(false);

            GameObject SpawnedSphere = Instantiate(modelSelect[1], MovingLAR.transform.position, Quaternion.Euler(90, 0, 0), SpawnedListObject.transform);
            SpawnedInObjects.Add(SpawnedSphere);

            Debug.Log("Sphere Spawned");
            dropButtonPressed = true;
        }
        else if (rapidSpawn == true && modelSelected == 2)
        {
            Debug.Log("Coin Spawned");
            _ = StartCoroutine(RapidSpawn());
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

            classicSavedPoints = data.classicSavedPoints;


            totalHighScore = data.highScore;

            isCheatsUsed = data.isCheatsEnabled;
            Debug.Log($"loaded save Points for Classic gamemode: {classicSavedPoints}");


            gainedSkins = data.gainedSkins;
        }
        catch { }
    }

    public void Reset()
    {
        string path = Application.persistentDataPath + "/player.save";

        if (File.Exists(path) || settingIsChanged == true || isCheatsUsed == true)
        {
            PlayerSettingsPreferences.SetMasterVolume(0);
            PlayerSettingsPreferences.SetSFXVolume(-40);
            PlayerSettingsPreferences.SetMusicVolume(-40);
            PlayerSettingsPreferences.updated = false;
            settingIsChanged = false;
            File.Delete(path);

            SceneManager.LoadScene(0);

        }
        else if (!File.Exists(path) || settingIsChanged == false)
        {
            _ = StartCoroutine(ResetButtonNoSaveFileFound());
        }
    }
    public IEnumerator DeletePlayerRoutine()
    {
        bool done = false;
        LootLockerSDKManager.DeletePlayer((response) =>
        {

            if (response.success)
            {
                done = true;
                Debug.Log("Successfully Deleted The Player");
            }
            else
            {
                done = true;
                Debug.LogWarning("Failed To Delete Player" + response.errorData);
            }

        });
        yield return new WaitWhile(() => done == false);

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
            if (gameModesController.chosenGamemode == 0)
                classicSavedPoints -= 200;


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
            _ = StartCoroutine(InsufficientFunds());
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
        else
        {
            SphereText.SetText("200");
        }

        StopCoroutine(InsufficientFunds());
    }

    public IEnumerator PrideLoopColor()
    {
        while (true)
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
                {
                    obj.GetComponent<Light>().color = Color.HSVToRGB(0.15f, 1f, .5f);
                }
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

    public IEnumerator FlashInputFieldXSquare()
    {
        while (LeaderboardPanel.activeInHierarchy == true)
        {

            GetComponent<EventSystemHelper>().nameInputFieldOpenIcon.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GetComponent<EventSystemHelper>().nameInputFieldOpenIcon.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}