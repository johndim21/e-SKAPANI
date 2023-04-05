using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IppodromosLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject eutuxisPrefab;
    [SerializeField] private DialoguePoint dialoguePoint1;
    [SerializeField] private GameObject map;
    [SerializeField] private AmaksaIppodromou amaksa;

    private GameObject player;
    private HUDController hudController;
    private DialogueController dialogueController;
    private OVRScreenFade cameraFade;

    public static bool isIppodromosVisited;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        cameraFade = player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();

        dialogueController = eutuxisPrefab.GetComponent<DialogueController>();
        StartCoroutine(EutuxisFirstDialogue());
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

    IEnumerator EutuxisFirstDialogue()
    {
        yield return new WaitForSeconds(dialogueController.PlayDialogue("6_1_1"));
        amaksa.StartMoving();
        yield return new WaitForSeconds(28f);
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        amaksa.StopMoving();
        Destroy(amaksa.GetComponent<Rigidbody>());
        amaksa.transform.position = new Vector3(-57.6f, 11.286f, 15.65f);
        amaksa.transform.Rotate(new Vector3(0, 180f, 0));
        cameraFade.FadeIn();
        isIppodromosVisited = true;
        if (VasilikiLevelManager.isVasilikiVisited && OktagonoLevelManager.isOktagonoVisited && isIppodromosVisited)
        {
            StartCoroutine(EutuxisEndGame());
        }
        else
        {
            StartCoroutine(EutuxisMapPrompt());
        }
    }

    IEnumerator EutuxisMapPrompt()
    {
        yield return new WaitForSeconds(dialogueController.PlayDialogue("MapPrompt"));
        map.SetActive(true);
        hudController.EnableUIHelpers();
    }

    IEnumerator EutuxisEndGame()
    {
        yield return new WaitForSeconds(dialogueController.PlayDialogue("EndGame"));
        hudController.LoadCreditsLevel();
    }
}
