using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject creditsPanel;

    public void SwitchPanel(GameObject currentPanel, GameObject targetPanel)
    {
        currentPanel.SetActive(false);
        targetPanel.SetActive(true);
    }

    public void MainMenuToSettings()
    {
        SwitchPanel(mainMenuPanel, settingsMenuPanel);
    }

    public void BackToMainMenuFromSettings()
    {
        SwitchPanel(settingsMenuPanel, mainMenuPanel);
    }

    public void PauseMenuToSettings()
    {
        SwitchPanel(pauseMenuPanel, settingsMenuPanel);
    }

    public void BackToPauseMenuFromSettings()
    {
        SwitchPanel(settingsMenuPanel, pauseMenuPanel);
    }

    public void PauseMenuToCredits()
    {
        SwitchPanel(pauseMenuPanel, creditsPanel);
    }

    public void BackToPauseMenuFromCredits()
    {
        SwitchPanel(creditsPanel, pauseMenuPanel);
    }

    public void PauseMenuToQuit()
    {
        Application.Quit(); // Quits the application
    }

    public void MainMenuToQuit()
    {
        Application.Quit(); // Quits the application
    }
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
        Debug.Log($"Opening URL: {url}");
    }
}
