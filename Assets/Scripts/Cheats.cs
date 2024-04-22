using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cheats : MonoBehaviour
{

    public TMP_InputField CheatBox;
    public GameManager gameManager;
    public EventSystemHelper esh;
    public bool cupCoverIsOn = false;
    public GameModesController gameModeController;


    public GameObject safeAreaObject;

    private void Start()
    {

    }

    private void Update()
    {
        if (gameManager.isCheatsUsed == true)
        {
            if (gameManager.playerData.isCheatsEnabled == false)
            {
                gameManager.playerData.isCheatsEnabled = true;
                Debug.Log("Cheats are enabled and saved");
                gameManager.SavePlayer();
            }
        }

    }

    public void PullInputedText(string cheatCodeEntered)
    {


        Debug.Log("Cheat Code Entered: " + cheatCodeEntered);



        if (cheatCodeEntered == "give1000")
        {
            gameManager.isCheatsUsed = true;
            
            if (gameModeController.chosenGamemode == 0)
                gameManager.classicSavedPoints += 1000;

            StartCoroutine(CheatCodeActivate());
        }

        else if (cheatCodeEntered == "pride")
        {
            if (gameManager.prideIsOn == false)
            {
                gameManager.isCheatsUsed = true;
                gameManager.prideIsOn = true;
                StartCoroutine(CheatCodeActivate());
                StartCoroutine(gameManager.PrideLoopColor());
            }
            else
            {
                gameManager.prideIsOn = false;
                StopCoroutine(gameManager.PrideLoopColor());
                StartCoroutine(CheatCodeActivate());
            }
        }

        else if (cheatCodeEntered == "rapidfire")
        {
            gameManager.isCheatsUsed = true;
            gameManager.rapidSpawn = true;
            StartCoroutine(CheatCodeActivate());
        }

        else if (cheatCodeEntered == "covercups")
        {
            if (cupCoverIsOn == false)
            {
                gameManager.isCheatsUsed = true;
                cupCoverIsOn = true;
                gameManager.isFloorCovered = true;
                gameManager.cupCover.SetActive(true);
                StartCoroutine(CheatCodeActivate());
            }
            else
            {
                cupCoverIsOn = false;
                gameManager.cupCover.SetActive(false);
                StartCoroutine(CheatCodeDeactivate());
            }
        }

        else if (cheatCodeEntered.StartsWith("debugscore"))
        {
            bool failed = false;
            gameManager.isCheatsUsed = true;
            try
            {
                int scoreSet = Convert.ToInt32(cheatCodeEntered[11..]);
                Debug.Log("Score = " + scoreSet);

                if (gameModeController.chosenGamemode == 0)
                    gameManager.classicSavedPoints = scoreSet;
                
            }
            catch
            {
                StartCoroutine(CheatCodeDelayBadCode());
                failed = true;
            }

            if (failed == false)
            {
                StartCoroutine(CheatCodeActivate());
            }
        }


        else if (cheatCodeEntered == "")
        {

            StartCoroutine(CheatCodeActivate());
        }


        else
        {
            StartCoroutine(CheatCodeDelayBadCode());
        }

        {
            EventSystem.current.SetSelectedGameObject(esh.cheatBoxButtonObject.gameObject);

#if UNITY_IOS || UNITY_ANDROID
            esh.CheatBox.transform.localPosition = esh.CheatBoxOGSpawnLocation;
#endif
        }
    }


    public void ResetText()
    {
        StopAllCoroutines();
        CheatBox.text = string.Empty;
    }
    public IEnumerator CheatCodeActivate()
    {
        CheatBox.text = "<i> Cheat Activated...</i>";
        yield return new WaitForSeconds(2);
        CheatBox.text = string.Empty;
    }

    public IEnumerator CheatCodeDeactivate()
    {
        CheatBox.text = "<i> Cheat Deactivated...</i>";
        yield return new WaitForSeconds(2);
        CheatBox.text = string.Empty;
    }

    public IEnumerator CheatCodeDelayBadCode()
    {
        CheatBox.text = "<i> Invalid Cheat Entered...</i>";
        yield return new WaitForSeconds(2);
        CheatBox.text = string.Empty;
    }

    public void CheatResetText()
    {
        CheatBox.text = string.Empty;
    }


}
