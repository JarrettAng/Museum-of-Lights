using UnityEngine;

public class Spot : MonoBehaviour
{
    [SerializeField]
    protected Transform r_lookRotation;

    [SerializeField]
    protected float m_lockInTime = 1.0f;
    [SerializeField]
    protected float m_lockInTimer = 0.0f;

    protected bool m_currentlyActive = false;

    protected Vector3 m_input = Vector3.zero;

    // Hacked in for game jam
    protected static PuzzleUI m_ui;

    private void Awake() {
        if (m_ui == null) {
            m_ui = FindFirstObjectByType<PuzzleUI>();
        }

    }

    public virtual void EnterPuzzle() {

    }

    public virtual void ExitPuzzle() {

    }

    public Quaternion GetLookRotation() {
        return r_lookRotation.rotation;
    }
}
