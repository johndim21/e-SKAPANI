using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private GameObject flyerPrefab;
    [SerializeField] private GameObject basiMikroTokso;
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject tuxi;

    private GameObject player;
    private HUDController hudController;
    private GameObject flyerToDestroy;
    private AudioSource basiAudioSource;
    private Animator basiAnimator;
    private VideoPlayer screenVideoPlayer;
    private Material screenMaterial;
    private OVRScreenFade cameraFade;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        basiAnimator = basiMikroTokso.GetComponent<Animator>();
        basiAudioSource = basiMikroTokso.GetComponent<AudioSource>();

        screenVideoPlayer = screen.GetComponent<VideoPlayer>();
        screenMaterial = screen.GetComponent<Renderer>().material;

        cameraFade = player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();

        flyerToDestroy = Instantiate(flyerPrefab);

        flyerToDestroy.transform.Find("MagnifierPos").gameObject.SetActive(false);

        Tutorial1Actions();

    }

    // Update is called once per frame
    void Update()
    {
        if (flyerToDestroy.transform.position.y < -2.5)
        {
            Destroy(flyerToDestroy);
            flyerToDestroy = Instantiate(flyerPrefab);
            flyerToDestroy.transform.Find("MagnifierPos").gameObject.SetActive(false);
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
            hudController.ToggleUICollider();
            if (!menuCanvas.activeSelf)
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
        hudController.EnableUIHelpers();
        hudController.DisableUICollider();
    }

    public void MainMenuResumeButton()
    {
        hudController.DisablePauseMenu();
        hudController.DisableHUDCanvas();
        hudController.DisableUICollider();
        if (!menuCanvas.activeSelf)
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
        hudController.DisableUICollider();
        if (!menuCanvas.activeSelf)
        {
            hudController.DisableUIHelpers();
        }
    }

    public void Tutorial1Actions()
    {
        arrowRight.transform.position = rightController.transform.Find("AButtonPos").transform.position;
        arrowLeft.SetActive(false);
    }

    public void Tutorial2Actions()
    {
        arrowRight.SetActive(false);
        OVRPlugin.SetBoundaryVisible(true);
    }

    public void Tutorial3Actions()
    {
        OVRPlugin.SetBoundaryVisible(false);
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
        arrowLeft.transform.SetParent(leftController.transform.Find("AnalogStickPos").transform, false);
        flyerToDestroy.transform.Find("MagnifierPos").gameObject.SetActive(true);
    }

    public void Tutorial6Actions()
    {
        flyerToDestroy.transform.Find("MagnifierPos").gameObject.SetActive(false);
        arrowRight.SetActive(false);
        arrowLeft.transform.SetParent(leftController.transform.Find("StartButtonPos").transform, false);
    }

    public void MainMenuActions()
    {
        arrowLeft.SetActive(false);
    }

    public void StartTransformation()
    {
        menuCanvas.SetActive(false);
        hudController.DisableUIHelpers();
        basiAnimator.enabled = true;
        basiAudioSource.enabled = true;
        screenVideoPlayer.Stop();
        screenMaterial.color = Color.black;
        StartCoroutine(TuxiAppearing());
    }

    IEnumerator TuxiAppearing()
    {
        yield return new WaitForSeconds(18f);
        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        tuxi.SetActive(true);
        cameraFade.FadeIn();
        player.GetComponent<CharacterController>().stepOffset = 0.3f;
    }

    //IEnumerator LerpMoveStatue(Vector3 endValue, float duration)
    //{
    //    float time = 0;
    //    Vector3 startValue = tuxiStatue.transform.position;

    //    while (time < duration)
    //    {
    //        tuxiStatue.transform.position = Vector3.Lerp(startValue, endValue, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    tuxiStatue.transform.position = endValue;
    //}

    //IEnumerator TransformPillars()
    //{
    //    basiMikroTokso.SetActive(true);
    //    yield return new WaitForSeconds(7f);

    //    StartCoroutine(RemoveArchPillars(3));
    //    yield return new WaitForSeconds(19f);

    //    basiMikroTokso.transform.Find("MARBLE_frame").gameObject.SetActive(false);
    //    yield return new WaitForSeconds(20f);

    //    StartCoroutine(LerpMoveStatue(new Vector3(-0.804f, -2.465f, 1.053f), 10f));
    //    StartCoroutine(LerpChangeImageColor(Color.white, 10f));
    //    yield return new WaitForSeconds(10f);

    //    player.GetComponent<CharacterController>().stepOffset = 0.3f;
    //}

    //IEnumerator RemoveArchPillars(float duration)
    //{
    //    float time = 0;
    //    Vector3 leftStartValue = pillarLeft.transform.position;
    //    Vector3 rightStartValue = pillarRight.transform.position;
    //    Vector3 leftEndValue = new Vector3(pillarLeft.transform.position.x, -3.3f, pillarLeft.transform.position.z);
    //    Vector3 rightEndValue = new Vector3(pillarRight.transform.position.x, -3.3f, pillarRight.transform.position.z);

    //    while(time < duration)
    //    {
    //        pillarLeft.transform.position = Vector3.Lerp(leftStartValue, leftEndValue, time / duration);
    //        pillarRight.transform.position = Vector3.Lerp(rightStartValue, rightEndValue, time / duration);

    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    pillarLeft.transform.position = leftEndValue;
    //    pillarRight.transform.position = rightEndValue;
    //    Destroy(pillarLeft);
    //    Destroy(pillarRight);
    //}

    //IEnumerator LerpChangeImageColor(Color endValue, float duration)
    //{
    //    float time = 0;
    //    //Color startValue = egnatia.color;

    //    while (time < duration)
    //    {
    //        //egnatia.color = Color.Lerp(startValue, endValue, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    //egnatia.color = endValue;
    //}
}
