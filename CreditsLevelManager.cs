using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsLevelManager : MonoBehaviour
{
    private GameObject player;
    private HUDController hudController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
        }
    }

    private void InitHUD()
    {
        hudController.DisablePauseMenu();
        hudController.DisableMessagePromptPanel();
        hudController.DisableHUDCanvas();
        hudController.DisableObjectivesMenu();
        hudController.DisableSettingsMenu();
        hudController.EnableUIHelpers();
    }

    public void MainMenuResumeButton()
    {
        hudController.DisablePauseMenu();
        hudController.DisableHUDCanvas();
    }

    public void ShowObjectivesButton()
    {
        hudController.EnableObjectivesMenu();
        hudController.DisablePauseMenu();
    }

    public void CloseObjectivesButton()
    {
        hudController.DisableObjectivesMenu();
        hudController.DisableHUDCanvas();
    }

    public void ShowSettingsButton()
    {
        hudController.EnableSettingsMenu();
        hudController.DisablePauseMenu();
    }

    public void CloseSettingsButton()
    {
        hudController.DisableSettingsMenu();
        hudController.DisableHUDCanvas();
    }
}
