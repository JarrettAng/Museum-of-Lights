using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private bool m_lockCursorOnStart = false;
    [SerializeField]
    private bool m_unlockCursorOnStart = false;

    // Hacked for game jam
    private void Start() {
        if (m_lockCursorOnStart) {
            LockCursor();
        }
        if (m_unlockCursorOnStart) {
            UnlockCursor();
        }
    }

    public void LoadOutdoors() {
        // Load scene next frame
        StartCoroutine(LoadScene("Main"));
    }

    public void LoadIndoors() {
        StartCoroutine(LoadScene("Indoors"));
    }

    public void LoadGameEnd() {
        StartCoroutine(LoadFinalScene());
    }

    public void CloseGame() {
        Application.Quit();
    }

    private IEnumerator LoadScene(string sceneName) {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadFinalScene() {
        Camera camera = Camera.main;

        float step = camera.farClipPlane / 5f;
        // Reduce far view until it's near
        while (camera.farClipPlane > camera.nearClipPlane) {
            camera.nearClipPlane += step * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene("GameEnd");
        camera.nearClipPlane = 0.3f;
    }

    // Hacked for game jam
    public void StartInsideBGM() {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.SwapToInsideBGM();
    }
    public void StartOutsideBGM() {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.SwapToOutsideBGM();
    }
    public void StartOutsideAmbience() {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.StartOutsideAmbience();
    }
    public void StopOutsideAmbience() {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.StopOutsideAmbience();
    }

    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
