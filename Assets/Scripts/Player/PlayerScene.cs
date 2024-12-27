using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScene : MonoBehaviour
{
    private static PlayerScene m_instance;

    private void OnEnable() {
        if (m_instance == null) {
            m_instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Awake() {
        // Persistent player
        DontDestroyOnLoad(gameObject);

        // Spawn it at the start of the level
        //SceneManager.sceneLoaded += SpawnAtSpawnPoint;

        // Spawn at spawn point
        SpawnAtSpawnPoint();
    }

    //private void SpawnAtSpawnPoint(Scene scene, LoadSceneMode mode) {
    //    SpawnAtSpawnPoint();
    //}

    private void SpawnAtSpawnPoint() {
        Transform spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
}
