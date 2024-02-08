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


        if (inputedCheatCode == "give1000")
        {
            gameManager.pointsAssign += 1000;
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(CheatCodeActivate());
        }
        else if (inputedCheatCode == "pride")
        {
            if (gameManager.prideIsOn == false)
            {
                EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(CheatCodeActivate());
                gameManager.prideIsOn = true;
            }
            else
            {
                gameManager.prideIsOn = false;
                EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(CheatCodeActivate());
            }
        }
        else if (inputedCheatCode == "rapidspawn")
        {
            gameManager.rapidSpawn = true;
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(CheatCodeActivate());
        }
        else if (inputedCheatCode == "covercups")
        {
            if (cupCoverIsOn == false)
            {
                cupCoverIsOn = true;
                gameManager.isFloorCovered = true;
                gameManager.cupCover.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(CheatCodeActivate());
            }
            else
            {
                cupCoverIsOn = false;
                gameManager.cupCover.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(CheatCodeDeactivate());
            }
        }





        else if (inputedCheatCode == "")
        {
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(CheatCodeActivate());
        }


        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(CheatCodeDelayBadCode());
        }
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
