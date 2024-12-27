using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_moveTutorial;
    [SerializeField]
    private GameObject m_lookTutorial;
    [SerializeField]
    private GameObject m_pauseTutorial;
    [SerializeField]
    private GameObject m_interactTutorial;
    [SerializeField]
    private GameObject m_puzzleTutorial;

    private static bool m_hasDoneTutorial = false;
    private bool m_hasMoved = false, m_hasLooked = false, m_hasPaused = false;

    private void Start() {
        if (!m_hasDoneTutorial) {
            StartCoroutine(ControlsTutorial());
        }
    }

    private void Update() {
        if (m_hasDoneTutorial) return;

        if (!m_hasMoved) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                m_hasMoved = true;
            }
        }
        if (!m_hasLooked) {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
                m_hasLooked = true;
            }
        }
        if (!m_hasPaused) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                m_hasPaused = true;
            }
        }
    }

    private IEnumerator ControlsTutorial() {
        yield return new WaitForSeconds(0.5f);

        if (!m_hasMoved) {
            m_moveTutorial.SetActive(true);
            while (!m_hasMoved) {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2.0f);
            m_moveTutorial.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        if (!m_hasLooked) {
            m_lookTutorial.SetActive(true);
            while (!m_hasLooked) {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2.0f);
            m_lookTutorial.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        if (!m_hasPaused) {
            float timeLimit = 2f, timer = 0f;
            m_pauseTutorial.SetActive(true);
            while (!m_hasPaused && timer < timeLimit) {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2.0f);
            m_pauseTutorial.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        m_hasDoneTutorial = true;
    }

    public void ShowInteractTutorial() {
        m_interactTutorial.SetActive(true);
    }

    public void ShowPuzzleTutorial() {
        m_puzzleTutorial.SetActive(true);
    }
}
