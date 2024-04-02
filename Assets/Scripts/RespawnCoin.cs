using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RespawnCoin : MonoBehaviour
{
    public Vector3 respawnPos;
    public Transform CoinMain;
    public PrideColorLoop DropTheCoin;
    public GameManager gameManager;
    public LeaderBoard leaderBoard;
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
        RestartGame();
        MovingLAR.rightLeftPressed = false;
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

            

            /*if (gameManager.pointsAssign > 0 && gameManager.isCheatsUsed == false)
            {
                StartCoroutine(leaderBoard.SubmitScoreRoutine(gameManager.totalHighScore));
            }*/
        }

        foreach (GameObject obj in gameManager.SpawnedInObjects)
        {
            Destroy(obj);
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
            Destroy(GameObject.FindGameObjectWithTag("Coin Is Dropped"));
        }

        StartCoroutine(RemoveWait(1f));

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
        /*CoinMain.GetComponent<Rigidbody>().velocity = Vector3.zero;
        try
        {
            CoinMain.GetComponent<Rigidbody>().useGravity = false;
            transform.position = respawnPos;
            gameManager.modelSelect[0].transform.localPosition = new Vector3(0, 0, 0);
            gameManager.rbCoin[0].constraints = RigidbodyConstraints.FreezePosition;
            gameManager.rbCoin[0].constraints = RigidbodyConstraints.FreezeRotation;
        }
        finally
        {
            try
            {
                gameManager.rbCoin[1].useGravity = false;
                gameManager.rbCoin[1].velocity = Vector3.zero;
                gameManager.modelSelect[1].transform.position = respawnPos;
                gameManager.rbCoin[1].constraints = RigidbodyConstraints.FreezePosition;
                gameManager.rbCoin[1].constraints = RigidbodyConstraints.FreezeRotation;
                gameManager.rbCoin[1].transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            finally
            {
                left.interactable = true;
                right.interactable = true;
                MovingLAR.dropButtonAfterPress.interactable = false;
                AddPoint.coinEnter = false;
                AddPoint.coinEntered = false;
            }
        }*/
    }
}
