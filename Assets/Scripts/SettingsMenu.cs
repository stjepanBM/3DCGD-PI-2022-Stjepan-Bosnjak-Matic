using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider backgroundMusic;
    public TMP_Dropdown quality;

    private void Start()
    {
        SetVolumeSlider();
        SetBackgroundMusicSlider();
        SetQualityDropdown();
    }

    private void SetQualityDropdown()
    {
        int qual = PlayerPrefs.GetInt("quality");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        quality.value = qual;
    }

    private void SetBackgroundMusicSlider()
    {
        float backMusic = PlayerPrefs.GetFloat("backgroundMusic");
        audioMixer.SetFloat("backgroundMusic", backMusic);
        backgroundMusic.value = backMusic;
    }

    private void SetVolumeSlider()
    {

        float vol = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("Volume", vol);
        volumeSlider.value = vol;
    }



    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetBackgroundMusic(float backMusicVolume)
    {
        audioMixer.SetFloat("backgroundMusic", backMusicVolume);
        PlayerPrefs.SetFloat("backgroundMusic", backMusicVolume);
    }

}
