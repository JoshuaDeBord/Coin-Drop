using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    public string inputedCheatCode;
    public TMP_InputField CheatBox;
    public GameManager gameManager;
    public EventSystemHelper esh;
    public bool cupCoverIsOn = false;
    public bool spinScreen = false;
    

    public GameObject safeAreaObject;
    void Start()
    {

    }


    void Update()
    {
        if (gameManager.isCheatsUsed == true)
        {
            if(gameManager.playerData.isCheatsEnabled == false)
            {
                gameManager.playerData.isCheatsEnabled = true;
                Debug.Log("Cheats are enabled and saved");
            }
        }

    }
    
    public void PullInputedText()
    {
        inputedCheatCode = CheatBox.text.ToLower();
        Debug.Log("Cheat Code Entered: " + inputedCheatCode);
        
        

        if (inputedCheatCode == "give1000")
        {
            gameManager.isCheatsUsed = true;
            gameManager.pointsAssign += 1000;
            StartCoroutine(CheatCodeActivate());
        }
        
        else if (inputedCheatCode == "pride")
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
        
        else if (inputedCheatCode == "rapidfire")
        {
            gameManager.isCheatsUsed = true;
            gameManager.rapidSpawn = true;
            StartCoroutine(CheatCodeActivate());
        }
        
        else if (inputedCheatCode == "covercups")
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

        else if (inputedCheatCode == "unplayable")
        {
            if (spinScreen == false)
            {
                gameManager.isCheatsUsed = true;
                StartCoroutine(CheatCodeActivate());
                StartCoroutine(gameManager.SpinSafeArea());
                spinScreen = true;
            }
            else
            {
                StartCoroutine(CheatCodeDeactivate());
                spinScreen = false;
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

        EventSystem.current.SetSelectedGameObject(esh.cheatBoxButtonObject.gameObject);

#if UNITY_IOS || UNITY_ANDROID
        esh.CheatBox.transform.localPosition = esh.CheatBoxOGSpawnLocation;
#endif
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
