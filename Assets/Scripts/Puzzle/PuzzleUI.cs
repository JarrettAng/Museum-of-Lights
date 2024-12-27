using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_puzzleUI;

    [SerializeField]
    private Image m_progressCircle;

    private void Start() {
        m_puzzleUI.SetActive(false);
    }

    public void EnterPuzzleUI() {
        m_puzzleUI.SetActive(true);
    }

    public void ExitPuzzleUI() {
        m_puzzleUI.SetActive(false);
    }

    public void SetLockInProgress(float progress) {
        if (progress < 0.1f || progress >= 1.0f) {
            m_progressCircle.gameObject.SetActive(false);
            return;
        }

        m_progressCircle.gameObject.SetActive(true);
        m_progressCircle.fillAmount = progress;
    }
}
