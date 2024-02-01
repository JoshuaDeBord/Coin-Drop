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
            StartCoroutine(CheatCodeDelay());
        }
        else if (inputedCheatCode == "pride")
        {
            if (gameManager.prideIsOn == false)
            {
                StartCoroutine(CheatCodeDelay());
                gameManager.prideIsOn = true;
            }
            else
            {
                gameManager.prideIsOn = false;
                StartCoroutine(CheatCodeDelay());
            }
        }
        else if (inputedCheatCode == "") { }
        

        else
        {
            StartCoroutine(CheatCodeDelayBadCode());
        }
    }

    public IEnumerator CheatCodeDelay()
    {
        CheatBox.text = "<i> Cheat Activated...</i>";
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
