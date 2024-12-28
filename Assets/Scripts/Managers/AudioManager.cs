using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public AudioMixer masterMixer;
    public AudioSource AmbienceSource;
    public AudioSource SFXSource;

    // BGM related
    public AudioSource BGMSource;
    public AudioSource OutsideSource;
    public AudioSource InsideSource;
    private float outsideVolume;
    private float insideVolume;
    private float ambienceVolume;

    // UI related
    public AudioSource UISource;
    public AudioClip[] UISounds;

    private void Start() {
        // Set initial volumes
        outsideVolume = OutsideSource.volume;
        insideVolume = InsideSource.volume;
        ambienceVolume = AmbienceSource.volume;
    }

    public void SetMasterLevel(float _volume) {
        masterMixer.SetFloat("MasterParam", Mathf.Log10(_volume) * 20);
    }

    public float GetMasterLevel() {
        float volume;
        masterMixer.GetFloat("MasterParam", out volume);
        volume = Mathf.Pow(10, volume / 20);

        return volume;
    }

    public void SetSFXLevel(float _volume) {
        masterMixer.SetFloat("SFXParam", Mathf.Log10(_volume) * 20);
    }

    public float GetSFXLevel() {
        float volume;
        masterMixer.GetFloat("SFXParam", out volume);
        volume = Mathf.Pow(10, volume / 20);

        return volume;
    }

    public void SetMusicLevel(float _volume) {
        masterMixer.SetFloat("MusicParam", Mathf.Log10(_volume) * 20);
    }

    public float GetMusicLevel() {
        float volume;
        masterMixer.GetFloat("MusicParam", out volume);
        volume = Mathf.Pow(10, volume / 20);

        return volume;
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

    public void SwapToInsideBGM() {
        StartCoroutine(SwapBGM(OutsideSource, InsideSource, OutsideSource.volume, insideVolume));
    }

    public void SwapToOutsideBGM() {
        StartCoroutine(SwapBGM(InsideSource, OutsideSource, InsideSource.volume, outsideVolume));
    }

    // Based on the assumption fade over 5 seconds
    private IEnumerator SwapBGM(AudioSource _out, AudioSource _in, float _outCurrent, float _inTarget) {
        // Fade out the current BGM and fade in the new BGM
        _in.volume = 0.0f;

        _in.Play();

        float outStep = _outCurrent / 5.0f;
        float inStep = _inTarget / 5.0f;

        while (_out.volume > 0.0f) {
            _out.volume -= Time.deltaTime * outStep;
            _in.volume += Time.deltaTime * inStep;
            yield return new WaitForEndOfFrame();
        }

        _out.volume = 0.0f;
        _in.volume = _inTarget;

        _out.Stop();
    }

    public void StartOutsideAmbience() {
        StartCoroutine(PlayAmbience(0.0f, ambienceVolume));
    }
    public void StopOutsideAmbience() {
        StartCoroutine(PlayAmbience(ambienceVolume, 0.0f));
    }
    private IEnumerator PlayAmbience(float _start, float _end) {
        if (_start < _end) {
            AmbienceSource.volume = _start;
            AmbienceSource.Play();
            while (AmbienceSource.volume < _end) {
                AmbienceSource.volume += Time.deltaTime / 5.0f;
                yield return new WaitForEndOfFrame();
            }
        }
        else {
            while (AmbienceSource.volume > _end) {
                AmbienceSource.volume -= Time.deltaTime / 5.0f;
                yield return new WaitForEndOfFrame();
            }
            AmbienceSource.Stop();
        }
    }
}