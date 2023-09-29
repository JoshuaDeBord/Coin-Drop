using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffColorChange : MonoBehaviour
{
    public Image ButtonSettings;
    public bool isOn = false;

    public void ColorOffOn()
    {
        isOn = !isOn;
    }

    private void Update()
    {
        if (isOn == true && ButtonSettings.color != new Color32(15, 255, 0, 255))
        {
            ButtonSettings.color = new Color32(15, 255, 0, 255);
        }
        else if (isOn == false && ButtonSettings.color != new Color32(255, 0, 2, 255))
        {
            ButtonSettings.color = new Color32(255, 0, 2, 255);
        }
    }
}
