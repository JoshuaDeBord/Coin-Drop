using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
            StartCoroutine(CheatCodeActivate());
        }
        else if (inputedCheatCode == "pride")
        {
            if (gameManager.prideIsOn == false)
            {
                StartCoroutine(CheatCodeActivate());
                gameManager.prideIsOn = true;
            }
            else
            {
                gameManager.prideIsOn = false;
                StartCoroutine(CheatCodeActivate());
            }
        }
        else if (inputedCheatCode == "rapidspawn")
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
