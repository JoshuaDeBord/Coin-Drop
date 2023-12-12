using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenuPanel, MainGamePanel, Settings;
    public GameObject BackSettingsButton;

    
    public ShopPoints ShopPoints;
    public int pointsAssign;

    public bool inMainMenuBool = true;

    void Start()
    {

    }

    void Update()
    {
        if (inMainMenuBool == true)
        {
            BackSettingsButton.SetActive(false);
        }
        else BackSettingsButton.SetActive(true);


    }

    private void FixedUpdate()
    {
        
        
    }

    public void GoToSettings()
    {
        if (inMainMenuBool == true)
        {
            MainMenuPanel.SetActive(true);
            Settings.SetActive(false);
        }
        else if (inMainMenuBool == false)
        {
            MainGamePanel.SetActive(true);
            Settings.SetActive(false);
        }
    }

    public void InMainMenu()
    {
        inMainMenuBool = true;


    }

    public void InMainGame()
    {
        inMainMenuBool = false;
    }
}
