using UnityEngine;

public class DebugSkip : MonoBehaviour
{
    // Skip to the end of the game
    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha0)) {
            LevelManager levelManager = FindFirstObjectByType<LevelManager>();
            levelManager.LoadGameEnd();
        }
    }
}
