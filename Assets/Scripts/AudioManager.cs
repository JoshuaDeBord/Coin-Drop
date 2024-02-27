using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameManager gameManager;

    private static AudioManager instance;

    public PlayerData playerData;
    public static AudioManager Instance { get { return instance; } }

    public AudioMixer masterMixer;
    public AudioSource musicAudioS, soundAudioS;
    public Slider musicSlider, soundSlider;
    public SettingsColorChange sCCMusic, sCCSounds;

    public float savedMusicVolume = -30f, savedSoundVolume = -30f;
    


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    void Start()
    {
        
        musicSlider.value = PlayerSettingsPreferences.GetMusicVolume();
        soundSlider.value = PlayerSettingsPreferences.GetSFXVolume();

        

        savedMusicVolume = musicAudioS.volume;
        savedSoundVolume = soundAudioS.volume;
        
        masterMixer.SetFloat("MasterVol", PlayerSettingsPreferences.GetMasterVolume());
        masterMixer.SetFloat("MusicVol", PlayerSettingsPreferences.GetMusicVolume());
        masterMixer.SetFloat("SFXVol", PlayerSettingsPreferences.GetSFXVolume());

        
       
    }

    public void ChangeSoundVolume(float soundLevel)
    {
        masterMixer.SetFloat("SFXVol", soundLevel);
        PlayerSettingsPreferences.SetSFXVolume(soundLevel);
        gameManager.settingIsChanged = true;
        gameManager.SavePlayer();
    }

    public void ChangeMusicVolume(float soundLevel)
    {
        masterMixer.SetFloat("MusicVol", soundLevel);
        PlayerSettingsPreferences.SetMusicVolume(soundLevel);
        gameManager.settingIsChanged = true;
        gameManager.SavePlayer();

    }

    void Update()
    {
        if (soundSlider.value == -80 && sCCSounds.isOn == true)
        {
            sCCSounds.ButtonSettings.color = new Color32(255, 0, 2, 255);
            sCCSounds.OnOffLabel.SetText("OFF");
            sCCSounds.OnOffLabel.fontSize = 150;
            sCCSounds.isOn = false;
        }

        if (soundSlider.value > -80 && sCCSounds.isOn == false)
        {
            sCCSounds.ButtonSettings.color = new Color32(15, 255, 0, 255);
            sCCSounds.OnOffLabel.SetText("ON");
            sCCSounds.OnOffLabel.fontSize = 191.76f;
            sCCSounds.isOn = true;
        }

            if (musicSlider.value == -80 && sCCMusic.isOn == true)
        {
            sCCMusic.ButtonSettings.color = new Color32(255, 0, 2, 255);
            sCCMusic.OnOffLabel.SetText("OFF");
            sCCMusic.OnOffLabel.fontSize = 150;
            sCCMusic.isOn = false;
        }

        if (musicSlider.value > -80 && sCCMusic.isOn == false)
        {
            sCCMusic.ButtonSettings.color = new Color32(15, 255, 0, 255);
            sCCMusic.OnOffLabel.SetText("ON");
            sCCMusic.OnOffLabel.fontSize = 191.76f;
            sCCMusic.isOn = true;
        }

        
    }


    


    public void SetObjectVolume()
    {
        foreach (GameObject objInList in gameManager.SpawnedInObjects)
        {
        }
        

    }


    
}
