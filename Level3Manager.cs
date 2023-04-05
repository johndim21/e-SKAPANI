using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    [SerializeField] private OVRGrabbable nomisma1Prefab;
    [SerializeField] private OVRGrabbable nomisma2Prefab;
    [SerializeField] private GameObject eutuxis;
    [SerializeField] private DialoguePoint dialoguePoint;
    [SerializeField] private GameObject sparksRising1;
    [SerializeField] private GameObject sparksRising2;
    [SerializeField] private OVRScreenFade cameraFade;

    private GameObject player;
    private OVRGrabbable nomisma1ToDestroy;
    private OVRGrabbable nomisma2ToDestroy;
    private HUDController hudController;
    private DialogueController dialogueController;
    private bool nomisma1Grabbed;
    private bool nomisma2Grabbed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        nomisma1ToDestroy = Instantiate(nomisma1Prefab);
        nomisma2ToDestroy = Instantiate(nomisma2Prefab);
        nomisma1Grabbed = false;
        nomisma2Grabbed = false;

        dialogueController = eutuxis.GetComponent<DialogueController>();
        StartCoroutine(EutixisFirstDialogue());
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
            hudController.ToggleUIHelpers();
        }

        if(nomisma1Grabbed && nomisma2Grabbed && !nomisma1ToDestroy.isGrabbed && !nomisma2ToDestroy.isGrabbed)
        {
            nomisma1Grabbed = nomisma2Grabbed = false;
            StartCoroutine(LoadAsyncScene("KamaraEdit"));
        }
    }

    void FixedUpdate()
    {
        if(nomisma1ToDestroy.transform.position.y < -1f)
        {
            Destroy(nomisma1ToDestroy.gameObject);
            nomisma1ToDestroy = Instantiate(nomisma1Prefab);
        }
        if (nomisma2ToDestroy.transform.position.y < -1f)
        {
            Destroy(nomisma2ToDestroy.gameObject);
            nomisma2ToDestroy = Instantiate(nomisma2Prefab);
        }

        if (nomisma1ToDestroy.isGrabbed)
        {
            nomisma1Grabbed = true;
            if (sparksRising1.activeInHierarchy)
            {
                sparksRising1.SetActive(false);
            }
        }

        if (nomisma2ToDestroy.isGrabbed)
        {
            nomisma2Grabbed = true;
            if (sparksRising2.activeInHierarchy)
            {
                sparksRising2.SetActive(false);
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

    IEnumerator EutixisFirstDialogue()
    {
        yield return new WaitForSeconds(dialogueController.PlayDialogue("3_1_1"));
        StartCoroutine(EutixisSecondDialogue());
    }

    IEnumerator EutixisSecondDialogue()
    {
        sparksRising1.SetActive(true);
        sparksRising2.SetActive(true);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("3_1_2"));
        dialoguePoint.DestroyDialoguePoint();
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
