using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = SoundManager.Instance.GetMasterVolume();
        bgmSlider.value = SoundManager.Instance.GetBGMVolume();
        sfxSlider.value = SoundManager.Instance.GetSFXVolume();

        masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
    }

    void OnMasterSliderChanged(float value)
    {
        masterSlider.value = value;
    }
    void OnBGMSliderChanged(float value)
    {
        bgmSlider.value = value;
    }
    void OnSFXSliderChanged(float value)
    {
        sfxSlider.value = value;
    }
}
