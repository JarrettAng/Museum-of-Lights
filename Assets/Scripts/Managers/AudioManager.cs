using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public void SetMasterLevel(float _volume)
    {
        masterMixer.SetFloat("MasterParam", _volume);
    }

    public void SetSFXLevel(float _volume)
    {
        masterMixer.SetFloat("SFXParam", _volume);
    }

    public void SetMusicLevel(float _volume)
    {
        masterMixer.SetFloat("MusicParam", _volume);
    }
}
