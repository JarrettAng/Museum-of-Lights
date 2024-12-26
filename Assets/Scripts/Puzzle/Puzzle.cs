using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public UnityEvent OnSolve;

    public UnityEvent OnUnsolve;

    public bool m_LockedIn = false;

    [SerializeField]
    protected bool m_solvedOnce = true;
    [SerializeField]
    private bool m_solved = false;

    protected void Update() {
        if (m_solvedOnce && m_solved) return;
        
        if (CheckSolved()) {
            m_solved = true;
            OnSolve.Invoke();
        }
        else {
            if (m_solved) {
                m_solved = false;
                OnUnsolve.Invoke();
            }
        }
    }

    protected virtual bool CheckSolved() {
        return m_solved;
    }
}
