using UnityEngine;
using UnityEngine.UI;

public class LightSpot : Spot
{
    [SerializeField]
    public bool m_horizontal;
    [SerializeField]
    public bool m_vertical;

    [SerializeField]
    private RotationPuzzle r_puzzle;

    [SerializeField]
    private Image[] r_arrowIcons; // 0 is up, 1 is right, 2 is down, 3 is left

    [SerializeField]
    private bool m_solveableFromThisSpot = true;

    private void Start() {
        if (m_horizontal) {
            r_arrowIcons[1].enabled = true;
            r_arrowIcons[3].enabled = true;
        }
        if (m_vertical) {
            r_arrowIcons[0].enabled = true;
            r_arrowIcons[2].enabled = true;
        }
    }

    public void Update() {
        if (!m_currentlyActive) return;

        HandleInput();
        if (m_solveableFromThisSpot) {
            CheckLockIn();
        }

        void HandleInput() {
            m_input = Vector3.zero;

            // Keyboard rotation
            if (m_vertical) {
                if (Input.GetKey(KeyCode.W)) m_input.x += 1f; // W S: Rotate around X axis
                if (Input.GetKey(KeyCode.S)) m_input.x -= 1f;
            }

            if (m_horizontal) {
                if (Input.GetKey(KeyCode.A)) m_input.y += 1f; // A D: Rotate around Y axis
                if (Input.GetKey(KeyCode.D)) m_input.y -= 1f;
            }

            if (m_horizontal && m_vertical) {
                if (Input.GetKey(KeyCode.Q)) m_input.z += 1f; // Q E: Rotate around Z axis
                if (Input.GetKey(KeyCode.E)) m_input.z -= 1f;
            }

            r_puzzle.transform.Rotate(r_lookRotation.up, m_input.y * r_puzzle.m_RotationSpeed * Time.deltaTime, Space.World);
            r_puzzle.transform.Rotate(r_lookRotation.right, m_input.x * r_puzzle.m_RotationSpeed * Time.deltaTime, Space.World);
            r_puzzle.transform.Rotate(r_lookRotation.forward, m_input.z * r_puzzle.m_RotationSpeed * Time.deltaTime, Space.World);
        }

        void CheckLockIn() {
            // If no input, increment lock in timer
            if (m_input.sqrMagnitude < 0.1f) {
                if (m_lockInTimer < m_lockInTime) {
                    m_lockInTimer += Time.deltaTime;
                    r_puzzle.m_LockedIn = false;
                }
            }
            else {
                m_lockInTimer = 0.0f;
            }

            // If locked in, check if the player has solved the puzzle
            if (m_lockInTimer > m_lockInTime) {
                if (!r_puzzle.m_LockedIn) {
                    r_puzzle.m_LockedIn = true;
                }
            }
        }
    }

    public override void EnterPuzzle() {
        r_puzzle.enabled = true;
        m_currentlyActive = true;
    }

    public override void ExitPuzzle() {
        r_puzzle.enabled = false;
        m_currentlyActive = false;
    }
}
