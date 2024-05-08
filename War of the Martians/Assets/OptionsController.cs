using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public Canvas canvas;
    public Slider masterVolumeSlider;
    public Slider ambienceVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;
    public AudioMixer audioMixer;

    private void Start() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        ambienceVolumeSlider.value = PlayerPrefs.GetFloat("AmbienceVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (canvas.enabled) {
                CloseOptions();
            } else {
                OpenOptions();
            }
        }
    }

    public void SetMasterVolume() {
        audioMixer.SetFloat("MasterVolume",ConvertToDec(masterVolumeSlider.value));
        PlayerPrefs.SetFloat("MasterVolume",masterVolumeSlider.value);
    }

    public void SetAmbienceVolume() {
        audioMixer.SetFloat("AmbienceVolume",ConvertToDec(ambienceVolumeSlider.value));
        PlayerPrefs.SetFloat("AmbienceVolume",ambienceVolumeSlider.value);
    }

    public void SetMusicVolume() {
        audioMixer.SetFloat("MusicVolume",ConvertToDec(musicVolumeSlider.value));
        PlayerPrefs.SetFloat("MusicVolume",musicVolumeSlider.value);
    }

    public void SetSFXVolume() {
        audioMixer.SetFloat("SFXVolume",ConvertToDec(SFXVolumeSlider.value));
        PlayerPrefs.SetFloat("SFXVolume",SFXVolumeSlider.value);
    }

    public void OpenOptions() {
        canvas.enabled = true;
    }

    public void CloseOptions() {
        canvas.enabled = false;
    }

    private float ConvertToDec(float sliderValue) {
        return Mathf.Log10(Mathf.Max(sliderValue,0.0001f)) * 20;
    }
}
