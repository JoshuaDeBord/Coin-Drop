using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameModesController : MonoBehaviour
{
    [Header("Script Links")]
    public GameManager gameManager;
    private BombsGamemodeController bombsGamemodeController;
    

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
    }

    public void ToggleGameModeChooser(bool toggleGamemodeScreen)
    {
        foreach (GameObject button in mainMenuButtons)
        {
            button.SetActive(!toggleGamemodeScreen);
        }
        gamemodeChooser.SetActive(toggleGamemodeScreen);
    }

    public void PlayGame(int gameMode)
    {
        StartCoroutine(LoadGameMode(gameMode));
    }

    public IEnumerator LoadGameMode(int gameMode)
    {
        chosenGamemode = gameMode;

        //waits a second before executing everything past this
        yield return new WaitForSeconds(1);

        gameManager.MainGamePanel.SetActive(true);
        gameManager.InMainGame();
        gameManager.UnPauseGame();
        gameManager.MainMenuPanel.SetActive(false);
        gameManager.PI.SwitchCurrentActionMap("Player");

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
        if (chosenGamemode == 2)
        {
            startButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(true);
        }
    }
}


