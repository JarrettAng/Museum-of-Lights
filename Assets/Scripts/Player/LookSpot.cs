using UnityEngine;
using UnityEngine.UI;

public class LookSpot : MonoBehaviour
{
    [SerializeField]
    private Puzzle r_puzzle;

    [SerializeField]
    private Transform r_lookRotation;

    [SerializeField]
    private Image[] r_arrowIcons; // 0 is up, 1 is right, 2 is down, 3 is left

    private void Start() {
        if (r_puzzle.m_Horizontal) {
            r_arrowIcons[1].enabled = true;
            r_arrowIcons[3].enabled = true;
        }
        if (r_puzzle.m_Vertical) {
            r_arrowIcons[0].enabled = true;
            r_arrowIcons[2].enabled = true;
        }
    }

    public void EnterPuzzle() {
        r_puzzle.enabled = true;
        // Set view angle, current position to puzzle position
        r_puzzle.r_ViewAngle = r_lookRotation;
    }

    public void ExitPuzzle() {
        r_puzzle.enabled = false;
    }

    public Quaternion GetLookRotation() {
        return r_lookRotation.rotation;
    }

}
