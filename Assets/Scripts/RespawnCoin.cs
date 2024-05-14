using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RespawnCoin : MonoBehaviour
{
    public Vector3 respawnPos;
    public Transform CoinMain;
    public PrideColorLoop DropTheCoin;
    public GameManager gameManager;
    public TimedGamemodeLeaderboard leaderBoard;
    public GameModesController gameModeController;
    public BombsGamemodeController bombsGamemodeController;
    public Cheats cheats;
    private MovingLeftAndRight MovingLAR;
    public PrideColorLoop PCL;

    public Button left, right, drop;

    private void Start()
    {
        respawnPos = transform.position;
        MovingLAR = GetComponent<MovingLeftAndRight>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    public void RespawnTheCoinCall(InputAction.CallbackContext context)
    {
        if (gameModeController.chosenGamemode == 0)
        {
            RestartGame();
            MovingLAR.rightLeftPressed = false;
        }
    }



    public IEnumerator ClearObjectsFromLists()
    {
        while (gameManager.SpawnedInObjects.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            gameManager.SpawnedInObjects.RemoveRange(0, gameManager.SpawnedInObjects.Count);
        }
    }

    public IEnumerator RemoveWait(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        gameManager.SpawnedInObjects.Clear();
    }

    public void ToggleCoinsOn(bool toggleObjectsActive, bool toggleDropButtonPressed)
    {
        gameManager.dropButtonPressed = toggleDropButtonPressed;

        if (gameManager.modelSelected == 1 && gameManager.inMainMenuBool == false)
        {
            gameManager.modelSelectedInScene[0].SetActive(toggleObjectsActive);
        }
        else if (gameManager.modelSelected == 2 && gameManager.inMainMenuBool == false)
        {
            gameManager.modelSelectedInScene[1].SetActive(toggleObjectsActive);
        }

    }

    public void RestartGame()
    {


        MovingLAR.rightLeftPressed = false;
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
        }
        catch { }

        if (gameManager.rapidSpawn == false)
        {
            gameManager.dropButtonPressed = false;

            if (gameManager.modelSelected == 1 && gameManager.inMainMenuBool == false)
            {
                gameManager.modelSelectedInScene[0].SetActive(true);
            }
            else if (gameManager.modelSelected == 2 && gameManager.inMainMenuBool == false)
            {
                gameManager.modelSelectedInScene[1].SetActive(true);
            }
        }

        if (gameModeController.chosenGamemode == 0)
        {
            foreach (GameObject obj in gameManager.SpawnedInObjects)
            {
                Destroy(obj);
                Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
                Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
                Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
                Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            }
        }

        StartCoroutine(RemoveWait(0.3f));

        if (gameManager.rapidSpawn == false)
        {
            gameManager.dropButtonPressed = false;
            MovingLAR.dropButtonAfterPress.interactable = false;
        }

        left.interactable = true;
        right.interactable = true;

        AddPoint.coinEnter = false;
        AddPoint.coinEntered = false;


        Debug.Log("Coin Reseted");

    }
}
