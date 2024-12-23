using System.Collections;
using UnityEngine;

public class PlayerSolver : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement r_pMovement;
    [SerializeField]
    private PlayerCamera r_pCamera;

    private LookSpot r_currentLookSpot = null;
    private bool m_puzzleMode = false;

    private Coroutine m_lookAnim;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) && r_currentLookSpot) {
            r_pMovement.m_Enabled = m_puzzleMode;
            r_pCamera.m_Enabled = m_puzzleMode;

            m_puzzleMode = !m_puzzleMode;
            if (m_puzzleMode) {
                r_currentLookSpot.EnterPuzzle();

                if (m_lookAnim != null) {
                    StopCoroutine(m_lookAnim);
                }
                m_lookAnim = StartCoroutine(LookAtPuzzle());
            }
            else {
                r_currentLookSpot.ExitPuzzle();

                if (m_lookAnim != null) {
                    StopCoroutine(m_lookAnim);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Puzzle")) {
            r_currentLookSpot = other.GetComponent<LookSpot>();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Puzzle")) {
            r_currentLookSpot = null;
        }
    }

    private IEnumerator LookAtPuzzle() {
        Quaternion targetRotation = r_currentLookSpot.GetLookRotation();

        float angleDifference = Quaternion.Angle(r_pCamera.GetRotation(), targetRotation);

        // Lerp player's rotation to look at the puzzle
        while (angleDifference > 0.2f) {
            r_pCamera.SetRotation(Quaternion.Lerp(r_pCamera.GetRotation(), targetRotation, Time.deltaTime * 5.0f));
            angleDifference = Quaternion.Angle(r_pCamera.GetRotation(), targetRotation);
            yield return new WaitForEndOfFrame();
        }

        r_pCamera.SetRotation(targetRotation);
    }
}
