using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilikiLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tuxiPrefab;
    [SerializeField] private DialoguePoint dialoguePoint1;
    [SerializeField] private DialoguePoint dialoguePoint2;
    [SerializeField] private GameObject map;

    private GameObject player;
    private HUDController hudController;
    private DialogueController dialogueController;
    private TuxiController tuxiController;
    private OVRScreenFade cameraFade;

    public static bool isVasilikiVisited;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        dialogueController = tuxiPrefab.GetComponent<DialogueController>();
        tuxiController = tuxiPrefab.GetComponent<TuxiController>();
        cameraFade = player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();

        tuxiController.DisableGrabHand();

        dialoguePoint2.gameObject.SetActive(false);

        StartCoroutine(TuxiFirstDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
            if (!map.activeSelf)
            {
                hudController.ToggleUIHelpers();
            }
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
        yield return new WaitUntil(() => dialoguePoint1.IsPlayerIn);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("7_1_1"));
        dialoguePoint1.DestroyDialoguePoint();
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        dialoguePoint2.gameObject.SetActive(true);
        tuxiPrefab.transform.position = new Vector3(34.05f, -40.78f, -1.69f);
        cameraFade.FadeIn();
        isVasilikiVisited = true;
        if(isVasilikiVisited && OktagonoLevelManager.isOktagonoVisited && IppodromosLevelManager.isIppodromosVisited)
        {
            StartCoroutine(TuxiEndGame());
        }
        else
        {
            StartCoroutine(TuxiMapPrompt());
        }
    }

    IEnumerator TuxiMapPrompt()
    {
        yield return new WaitUntil(() => dialoguePoint2.IsPlayerIn);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("MapPrompt"));
        dialoguePoint2.DestroyDialoguePoint();
        map.SetActive(true);
        hudController.EnableUIHelpers();
    }

    IEnumerator TuxiEndGame()
    {
        yield return new WaitUntil(() => dialoguePoint2.IsPlayerIn);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("EndGame"));
        hudController.LoadCreditsLevel();
    }
}
