using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private OVRGrabbable flyerPrefab;
    [SerializeField] private GameObject sparksRising;
    [SerializeField] private GameObject basiMikroTokso;
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject tuxiPrefab;
    [SerializeField] private GameObject archBase;
    [SerializeField] private DialoguePoint dialoguePoint;
    [SerializeField] private GameObject highlightTokso;
    [SerializeField] private GameObject highlightEpikrana;
    [SerializeField] private GameObject highlightMarmara;
    [SerializeField] private GameObject romanMap;
    [SerializeField] private Image thessalonikiMapPanel;
    [SerializeField] private Sprite thessalonikiMapGR;
    [SerializeField] private Sprite thessalonikiMapEN;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject tutorial7Panel;
    [SerializeField] private GameObject egnatiaPanel;
    [SerializeField] private VideoPlayer romanMapVideoPlayer;
    [SerializeField] private RawImage romanMapRawImage;
    [SerializeField] private VideoClip romanMapVideoGR;
    [SerializeField] private VideoClip romanMapVideoEN;
    [SerializeField] private Slider volumeSlider;

    private GameObject player;
    private HUDController hudController;
    private OVRGrabbable flyerToDestroy;
    private AudioSource basiAudioSource;
    private Animator basiAnimator;
    private VideoPlayer screenVideoPlayer;
    private Material screenMaterial;
    private OVRScreenFade cameraFade;
    private TuxiController tuxiController;
    private DialogueController dialogueController;
    private bool tutorialFinished;

    // Start is called before the first frame update
    void Start()
    {
        OktagonoLevelManager.isOktagonoVisited = false;
        IppodromosLevelManager.isIppodromosVisited = false;
        VasilikiLevelManager.isVasilikiVisited = false;
        volumeSlider.value = 1;
        volumeSlider.GetComponent<VolumeSlider>().SetVolume(1);

        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        basiAnimator = basiMikroTokso.GetComponent<Animator>();
        basiAudioSource = basiMikroTokso.GetComponent<AudioSource>();

        screenVideoPlayer = screen.GetComponent<VideoPlayer>();
        screenMaterial = screen.GetComponent<Renderer>().material;

        cameraFade = player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();

        flyerToDestroy = Instantiate(flyerPrefab);

        dialoguePoint.gameObject.SetActive(false);

        tutorialFinished = false;

        Tutorial1Actions();

    }

    // Update is called once per frame
    void Update()
    {
        if (flyerToDestroy.transform.position.y < -2.5)
        {
            Destroy(flyerToDestroy.gameObject);
            flyerToDestroy = Instantiate(flyerPrefab);
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
            if (tutorialFinished)
            {
                hudController.ToggleUIHelpers();
            }
        }

        if (flyerToDestroy.isGrabbed)
        {
            sparksRising.SetActive(false);
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
        if (tutorialFinished)
        {
            hudController.DisableUIHelpers();
        }
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
        if (tutorialFinished)
        {
            hudController.DisableUIHelpers();
        }
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
        if (tutorialFinished)
        {
            hudController.DisableUIHelpers();
        }
    }

    public void Tutorial1Actions()
    {
        arrowRight.transform.position = rightController.transform.Find("IndexTriggerButtonPos").transform.position;
        arrowLeft.SetActive(false);
    }

    public void Tutorial2Actions()
    {
        arrowRight.SetActive(false);
        OVRPlugin.SetBoundaryVisible(true);
    }

    public void Tutorial3Actions()
    {
        //OVRPlugin.SetBoundaryVisible(false);
        arrowRight.SetActive(false);
        arrowLeft.transform.position = leftController.transform.Find("AnalogStickPos").transform.position;
        arrowLeft.SetActive(true);
    }

    public void Tutorial4Actions()
    {
        arrowLeft.transform.SetParent(leftController.transform.Find("HandTriggerButtonPos").transform, false);
        arrowRight.transform.SetParent(rightController.transform.Find("HandTriggerButtonPos").transform, false);
        arrowRight.SetActive(true);
    }

    public void Tutorial5Actions()
    {
        arrowLeft.transform.SetParent(leftController.transform.Find("StartButtonPos").transform, false);
        arrowRight.SetActive(false);
        arrowLeft.SetActive(true);
    }

    public void Tutorial6Actions()
    {
        arrowRight.SetActive(true);
        arrowLeft.transform.SetParent(leftController.transform.Find("AnalogStickPos").transform, false);
        sparksRising.SetActive(true);
    }

    public void Tutorial7Actions()
    {
        sparksRising.SetActive(false);
        arrowRight.SetActive(false);
        arrowLeft.SetActive(false);
        dialoguePoint.gameObject.SetActive(true);
    }

    public void MainMenuActions()
    {
        if (dialoguePoint.IsPlayerIn)
        {
            tutorial7Panel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    public void StartTransformation()
    {
        mainMenuPanel.SetActive(false);
        menuCanvas.SetActive(false);
        hudController.DisableUIHelpers();
        tutorialFinished = true;
        basiAnimator.enabled = true;
        basiAudioSource.enabled = true;
        screenVideoPlayer.Stop();
        screenMaterial.color = Color.black;
        leftController.SetActive(false);
        rightController.SetActive(false);
        StartCoroutine(TuxiAppearing());
    }

    IEnumerator TuxiAppearing()
    {
        yield return new WaitForSeconds(18f);
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        tuxiPrefab.SetActive(true);
        cameraFade.FadeIn();
        Destroy(archBase.GetComponent<CapsuleCollider>());
        tuxiController = tuxiPrefab.GetComponent<TuxiController>();
        dialogueController = tuxiPrefab.GetComponent<DialogueController>();
        tuxiController.DisableGrabHand();
        yield return new WaitForSeconds(dialogueController.PlayDialogue("1_1_1"));
        menuCanvas.SetActive(true);
        romanMap.SetActive(true);
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            romanMapVideoPlayer.clip = romanMapVideoEN;
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            romanMapVideoPlayer.clip = romanMapVideoGR;
        }
        romanMapVideoPlayer.Prepare();
        StartCoroutine(TuxiSecondDialogue());
    }

    IEnumerator TuxiSecondDialogue()
    {
        romanMapVideoPlayer.Play();
        StartCoroutine(RawImageAlphaFade(romanMapRawImage, 1f, 1f));
        yield return new WaitForSeconds(dialogueController.PlayDialogue("1_1_2"));
        StartCoroutine(TuxiThirdDialogue());
    }

    IEnumerator TuxiThirdDialogue()
    {
        romanMap.SetActive(false);
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            thessalonikiMapPanel.sprite = thessalonikiMapEN;
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            thessalonikiMapPanel.sprite = thessalonikiMapGR;
        }
        thessalonikiMapPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("1_1_3"));
        StartCoroutine(TuxiFourthDialogue());
    }

    IEnumerator TuxiFourthDialogue()
    {
        thessalonikiMapPanel.gameObject.SetActive(false);
        menuCanvas.SetActive(false);
        StartCoroutine(HightlightItemsGreek());
        yield return new WaitForSeconds(dialogueController.PlayDialogue("1_1_4"));
        StartCoroutine(TuxiFifthDialogue());
    }

    IEnumerator TuxiFifthDialogue()
    {
        highlightEpikrana.SetActive(false);
        highlightTokso.SetActive(false);
        highlightMarmara.SetActive(false);
        menuCanvas.SetActive(true);
        egnatiaPanel.SetActive(true);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("1_1_5"));
        tuxiController.EnableGrabHand();
        dialoguePoint.DestroyDialoguePoint();
    }

    IEnumerator RawImageAlphaFade(RawImage rawImage, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = rawImage.color.a;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, newAlpha);
            yield return null;
        }
    }

    IEnumerator HightlightItemsGreek()
    {
        highlightEpikrana.SetActive(true);
        yield return new WaitForSeconds(10);
        highlightTokso.SetActive(true);
        yield return new WaitForSeconds(3);
        highlightMarmara.SetActive(true);
    }
}
