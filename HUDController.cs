using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject UIHelpers;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject objectivesMenu;
    [SerializeField] TextMeshProUGUI objectiveTitleText;
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] GameObject messagePromptPanel;
    [SerializeField] TextMeshProUGUI messagePromptText;

    private BoxCollider UICollider;

    private void Awake()
    {
        UICollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //if (OVRInput.GetDown(OVRInput.Button.Start))
        //{
        //    if (SceneManager.GetActiveScene().name.Equals("Museum"))
        //    {
        //        ToggleInMainMenu();
        //    }
        //    else
        //    {
        //        if (!messagePromptPanel.activeSelf)
        //        {
        //            TogglePauseMenu();
        //        }
        //    }            
        //}
    }

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

    public void EnableUICollider()
    {
        UICollider.enabled = true;
    }

    public void DisableUICollider()
    {
        UICollider.enabled = false;
    }

    public void ToggleUICollider()
    {
        if (UICollider.enabled)
        {
            DisableUICollider();
        }
        else
        {
            EnableUICollider();
        }
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
        DisableUIHelpers();
        DisableHUDCanvas();
        DisableUICollider();
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
        DisableUIHelpers();
        DisableObjectivesMenu();
        DisableHUDCanvas();
        DisableUICollider();
    }

    public void SetObjectiveTitleText(string objectiveTitle)
    {
        objectiveTitleText.text = objectiveTitle;
    }

    public void SetObjectiveText(string objective)
    {
        objectiveText.text = objective;
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

    public void SetMessagePromptText(string message)
    {
        messagePromptText.text = message;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
