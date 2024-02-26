using System.Linq.Expressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsColorChange : MonoBehaviour
{
    public Image ButtonSettings;
    public TextMeshProUGUI OnOffLabel;

    private AudioManager audioManager;
    public bool isOn = true;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {

    }
    public void ColorOffOn()
    {
        if (isOn == true)
        {
            ColorOff();
            isOn = !isOn;
        }
        else
        {
            ColorOn();
            isOn = !isOn;
        }
    }

    public void ColorOn()
    {
        ButtonSettings.color = new Color32(15, 255, 0, 255);
        OnOffLabel.SetText("ON");
        OnOffLabel.fontSize = 191.76f;

        if (this.name == "MUSIC BUTTON")
        {
            audioManager.musicSlider.value = audioManager.savedMusicVolume;
        }

        if (this.name == "SOUND EFFECTS BUTTON")
        {
            audioManager.soundSlider.value = audioManager.savedSoundVolume;
            audioManager.SetObjectVolume();
        }
    }

    public void ColorOff()
    {
        ButtonSettings.color = new Color32(255, 0, 2, 255);
        OnOffLabel.SetText("OFF");
        OnOffLabel.fontSize = 150;

        if (this.name == "MUSIC BUTTON")
        {
            audioManager.savedMusicVolume = audioManager.musicSlider.value;
            audioManager.musicSlider.value = -80;
            
            Debug.Log("music button pressed");
        }


        if (this.name == "SOUND EFFECTS BUTTON")
        {
            audioManager.savedSoundVolume = audioManager.soundSlider.value;

            audioManager.soundSlider.value = -80;
            Debug.Log("sounds button pressed");
        }
    }


}
