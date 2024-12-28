using UnityEngine;
using UnityEngine.Events;

public class ColorSpot : Spot 
{
    // Hack to change skybox color
    public UnityEvent m_OnColorChange;
    [SerializeField]
    private Skybox m_skybox;
    [SerializeField]
    private Material[] m_skyboxMaterial;

    [SerializeField]
    private Light r_light;
    [SerializeField]
    private Color[] m_colors;
    [SerializeField]
    private int m_startingColor = 0;

    // Hacked for game jam
    public int m_currentColor;

    [SerializeField]
    private ColorPuzzle r_puzzle;

    private void Start() {
        r_light.color = m_colors[m_startingColor];
        m_currentColor = m_startingColor;
    }

    public void Update() {
        if (!m_currentlyActive) return;

        HandleInput();
        CheckLockIn();

        void HandleInput() {
            m_input = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.A)) m_input.x -= 1f; // A D : Change colors
            if (Input.GetKeyDown(KeyCode.D)) m_input.x += 1f;

            if (m_input.sqrMagnitude > 0.1f) {
                m_currentColor += (int)m_input.x;
                if (m_currentColor < 0) m_currentColor = m_colors.Length - 1;
                if (m_currentColor >= m_colors.Length) m_currentColor = 0;

                r_light.color = m_colors[m_currentColor];
                m_OnColorChange.Invoke();
            }
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
                if (!r_puzzle.m_LockedIn) {
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
        r_puzzle.m_LockedIn = true;
        Invoke("DisablePuzzle", 0.1f);

        m_currentlyActive = false;
        m_ui.ExitPuzzleUI();
    }

    private void DisablePuzzle() {
        if (m_currentlyActive) return;
        r_puzzle.enabled = false;
    }

    public void ChangeSkyColor() {
        if (!m_skybox) {
            m_skybox = Camera.main.GetComponent<Skybox>();
        }
        m_skybox.material = m_skyboxMaterial[m_currentColor];
    }
}
