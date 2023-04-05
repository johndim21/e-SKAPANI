using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private DialoguePoint dialoguePoint1;
    [SerializeField] private DialoguePoint dialoguePoint2;
    [SerializeField] private DialoguePoint dialoguePoint3;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private GameObject enterAmaksaCanvas;

    private GameObject player;
    private HUDController hudController;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();
        enterAmaksaCanvas.SetActive(false);
        dialoguePoint2.gameObject.SetActive(false);
        dialoguePoint3.gameObject.SetActive(false);
        StartCoroutine(TuxiFirstDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
            hudController.ToggleUIHelpers();
        }
    }

    private void InitHUD()
    {
        hudController.DisablePauseMenu();
        hudController.DisableMessagePromptPanel();
        hudController.DisableHUDCanvas();
        hudController.DisableObjectivesMenu();
        hudController.DisableSettingsMenu();
        hudController.DisableUIHelpers();
    }

    IEnumerator TuxiFirstDialogue()
    {
        yield return new WaitForSeconds(dialogueController.PlayDialogue("2_1_1"));
        StartCoroutine(TuxiSecondDialogue());
    }

    IEnumerator TuxiSecondDialogue()
    {
        yield return new WaitUntil(() => dialoguePoint1.IsPlayerIn);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("2_1_2"));
        dialoguePoint1.DestroyDialoguePoint();
        StartCoroutine(AmaksasFirstDialogue());
    }

    IEnumerator AmaksasFirstDialogue()
    {
        dialoguePoint2.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialoguePoint2.IsPlayerIn);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("2_2_1"));
        dialoguePoint2.DestroyDialoguePoint();
        StartCoroutine(EnterAmaksa());
    }

    IEnumerator EnterAmaksa()
    {
        dialoguePoint3.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialoguePoint3.IsPlayerIn);
        enterAmaksaCanvas.SetActive(true);
    }
}
