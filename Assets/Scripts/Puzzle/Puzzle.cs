using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public UnityEvent OnSolve;

    public bool m_LockedIn = false;

    [SerializeField]
    private bool m_solved = false;

    protected void Update() {
        if (m_solved) return;
        
        if (CheckSolved()) {
            m_solved = true;
            OnSolve.Invoke();
        }
    }

    protected virtual bool CheckSolved() {
        return m_solved;
    }
}
