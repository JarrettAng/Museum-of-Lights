using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_selectedSound;

    public void PlaySound() {
        GetComponent<AudioSource>().Play();
    }

    public void PlaySelectedSound() {
        GetComponent<AudioSource>().clip = m_selectedSound;
        GetComponent<AudioSource>().Play();
    }
}
