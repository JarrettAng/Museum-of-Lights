using UnityEngine;

public class RotationPuzzle : Puzzle
{
    [SerializeField]
    // Hacked for game jam
    public Transform r_correctRotation;
    [SerializeField]
    // Hacked for game jam
    public float m_tolerance = 5f;

    public float m_RotationSpeed = 80.0f;

    private bool m_correctRot = false;

    new void Update() {
        base.Update();

        if (m_solvedOnce && m_correctRot) return;

        if (m_LockedIn) {
            if (Quaternion.Angle(transform.rotation, r_correctRotation.rotation) < m_tolerance) {
                m_correctRot = true;
            }
            else {
                m_correctRot = false;
            }
        }
    }

    protected override bool CheckSolved() {
        return m_correctRot;
    }
}
