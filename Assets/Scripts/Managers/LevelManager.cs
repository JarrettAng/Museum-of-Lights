using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadOutdoors() {
        SceneManager.LoadScene("Main");
    }

    public void LoadIndoors() {
        SceneManager.LoadScene("Indoors");
    }
}
