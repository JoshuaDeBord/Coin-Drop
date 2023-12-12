using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnOffColorChangeHighGravity : MonoBehaviour
{
    public Image ButtonSettings;
    public TextMeshProUGUI OnOffLabel;
    
    
    public bool isOn = true;
    public DropTheCoin DropTheCoin;
    public void ColorOffOn()
    {
        isOn = !isOn;
        Thread.Sleep(50);

        if (isOn == true && ButtonSettings.color != new Color32(15, 255, 0, 255) && DropTheCoin.dropButtonPressed == false)
        {
            ButtonSettings.color = new Color32(15, 255, 0, 255);
            OnOffLabel.SetText("ON");
            OnOffLabel.fontSize = 191.76f;
            
            
        }
        else if (isOn == false && ButtonSettings.color != new Color32(255, 0, 2, 255) && DropTheCoin.dropButtonPressed == false)
        {
            ButtonSettings.color = new Color32(255, 0, 2, 255);
            OnOffLabel.SetText("OFF");
            OnOffLabel.fontSize = 150;

            
        }
    }
}
