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

    private IEnumerator LoadScene(string sceneName) {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }
}
