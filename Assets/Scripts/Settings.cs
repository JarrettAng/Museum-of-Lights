using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private Slider m_masterVolumeSlider;
    [SerializeField]
    private Slider m_musicVolumeSlider;
    [SerializeField]
    private Slider m_sfxVolumeSlider;

    private void Start() {
        // Get audio levels
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        m_masterVolumeSlider.value = audioManager.GetMasterLevel();
        m_musicVolumeSlider.value = audioManager.GetMusicLevel();
        m_sfxVolumeSlider.value = audioManager.GetSFXLevel();
    }

    public void MasterVolumeChange(float _volume)
    {
        FindFirstObjectByType<AudioManager>().SetMasterLevel(_volume);
    }
    public void MusicVolumeChange(float _volume)
    {
        FindFirstObjectByType<AudioManager>().SetMusicLevel(_volume);

    }
    public void SFXVolumeChange(float _volume)
    {
        FindFirstObjectByType<AudioManager>().SetSFXLevel(_volume);
    }
}
