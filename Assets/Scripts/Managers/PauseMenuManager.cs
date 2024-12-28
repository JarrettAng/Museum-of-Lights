using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject creditsPanel;
    public GameObject creditsPanel2;

    private AudioManager audioManager;
    private PlayerCamera playerCamera;

    [SerializeField]
    private AudioSource ambienceAudioSource;
    [SerializeField]
    private AudioSource bgmAudioSource;

    private bool gamePaused = false;

    private void Update()
    {
        // Hack into game jam
        if (!ambienceAudioSource) {
            audioManager = FindFirstObjectByType<AudioManager>();
            ambienceAudioSource = audioManager.AmbienceSource;
        }
        if (!bgmAudioSource) {
            audioManager = FindFirstObjectByType<AudioManager>();
            bgmAudioSource = audioManager.BGMSource;
        }
        if (!playerCamera) {
            playerCamera = FindFirstObjectByType<PlayerCamera>();
        }
        if (mainMenuPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused && pauseMenuPanel.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenuPanel.SetActive(false);
                ambienceAudioSource.Play();
                //bgmAudioSource.Play();

                // Enable camera input & disable cursor
                playerCamera.enabled = true;
                FindFirstObjectByType<LevelManager>().LockCursor();
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
                BackToPauseMenuFromCredits();
            }
            else if (!gamePaused && creditsPanel.activeSelf)
            {
                BackToMainMenuFromCredits();
            }
            else if (gamePaused && creditsPanel2.activeSelf)
            {
                BackToPauseMenuFromCredits2();
            }
            else if (!gamePaused && creditsPanel2.activeSelf)
            {
                BackToMainMenuFromCredits2();
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
                //bgmAudioSource.Pause();

                // Disable camera input & enable cursor
                playerCamera.enabled = false;
                FindFirstObjectByType<LevelManager>().UnlockCursor();
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

    public void BackToPreviousMenuFromCredits2()
    {
        if (gamePaused)
        {
            BackToPauseMenuFromCredits2();
        }
        else
        {
            BackToMainMenuFromCredits2();
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
    private void BackToMainMenuFromCredits2()
    {
        SwitchPanel(creditsPanel2, mainMenuPanel);
    }
    private void BackToPauseMenuFromCredits2()
    {
        SwitchPanel(creditsPanel2, pauseMenuPanel);
    }

    public void Credit1ToCredit2()
    {
        SwitchPanel(creditsPanel, creditsPanel2);
    }

    public void Credit2ToCredit1()
    {
        SwitchPanel(creditsPanel2, creditsPanel);
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
        //bgmAudioSource.Play();

        // Enable camera input & disable cursor
        playerCamera.enabled = true;
        FindFirstObjectByType<LevelManager>().LockCursor();
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void PlayUISound() 
    {
        audioManager.PlayButtonClick();
    }

    public void PlayerSliderSound() 
    {
        audioManager.PlaySliderMove();
    }
}
