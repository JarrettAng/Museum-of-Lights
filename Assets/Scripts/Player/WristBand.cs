using UnityEngine;
using UnityEngine.Events;

public class WristBand : MonoBehaviour
{
    [SerializeField]
    private bool m_isCollidingWithPlayer = false;

    public UnityEvent OnInteract;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && m_isCollidingWithPlayer) {
            OnInteract.Invoke();
            FindFirstObjectByType<PlayerSolver>().enabled = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            m_isCollidingWithPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            m_isCollidingWithPlayer = false;
        }
    }
}
