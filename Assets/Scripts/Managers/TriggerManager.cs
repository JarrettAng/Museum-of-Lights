using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent[] m_onTriggers;

    public void Trigger(int index, float delay = 0.0f) {
        if (delay == 0.0f) {
            m_onTriggers[index].Invoke();
        }
        else {
            StartCoroutine(TriggerEvent(index, delay));
        }
    }

    public void Trigger2HalfSecDelay(int index) {
        StartCoroutine(TriggerEvent(index, 2.5f));
    }

    private IEnumerator TriggerEvent(int index, float delay) {
        yield return new WaitForSeconds(delay);
        m_onTriggers[index].Invoke();
    }
}
