using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnEnablePanel : MonoBehaviour
{


    public GameObject SettingsFirstButton, MainMenuPlayButton, StoreFirstButton;

    public void OpenSettings()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        EventSystem.current.SetSelectedGameObject(SettingsFirstButton);
#endif
    }

    public void OpenMainMenu()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        EventSystem.current.SetSelectedGameObject(MainMenuPlayButton);
#endif
    }

    public void OpenShop()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        EventSystem.current.SetSelectedGameObject(StoreFirstButton);
#endif
    }


}
