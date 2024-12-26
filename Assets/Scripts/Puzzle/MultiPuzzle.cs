using UnityEngine;
using UnityEngine.Events;

public class MultiPuzzle : MonoBehaviour
{
    public UnityEvent OnSolve;

    [SerializeField]
    private bool[] m_puzzlesSolved;

    [SerializeField]
    private bool m_solvedOnce = true;
    [SerializeField]
    private bool m_allSolved = false;

    public void SolvePuzzle(int index) {
        if (m_solvedOnce && m_allSolved) return;

        m_puzzlesSolved[index] = true;

        bool allSolved = true;
        for (int i = 0; i < m_puzzlesSolved.Length; i++) {
            if (!m_puzzlesSolved[i]) {
                allSolved = false;
                break;
            }
        }

        if (allSolved && !m_allSolved) {
            m_allSolved = true;
            OnSolve.Invoke();
        }
    }

    public void UnsolvePuzzle(int index) {
        if (m_solvedOnce && m_allSolved) return;

        m_puzzlesSolved[index] = false;
    }
}
