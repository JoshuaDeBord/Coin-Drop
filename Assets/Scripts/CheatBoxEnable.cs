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

    public GameManager gameManager;

    private void Awake()
    {
        
        
        
    }

    private void Start()
    {
        
    }
    public void OpenCheatBox()
    {
        CheatCodeBox.gameObject.SetActive(true);

        gameManager.isCheatsUsed = true;
        gameManager.musicSlider.navigation = gameManager.ChangingSettings;
        gameManager.LeftSongButton.navigation = gameManager.LeftSongToCheats;
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
