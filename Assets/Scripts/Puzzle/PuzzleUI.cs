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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            m_puzzleUI.SetActive(!m_puzzleUI.activeSelf);
        }
    }

    public void SetLockInProgress(float progress) {
        if (progress < 0.1f) {
            m_progressCircle.gameObject.SetActive(false);
            return;
        }

        m_progressCircle.gameObject.SetActive(true);
        m_progressCircle.fillAmount = progress;
    }
}
