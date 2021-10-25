using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    private GameObject player;
    private HUDController hudController;
    private AudioSource objectiveCompleteSound;

    private void Awake()
    {
        objectiveCompleteSound = GetComponent<AudioSource>();
    }

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
            hudController.ToggleUICollider();
            hudController.ToggleUIHelpers();
        }
    }

    private void InitHUD()
    {
        hudController.DisablePauseMenu();
        hudController.DisableMessagePromptPanel();
        hudController.DisableHUDCanvas();
        hudController.DisableObjectivesMenu();
        hudController.DisableUIHelpers();
        hudController.DisableUICollider();
        hudController.SetObjectiveTitleText("");
    }
}
