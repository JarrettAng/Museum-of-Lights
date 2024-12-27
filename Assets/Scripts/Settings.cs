using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
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
