using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameModesController : MonoBehaviour
{
    [Header("Script Links")]
    public GameManager gameManager;
    private BombsGamemodeController bombsGamemodeController;
    public LoadingScreen loadingScreen;

    [Header("GameObjects")]
    public GameObject[] mainMenuButtons;
    public GameObject gamemodeChooser;
    public GameObject backButton;
    public GameObject timer;
    public GameObject startButton;
    public GameObject dropButton;
    public GameObject restartButton;
    public Button settingsButton;
    public GameObject livesLeftObj;
    public GameObject[] selectionBox;
    public GameObject loadingScreenStartButton;
    

    [Header("Buttons")]
    public Button[] gamemodeChoices;

    [Header("Text Objects")]
    public TextMeshProUGUI mainmenuTitle;

    [Header("Booleons")]
    public bool timerStarted = false;

    [Header("integers")]
    public int chosenGamemode = 0;
    
    private void Start()
    {
        bombsGamemodeController = this.GetComponent<BombsGamemodeController>();
    }
    private void Update()
    {
        if (gamemodeChooser.gameObject.activeInHierarchy == true)
        {
            mainmenuTitle.text = "Game Modes";
        }
        else mainmenuTitle.text = "Coin Dropper";

        if (chosenGamemode > 0)
        {
            dropButton.GetComponent<Button>().interactable = true;
        }

        if (timerStarted == true && chosenGamemode == 1)
        {
            settingsButton.interactable = false;
        }
        else if (chosenGamemode == 1)
        {
            settingsButton.interactable = true;
        }

        if (EventSystem.current.currentSelectedGameObject == backButton)
        {
            selectionBox[0].SetActive(false); selectionBox[1].SetActive(false); selectionBox[2].SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == gamemodeChoices[0].gameObject)
        {
            selectionBox[0].SetActive(true); selectionBox[1].SetActive(false); selectionBox[2].SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == gamemodeChoices[1].gameObject.gameObject)
        {
            selectionBox[0].SetActive(false); selectionBox[1].SetActive(true); selectionBox[2].SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == gamemodeChoices[2].gameObject)
        {
            selectionBox[0].SetActive(false); selectionBox[1].SetActive(false); selectionBox[2].SetActive(true);
        }
        
    }

    public void ToggleGameModeChooser(bool toggleGamemodeScreen)
    {
        foreach (GameObject button in mainMenuButtons)
        {
            button.SetActive(!toggleGamemodeScreen);
        }
        gamemodeChooser.SetActive(toggleGamemodeScreen);

        EventSystem.current.SetSelectedGameObject(gamemodeChoices[0].gameObject);
    }

    public void PlayGame(int gameMode)
    {
        StartCoroutine(LoadGameMode(gameMode));
        EventSystem.current.SetSelectedGameObject(loadingScreenStartButton.gameObject);
    }

    public IEnumerator LoadGameMode(int gameMode)
    {
        chosenGamemode = gameMode;

        //waits a second before executing everything past this
        yield return new WaitForSeconds(1);

        gameManager.MainGamePanel.SetActive(true);
        gameManager.InMainGame();
        
        gameManager.MainMenuPanel.SetActive(false);
        
        

        if (chosenGamemode == 0) //classic gamemode
        {
            startButton.SetActive(false);
            timer.SetActive(false);
            dropButton.SetActive(true);
            restartButton.SetActive(true);
            livesLeftObj.SetActive(false);
            bombsGamemodeController.ResetPinsColors();
        }
        else if (chosenGamemode == 1) //timed gamemode
        {
            startButton.SetActive(true);
            timer.SetActive(true);
            dropButton.SetActive(false);
            restartButton.SetActive(false);
            livesLeftObj.SetActive(false);
            bombsGamemodeController.ResetPinsColors();
            

        }
        else if (chosenGamemode == 2) //bombs gamemode
        {
            startButton.SetActive(true);
            timer.SetActive(false);
            dropButton.SetActive(false);
            restartButton.SetActive(false);
            livesLeftObj.SetActive(true);
            bombsGamemodeController.StartGamemode();
            
        }
    }
    public void StartButtonBombGamemode()
    {
        if (chosenGamemode > 0)
        {
            startButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(true);
        }
    }
}


