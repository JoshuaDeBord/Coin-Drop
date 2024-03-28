using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CheatBoxEnable : MonoBehaviour
{
    public GameObject cheatTextBox;


    public bool highGravityHeldDown = false;


    public TMP_InputField CheatCodeBox;

    void Start()
    {

    }


    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS


        
#else
        cheatTextBox.SetActive(true);
        
#endif
    }


    public void OpenCheatBox()
    {
        CheatCodeBox.ActivateInputField();
    }
    public void hGButtonHeldDown()
    {
        highGravityHeldDown = true;
        StartCoroutine(SettingsHeldDown());
    }
    public void hGButtonHeldUp()
    {
        highGravityHeldDown = false;
        StopCoroutine(SettingsHeldDown());
        
    }

    

    public IEnumerator SettingsHeldDown()
    {
        yield return new WaitForSeconds(2);
        if (highGravityHeldDown == true)
        {
            cheatTextBox.SetActive(true);
            Debug.Log("CHEATS OPENED!!");

        }
    }
}
