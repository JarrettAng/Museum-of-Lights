using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject creditsPanel;

    public GameObject ambienceObject;
    public GameObject bgmObject;

    private AudioSource ambienceAudioSource;
    private AudioSource bgmAudioSource;

    private bool gamePaused = false;

    private void Start()
    {
        if (ambienceObject != null)
        {
            ambienceAudioSource = ambienceObject.GetComponent<AudioSource>();
        }
        if (bgmObject != null)
        {
            bgmAudioSource = bgmObject.GetComponent<AudioSource>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused && pauseMenuPanel.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenuPanel.SetActive(false);
                ambienceAudioSource.Play();
                bgmAudioSource.Play();
            }
            else if (gamePaused && settingsMenuPanel.activeSelf)
            {
                BackToPauseMenuFromSettings();
            }
            else if (!gamePaused && settingsMenuPanel.activeSelf)
            {
                BackToMainMenuFromSettings();
            }
            else if (gamePaused && creditsPanel.activeSelf)
            {
                BackToMainMenuFromCredits();
            }
            else if (!gamePaused && creditsPanel.activeSelf)
            {
                BackToPauseMenuFromCredits();
            }
            else if (!gamePaused && mainMenuPanel.activeSelf)
            {
                MainMenuToQuit();
            }
            else // Pause game
            {
                Time.timeScale = 0;
                pauseMenuPanel.SetActive(true);
                ambienceAudioSource.Pause();
                bgmAudioSource.Pause();
            }

            gamePaused = !gamePaused;
        }
    }

    public void SwitchPanel(GameObject currentPanel, GameObject targetPanel)
    {
        currentPanel.SetActive(false);
        targetPanel.SetActive(true);
    }

    public void MainMenuToSettings()
    {
        SwitchPanel(mainMenuPanel, settingsMenuPanel);
    }

    public void BackToPreviousMenuFromSettings()
    {
        if (gamePaused)
        {
            BackToPauseMenuFromSettings();
        }
        else
        {
            BackToMainMenuFromSettings();
        }
    }
    public void BackToPreviousMenuFromCredits()
    {
        if (gamePaused)
        {
            BackToPauseMenuFromCredits();
        }
        else
        {
            BackToMainMenuFromCredits();
        }
    }


    private void BackToMainMenuFromSettings()
    {
        SwitchPanel(settingsMenuPanel, mainMenuPanel);
    }

    public void PauseMenuToSettings()
    {
        SwitchPanel(pauseMenuPanel, settingsMenuPanel);
    }

    private void BackToPauseMenuFromSettings()
    {
        SwitchPanel(settingsMenuPanel, pauseMenuPanel);
    }

    public void PauseMenuToCredits()
    {
        SwitchPanel(pauseMenuPanel, creditsPanel);
    }

    private void BackToPauseMenuFromCredits()
    {
        SwitchPanel(creditsPanel, pauseMenuPanel);
    }

    public void MainMenuToCredits()
    {
        SwitchPanel(mainMenuPanel, creditsPanel);
    }

    private void BackToMainMenuFromCredits()
    {
        SwitchPanel(creditsPanel, mainMenuPanel);
    }

    public void PauseMenuToQuit()
    {
        Application.Quit(); // Quits the application
    }

    public void MainMenuToQuit()
    {
        Application.Quit(); // Quits the application
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
        gamePaused = false;
        ambienceAudioSource.Play();
        bgmAudioSource.Play();
    }
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
