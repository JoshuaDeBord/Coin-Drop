using System.Linq.Expressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OnOffColorChange : MonoBehaviour
{
    public Image ButtonSettings;
    public TextMeshProUGUI OnOffLabel;

    public TextMeshProUGUI OnOffLabelMainMenu;
    public bool isOn = true;

    public void ColorOffOn()
    {
        isOn = !isOn;
        Thread.Sleep(50);

        if (isOn == true && ButtonSettings.color != new Color32(15, 255, 0, 255))
        {
            ButtonSettings.color = new Color32(15, 255, 0, 255);
            OnOffLabel.SetText("ON");
            OnOffLabel.fontSize = 191.76f;

            

        }
        else if (isOn == false && ButtonSettings.color != new Color32(255, 0, 2, 255))
        {
            ButtonSettings.color = new Color32(255, 0, 2, 255);
            OnOffLabel.SetText("OFF");
            OnOffLabel.fontSize = 150;

            


        }
    }
}
