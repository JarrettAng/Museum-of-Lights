using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    public UnityEvent m_OnTrigger;

    [SerializeField]
    private bool m_onlyOnce = true;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            m_OnTrigger.Invoke();
        }   
        if (m_onlyOnce) {
            gameObject.SetActive(false);
        }
    }
}
