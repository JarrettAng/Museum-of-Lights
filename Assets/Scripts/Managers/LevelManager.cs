using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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
        while (camera.nearClipPlane < camera.farClipPlane) {
            camera.nearClipPlane += step * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene("GameEnd");
        camera.nearClipPlane = 1000.0f;
    }
}
