using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public static class PlayerSettingsPreferences
{
    public static bool updated;
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", -30);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SoundVolume", -30);
    }

    public static void SetMasterVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("MasterVolume", soundLevel);
    }

    public static void SetMusicVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("MusicVolume", soundLevel);
    }

    public static void SetSFXVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("SoundVolume", soundLevel);
    }
}
