using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    public string inputedCheatCode;
    public TMP_InputField CheatBox;
    public GameManager gameManager;
    public EventSystemHelper esh;
    public bool cupCoverIsOn = false;

    void Start()
    {

    }


    void Update()
    {


    }

    public void PullInputedText()
    {
        inputedCheatCode = CheatBox.text;
        Debug.Log("Cheat Code Entered: " + inputedCheatCode);
        EventSystem.current.SetSelectedGameObject(null);
        esh.CheatBox.transform.localPosition = esh.CheatBoxOGSpawnLocation;

        if (inputedCheatCode == "give1000")
        {
            gameManager.pointsAssign += 1000;
            StartCoroutine(CheatCodeActivate());
        }
        
        else if (inputedCheatCode == "pride")
        {
            if (gameManager.prideIsOn == false)
            {
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
        
        else if (inputedCheatCode == "rapidfire")
        {
            gameManager.rapidSpawn = true;
            StartCoroutine(CheatCodeActivate());
        }
        
        else if (inputedCheatCode == "covercups")
        {
            if (cupCoverIsOn == false)
            {
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





        else if (inputedCheatCode == "")
        {

            StartCoroutine(CheatCodeActivate());
        }


        else
        {
            StartCoroutine(CheatCodeDelayBadCode());
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
