using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject UIHelpers;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject objectivesMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject messagePromptPanel;
    [SerializeField] TextMeshProUGUI messagePromptText;
    [SerializeField] private OVRScreenFade cameraFade;

    public void EnableHUDCanvas()
    {
        HUDCanvas.SetActive(true);
    }

    public void DisableHUDCanvas()
    {
        HUDCanvas.SetActive(false);
    }

    public void ToggleHUDCanvas()
    {
        HUDCanvas.SetActive(!HUDCanvas.activeSelf);
    }

    public void EnableUIHelpers()
    {
        UIHelpers.SetActive(true);
    }

    public void DisableUIHelpers()
    {
        UIHelpers.SetActive(false);
    }

    public void ToggleUIHelpers()
    {
        UIHelpers.SetActive(!UIHelpers.activeSelf);
    }

    public void ResumeButton()
    {
        DisablePauseMenu();
        DisableHUDCanvas();
        if (GameObject.FindGameObjectWithTag("Map") == null)
        {
            DisableUIHelpers();
        }
    }

    public void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void EnableObjectivesMenu()
    {
        objectivesMenu.SetActive(true);
    }

    public void DisableObjectivesMenu()
    {
        objectivesMenu.SetActive(false);
    }

    public void ShowObjectivesButton() {
        DisablePauseMenu();
        EnableObjectivesMenu();
    }

    public void CloseObjectivesButton()
    {
        DisableObjectivesMenu();
        DisableHUDCanvas();
        if (GameObject.FindGameObjectWithTag("Map") == null)
        {
            DisableUIHelpers();
        }
    }

    public void EnableSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void DisableSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void ShowSettingsButton()
    {
        DisablePauseMenu();
        EnableSettingsMenu();
    }

    public void CloseSettingsButton()
    {
        DisableSettingsMenu();
        DisableHUDCanvas();
        if (GameObject.FindGameObjectWithTag("Map") == null)
        {
            DisableUIHelpers();
        }
    }

    public void EnableMessagePromptPanel()
    {
        messagePromptPanel.SetActive(true);
    }

    public void DisableMessagePromptPanel()
    {
        messagePromptPanel.SetActive(false);
    }

    public bool IsMessagePromptPanelActive()
    {
        return messagePromptPanel.activeSelf;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        StartCoroutine(LoadAsyncScene("Museum"));
    }

    public void LoadEgnatiaLevel()
    {
        StartCoroutine(LoadAsyncScene("ReachingCity"));
    }

    public void LoadAmaksaLevel()
    {
        StartCoroutine(LoadAsyncScene("InsideWagon2"));
    }

    public void LoadKamaraLevel()
    {
        StartCoroutine(LoadAsyncScene("KamaraEdit"));
    }

    public void LoadOktagonoLevel()
    {
        StartCoroutine(LoadAsyncScene("Oktagono"));
    }

    public void LoadIppodromosLevel()
    {
        StartCoroutine(LoadAsyncScene("Ippodromos"));
    }

    public void LoadVasilikiLevel()
    {
        StartCoroutine(LoadAsyncScene("Vasiliki"));
    }

    public void LoadCreditsLevel()
    {
        StartCoroutine(LoadAsyncScene("FinalCredits"));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
