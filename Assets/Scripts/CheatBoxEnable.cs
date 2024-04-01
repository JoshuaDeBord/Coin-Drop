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

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        
    }
    
    
    public void OpenCheatBox()
    {
        CheatCodeBox.gameObject.SetActive(true);

        gameManager.isCheatsUsed = true;
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
