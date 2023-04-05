using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        audioMixer.GetFloat("MasterVolume", out float audioValue);
        slider.value = Mathf.Pow(10, audioValue / 20);
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}
