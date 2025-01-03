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
    [SerializeField]
    private bool m_invertedX = false;
    [SerializeField]
    private bool m_intertedY = false;

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
        CheckLockIn();

        void HandleInput() {
            m_input = Vector3.zero;

            // Keyboard rotation
            if (m_vertical) {
                if (m_intertedY) {
                    if (Input.GetKey(KeyCode.W)) m_input.x -= 1f; // W S: Rotate around X axis
                    if (Input.GetKey(KeyCode.S)) m_input.x += 1f;
                }
                else {
                    if (Input.GetKey(KeyCode.W)) m_input.x += 1f; // W S: Rotate around X axis
                    if (Input.GetKey(KeyCode.S)) m_input.x -= 1f;
                }
            }

            if (m_horizontal) {
                if (m_invertedX) {
                    if (Input.GetKey(KeyCode.D)) m_input.y += 1f; // A D: Rotate around Y axis
                    if (Input.GetKey(KeyCode.A)) m_input.y -= 1f;
                }
                else {
                    if (Input.GetKey(KeyCode.A)) m_input.y += 1f; // A D: Rotate around Y axis
                    if (Input.GetKey(KeyCode.D)) m_input.y -= 1f;
                }
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
                    m_ui.SetLockInProgress(m_lockInTimer / m_lockInTime);
                }
            }
            else {
                r_puzzle.m_LockedIn = false;
                m_lockInTimer = 0.0f;
                m_ui.SetLockInProgress(0.0f);
            }

            // If locked in, check if the player has solved the puzzle
            if (m_lockInTimer > m_lockInTime) {
                if (!r_puzzle.m_LockedIn && m_solveableFromThisSpot) {
                    r_puzzle.m_LockedIn = true;
                }
            }
        }
    }

    public override void EnterPuzzle() {
        r_puzzle.enabled = true;
        m_currentlyActive = true;
        m_ui.EnterPuzzleUI();
    }

    public override void ExitPuzzle() {
        // Lock in on exit
        if (m_solveableFromThisSpot) {
            r_puzzle.m_LockedIn = true;
        }
        Invoke("DisablePuzzle", 0.1f);

        m_currentlyActive = false;
        m_ui.ExitPuzzleUI();
    }

    private void DisablePuzzle() {
        if (m_currentlyActive) return;
        r_puzzle.enabled = false;
    }

}
