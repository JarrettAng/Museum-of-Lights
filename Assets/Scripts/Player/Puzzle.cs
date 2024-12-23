using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Puzzle : MonoBehaviour
{
    public UnityEvent OnSolve;

    public bool m_Horizontal;
    public bool m_Vertical;

    [SerializeField]
    private Transform r_correctRotation;
    [SerializeField]
    private float m_tolerance = 5f;

    [SerializeField]
    private float m_rotationSpeed = 80.0f;

    [SerializeField]
    private float m_lockInTime = 1.0f;
    
    public Transform r_ViewAngle;

    private bool m_lockedIn = false;
    private bool m_solved = false;
    private float m_lockInTimer = 0.0f;


    private void Update() {
        Vector3 input = Vector3.zero;

        HandleInput();
        CheckStatus();

        void HandleInput() {
            // Keyboard rotation
            if (m_Vertical) {
                if (Input.GetKey(KeyCode.W)) input.x += 1f; // W S: Rotate around X axis
                if (Input.GetKey(KeyCode.S)) input.x -= 1f;
            }

            if (m_Horizontal) {
                if (Input.GetKey(KeyCode.A)) input.y += 1f; // A D: Rotate around Y axis
                if (Input.GetKey(KeyCode.D)) input.y -= 1f;
            }

            if (m_Horizontal && m_Vertical) {
                if (Input.GetKey(KeyCode.Q)) input.z += 1f; // Q E: Rotate around Z axis
                if (Input.GetKey(KeyCode.E)) input.z -= 1f;
            }

            transform.Rotate(r_ViewAngle.up, input.y * m_rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(r_ViewAngle.right, input.x * m_rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(r_ViewAngle.forward, input.z * m_rotationSpeed * Time.deltaTime, Space.World);

            //if (input.sqrMagnitude > 0.1f) {
            //    input *= m_rotationSpeed * Time.deltaTime;
            //    transform.Rotate(input, Space.Self);
            //}

        }

        void CheckStatus() {
            if (m_solved) return;

            // If no input, increment lock in timer
            if (input.sqrMagnitude < 0.1f && !Input.GetMouseButton(0)) {
                if (m_lockInTimer < m_lockInTime) {
                    m_lockInTimer += Time.deltaTime;
                    m_lockedIn = false;
                }
            }
            else {
                m_lockInTimer = 0.0f;
            }

            // If locked in, check if the player has solved the puzzle
            if (m_lockInTimer > m_lockInTime) {
                if (!m_lockedIn) {
                    m_lockedIn = true;
                    Debug.LogFormat("Locked in angle {0}", Quaternion.Angle(transform.rotation, r_correctRotation.rotation));

                    if (Quaternion.Angle(transform.rotation, r_correctRotation.rotation) < m_tolerance) {
                        m_solved = true;
                        OnSolve.Invoke();
                    }
                }
            }
        }
    }


    public bool Solved() {
        return m_solved;
    }
}
