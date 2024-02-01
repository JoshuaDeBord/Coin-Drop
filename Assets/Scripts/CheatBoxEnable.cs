using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CheatBoxEnable : MonoBehaviour
{
    public GameObject cheatTextBox;
    

    public bool PauseHeldDownBool = false;


    public TMP_InputField CheatCodeBox;

    void Start()
    {

    }


    void Update()
    {
#if UNITY_ANDROID && UNITY_IOS
        if (CheatCodeBox.isFocused)
        {
            CheatCodeBox.transform.localPosition = new Vector3(-1086.6f, 857.75f, 0);
        }
        else
        {
            CheatCodeBox.transform.localPosition = new Vector3(-1086.6f, -857.75f, 0);
        }

        if (PauseHeldDownBool == true)
        {
            StartCoroutine(SettingsHeldDown());
        }
#endif
    }


    public void OpenCheatBox()
    {
        CheatCodeBox.ActivateInputField();
    }
    public void PauseButtonHeldDown()
    {
        PauseHeldDownBool = true;
    }
    public void PauseButtonHeldUp()
    {
        PauseHeldDownBool = false;
    }




    public IEnumerator SettingsHeldDown()
    {
        yield return new WaitForSeconds(2);
        if (PauseHeldDownBool == true)
        {
            cheatTextBox.SetActive(true);
            Debug.Log("CHEATS OPENED!!");

        }
    }
}
