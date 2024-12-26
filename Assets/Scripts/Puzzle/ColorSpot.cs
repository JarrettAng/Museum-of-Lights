using UnityEngine;

public class ColorSpot : MonoBehaviour {
    [SerializeField]
    private ColorPuzzle r_puzzle;

    [SerializeField]
    private Transform r_lookRotation;

    [SerializeField]
    private Light r_light;

    [SerializeField]
    private Color[] m_colors;
    [SerializeField]
    private int m_startingColor = 0;

    public int m_currentColor { get; set; }

    private void Start() {
        r_light.color = m_colors[m_startingColor];
        m_currentColor = m_startingColor;
    }

    public void EnterPuzzle() {
        r_puzzle.enabled = true;
    }

    public void ExitPuzzle() {
        r_puzzle.enabled = false;
    }

    public Quaternion GetLookRotation() {
        return r_lookRotation.rotation;
    }
}
