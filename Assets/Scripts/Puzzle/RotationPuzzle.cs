using UnityEngine;

public class RotationPuzzle : Puzzle
{
    [SerializeField]
    private Transform r_correctRotation;
    [SerializeField]
    private float m_tolerance = 5f;

    public float m_RotationSpeed = 80.0f;
    private bool m_correctRot = false;

    new void Update() {
        base.Update();

        if (m_correctRot) return;

        if (m_LockedIn) {
            if (Quaternion.Angle(transform.rotation, r_correctRotation.rotation) < m_tolerance) {
                m_correctRot = true;
            }
        }
    }

    protected override bool CheckSolved() {
        return m_correctRot;
    }
}
