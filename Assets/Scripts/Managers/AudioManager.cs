using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioSource BGMSource;
    public AudioSource AmbienceSource;
    public AudioSource SFXSource;

    public AudioSource UISource;
    public AudioClip[] UISounds;

    public void SetMasterLevel(float _volume)
    {
        masterMixer.SetFloat("MasterParam", Mathf.Log10(_volume) * 20);
    }

    public void SetSFXLevel(float _volume)
    {
        masterMixer.SetFloat("SFXParam", Mathf.Log10(_volume) * 20);
    }

    public void SetMusicLevel(float _volume)
    {
        masterMixer.SetFloat("MusicParam", Mathf.Log10(_volume) * 20);
    }

    // UI sounds
    public void PlayButtonClick() {
        if (UISource.clip != UISounds[0]) {
            UISource.clip = UISounds[0];
        }
        UISource.Play();
    }

    public void PlaySliderMove() {
        if (UISource.clip != UISounds[1]) {
            UISource.clip = UISounds[1];
        }
        UISource.Play();
    }
}
