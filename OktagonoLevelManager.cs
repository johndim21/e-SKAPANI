using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OktagonoLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject eutuxisPrefab;
    [SerializeField] private GameObject tuxiPrefab;
    [SerializeField] private DialoguePoint dialoguePoint1;
    [SerializeField] private DialoguePoint dialoguePoint2;
    [SerializeField] private DialoguePoint dialoguePoint3;
    [SerializeField] private GameObject map;
    [SerializeField] private OVRGrabbable plaka;
    [SerializeField] private GameObject spathi;
    [SerializeField] private OVRGrabbable spathiGrabbable;
    [SerializeField] private AgalmaGrabHand agalmaTuxis;
    [SerializeField] private OktagonoMarble marmaroThasou;
    [SerializeField] private OktagonoMarble marmaroLesvou;
    [SerializeField] private OktagonoMarble thessalikosLithos;
    [SerializeField] private OktagonoMarble marmaroKozanis;
    [SerializeField] private OktagonoMarble marmaroOnyxas;
    [SerializeField] private GameObject grabSwordCanvas;

    private GameObject player;
    private HUDController hudController;
    private DialogueController eutuxisDialogueController;
    private DialogueController tuxiDialogueController;
    private TuxiController tuxiController;
    private OVRScreenFade cameraFade;
    private OVRPlayerController playerController;
    private Animator eutuxisAnimatorController;
    private int isDialogueFinishedHash;
    private int isSwordGrabbedHash;

    public static bool isOktagonoVisited;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        playerController = player.GetComponent<OVRPlayerController>();
        cameraFade = player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();
        eutuxisDialogueController = eutuxisPrefab.GetComponent<DialogueController>();
        eutuxisAnimatorController = eutuxisPrefab.GetComponent<Animator>();
        isDialogueFinishedHash = Animator.StringToHash("isDialogueFinished");
        isSwordGrabbedHash = Animator.StringToHash("isSwordGrabbed");

        agalmaTuxis.DisableGrabHand();

        dialoguePoint2.gameObject.SetActive(false);
        dialoguePoint3.gameObject.SetActive(false);

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
        yield return new WaitUntil(() => dialoguePoint1.IsPlayerIn);
        yield return new WaitForSeconds(eutuxisDialogueController.PlayDialogue("5_1_1"));
        eutuxisAnimatorController.SetBool(isDialogueFinishedHash, true);
        dialoguePoint1.DisableDialoguePoint();
        yield return new WaitUntil(() => eutuxisAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Spathi_Standing"));
        spathi.gameObject.SetActive(false);
        spathiGrabbable.gameObject.SetActive(true);
        grabSwordCanvas.SetActive(true);
        yield return new WaitUntil(() => spathiGrabbable.isGrabbed);
        var spathiRigidbody = spathiGrabbable.GetComponent<Rigidbody>();
        grabSwordCanvas.SetActive(false);
        spathiGrabbable.gameObject.transform.parent = null;
        spathiRigidbody.constraints = RigidbodyConstraints.None;
        spathiRigidbody.useGravity = true;
        eutuxisAnimatorController.SetBool(isSwordGrabbedHash, true);
        eutuxisAnimatorController.SetBool(isDialogueFinishedHash, false);
        yield return new WaitUntil(() => !spathiGrabbable.isGrabbed);
        StartCoroutine(EutuxisSecondDialogue());
    }

    IEnumerator EutuxisSecondDialogue()
    {
        dialoguePoint1.EnableDialoguePoint();
        yield return new WaitUntil(() => dialoguePoint1.IsPlayerIn);
        yield return new WaitForSeconds(eutuxisDialogueController.PlayDialogue("5_1_2"));
        dialoguePoint1.DestroyDialoguePoint();
        agalmaTuxis.EnableGrabHand();
        yield return new WaitUntil(() => agalmaTuxis.IsHandInCollider);
        StartCoroutine(TuxiFirstDialogue());
    }

    IEnumerator TuxiFirstDialogue()
    {
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        Destroy(eutuxisPrefab);
        Destroy(agalmaTuxis);
        dialoguePoint2.gameObject.SetActive(true);
        tuxiPrefab.SetActive(true);
        tuxiDialogueController = tuxiPrefab.GetComponent<DialogueController>();
        tuxiController = tuxiPrefab.GetComponent<TuxiController>();
        tuxiController.DisableGrabHand();
        playerController.enabled = false;
        player.transform.position = new Vector3(-62, -2.9f, -23.35f);
        playerController.enabled = true;
        cameraFade.FadeIn();
        yield return new WaitUntil(() => dialoguePoint2.IsPlayerIn);
        yield return new WaitForSeconds(tuxiDialogueController.PlayDialogue("5_2_1"));
        dialoguePoint2.DestroyDialoguePoint();
        StartCoroutine(MarbleInteraction());
    }

    IEnumerator MarbleInteraction()
    {
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        marmaroThasou.gameObject.SetActive(true);
        marmaroLesvou.gameObject.SetActive(true);
        marmaroKozanis.gameObject.SetActive(true);
        thessalikosLithos.gameObject.SetActive(true);
        marmaroOnyxas.gameObject.SetActive(true);
        tuxiPrefab.SetActive(false);
        cameraFade.FadeIn();
        yield return new WaitUntil(() => marmaroThasou.IsMarbleSeen && marmaroLesvou.IsMarbleSeen && marmaroKozanis.IsMarbleSeen && thessalikosLithos.IsMarbleSeen && marmaroOnyxas.IsMarbleSeen);
        StartCoroutine(TuxiSecondDialogue());
    }

    IEnumerator TuxiSecondDialogue()
    {
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        dialoguePoint3.gameObject.SetActive(true);
        tuxiPrefab.transform.position = new Vector3(-79.68f, -3.818f, -23.8f);
        tuxiPrefab.transform.rotation = Quaternion.Euler(0, 90f, 0);
        tuxiPrefab.SetActive(true);
        StartCoroutine(tuxiPrefab.GetComponent<BlinkEyes>().Blink());
        plaka.gameObject.SetActive(true);
        var plakaRigidbody = plaka.GetComponent<Rigidbody>();
        plakaRigidbody.detectCollisions = false;
        cameraFade.FadeIn();
        yield return new WaitUntil(() => dialoguePoint3.IsPlayerIn);
        yield return new WaitForSeconds(tuxiDialogueController.PlayDialogue("5_2_2"));
        dialoguePoint3.DestroyDialoguePoint();
        tuxiController.EnableGrabHand();
        plakaRigidbody.detectCollisions = true;
        yield return new WaitUntil(() => plaka.isGrabbed);
        plaka.gameObject.transform.parent = null;
        plakaRigidbody.constraints = RigidbodyConstraints.None;
        plakaRigidbody.useGravity = true;
        tuxiController.DisableGrabHand();
        yield return new WaitUntil(() => !plaka.isGrabbed);
        isOktagonoVisited = true;
        if(isOktagonoVisited && IppodromosLevelManager.isIppodromosVisited && VasilikiLevelManager.isVasilikiVisited)
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
        yield return new WaitForSeconds(tuxiDialogueController.PlayDialogue("MapPrompt"));
        map.SetActive(true);
        hudController.EnableUIHelpers();
    }

    IEnumerator TuxiEndGame()
    {
        yield return new WaitForSeconds(tuxiDialogueController.PlayDialogue("EndGame"));
        hudController.LoadCreditsLevel();
    }
}
