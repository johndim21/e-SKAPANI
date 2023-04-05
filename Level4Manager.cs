using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : MonoBehaviour
{
    [SerializeField] private GameObject tuxiPrefab;
    [SerializeField] private DialoguePoint dialoguePoint1;
    [SerializeField] private DialoguePoint dialoguePoint2;
    [SerializeField] private GameObject rectangleHighlight1;
    [SerializeField] private GameObject rectangleHighlight2;
    [SerializeField] private GameObject rectangleHighlight3;
    [SerializeField] private GameObject rectangleHighlight4;
    [SerializeField] private GameObject galerianMap;
    [SerializeField] private GameObject anaglufo20GR;
    [SerializeField] private GameObject scene20GR;
    [SerializeField] private GameObject scene20DarkGR;
    [SerializeField] private GameObject galeriosGR;
    [SerializeField] private GameObject narsisGR;
    [SerializeField] private GameObject aetosGR;
    [SerializeField] private GameObject anaglufo20EN;
    [SerializeField] private GameObject scene20EN;
    [SerializeField] private GameObject scene20DarkEN;
    [SerializeField] private GameObject galeriosEN;
    [SerializeField] private GameObject narsisEN;
    [SerializeField] private GameObject aetosEN;
    [SerializeField] private GameObject anaglufo21GR;
    [SerializeField] private GameObject scene21GR;
    [SerializeField] private GameObject scene21DarkGR;
    [SerializeField] private GameObject dioklitianosGR;
    [SerializeField] private GameObject galerios21GR;
    [SerializeField] private GameObject konstantiosGR;
    [SerializeField] private GameObject maximianosGR;
    [SerializeField] private GameObject ouranosGR;
    [SerializeField] private GameObject anaglufo21EN;
    [SerializeField] private GameObject scene21EN;
    [SerializeField] private GameObject scene21DarkEN;
    [SerializeField] private GameObject dioklitianosEN;
    [SerializeField] private GameObject galerios21EN;
    [SerializeField] private GameObject konstantiosEN;
    [SerializeField] private GameObject maximianosEN;
    [SerializeField] private GameObject ouranosEN;
    [SerializeField] private GameObject anaglufo16GR;
    [SerializeField] private GameObject scene16GR;
    [SerializeField] private GameObject scene16DarkGR;
    [SerializeField] private GameObject galerios16GR;
    [SerializeField] private GameObject persesGR;
    [SerializeField] private GameObject persiaGR;
    [SerializeField] private GameObject rwmiGR;
    [SerializeField] private GameObject anaglufo16EN;
    [SerializeField] private GameObject scene16EN;
    [SerializeField] private GameObject scene16DarkEN;
    [SerializeField] private GameObject galerios16EN;
    [SerializeField] private GameObject persesEN;
    [SerializeField] private GameObject persiaEN;
    [SerializeField] private GameObject rwmiEN;
    [SerializeField] private GameObject anaglufo17GR;
    [SerializeField] private GameObject scene17GR;
    [SerializeField] private GameObject scene17DarkGR;
    [SerializeField] private GameObject aiwnasGR;
    [SerializeField] private GameObject dioklitianos17GR;
    [SerializeField] private GameObject eiriniGR;
    [SerializeField] private GameObject galerios17GR;
    [SerializeField] private GameObject oikoumeniGR;
    [SerializeField] private GameObject omonoiaGR;
    [SerializeField] private GameObject omonoiaXeriGR;
    [SerializeField] private GameObject thisiaGR;
    [SerializeField] private GameObject anaglufo17EN;
    [SerializeField] private GameObject scene17EN;
    [SerializeField] private GameObject scene17DarkEN;
    [SerializeField] private GameObject aiwnasEN;
    [SerializeField] private GameObject dioklitianos17EN;
    [SerializeField] private GameObject eiriniEN;
    [SerializeField] private GameObject galerios17EN;
    [SerializeField] private GameObject oikoumeniEN;
    [SerializeField] private GameObject omonoiaEN;
    [SerializeField] private GameObject omonoiaXeriEN;
    [SerializeField] private GameObject thisiaEN;

    private GameObject player;
    private HUDController hudController;
    private DialogueController dialogueController;
    private TuxiController tuxiController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
        InitHUD();

        dialogueController = tuxiPrefab.GetComponent<DialogueController>();
        tuxiController = tuxiPrefab.GetComponent<TuxiController>();

        tuxiController.DisableGrabHand();

        dialoguePoint2.gameObject.SetActive(false);
        rectangleHighlight1.SetActive(false);
        rectangleHighlight2.SetActive(false);
        rectangleHighlight3.SetActive(false);
        rectangleHighlight4.SetActive(false);

        StartCoroutine(TuxiFirstDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            hudController.ToggleHUDCanvas();
            hudController.TogglePauseMenu();
            if (!galerianMap.activeSelf)
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

    public void MainMenuResumeButton()
    {
        hudController.DisablePauseMenu();
        hudController.DisableHUDCanvas();
        if (!galerianMap.activeSelf)
        {
            hudController.DisableUIHelpers();
        }
    }

    public void CloseObjectivesButton()
    {
        hudController.DisableObjectivesMenu();
        hudController.DisableHUDCanvas();
        if (!galerianMap.activeSelf)
        {
            hudController.DisableUIHelpers();
        }
    }

    public void CloseSettingsButton()
    {
        hudController.DisableSettingsMenu();
        hudController.DisableHUDCanvas();
        if (!galerianMap.activeSelf)
        {
            hudController.DisableUIHelpers();
        }
    }

    IEnumerator TuxiFirstDialogue()
    {
        yield return new WaitUntil(() => dialoguePoint1.IsPlayerIn);
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            StartCoroutine(Anaglufo20EN());
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            StartCoroutine(Anaglufo20GR());
        }
        yield return new WaitForSeconds(dialogueController.PlayDialogue("4_1_1"));
        dialoguePoint1.DisableDialoguePoint();
        StartCoroutine(TuxiSecondDialogue());
    }

    IEnumerator TuxiSecondDialogue()
    {
        dialoguePoint2.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialoguePoint2.IsPlayerIn);
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            StartCoroutine(Anaglufo16EN());
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            StartCoroutine(Anaglufo16GR());
        }
        yield return new WaitForSeconds(dialogueController.PlayDialogue("4_1_2"));
        StartCoroutine(TuxiThirdDialogue());
    }

    IEnumerator TuxiThirdDialogue()
    {
        rectangleHighlight2.SetActive(false);
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            StartCoroutine(Anaglufo17EN());
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            StartCoroutine(Anaglufo17GR());
        }
        yield return new WaitForSeconds(dialogueController.PlayDialogue("4_1_3"));
        dialoguePoint2.DestroyDialoguePoint();
        StartCoroutine(TuxiFourthDialogue());
    }

    IEnumerator TuxiFourthDialogue()
    {
        rectangleHighlight3.SetActive(false);
        dialoguePoint1.EnableDialoguePoint();
        yield return new WaitUntil(() => dialoguePoint1.IsPlayerIn);
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            StartCoroutine(Anaglufo21EN());
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            StartCoroutine(Anaglufo21GR());
        }
        yield return new WaitForSeconds(dialogueController.PlayDialogue("4_1_4"));
        StartCoroutine(TuxiFifthDialogue());
    }

    IEnumerator TuxiFifthDialogue()
    {
        rectangleHighlight4.SetActive(false);
        yield return new WaitForSeconds(dialogueController.PlayDialogue("4_1_5"));
        dialoguePoint1.DestroyDialoguePoint();
        galerianMap.SetActive(true);
        hudController.EnableUIHelpers();
    }

    IEnumerator Anaglufo20GR()
    {
        yield return new WaitForSeconds(31);
        rectangleHighlight1.SetActive(true);
        anaglufo20GR.SetActive(true);
        yield return new WaitForSeconds(10.5f);
        galeriosGR.SetActive(true);
        scene20DarkGR.SetActive(true);
        scene20GR.SetActive(false);
        yield return new WaitForSeconds(1);
        narsisGR.SetActive(true);
        yield return new WaitForSeconds(5);
        aetosGR.SetActive(true);
        yield return new WaitForSeconds(20);
        rectangleHighlight1.SetActive(false);
        anaglufo20GR.SetActive(false);
    }

    IEnumerator Anaglufo20EN()
    {
        yield return new WaitForSeconds(33);
        rectangleHighlight1.SetActive(true);
        anaglufo20EN.SetActive(true);
        yield return new WaitForSeconds(11f);
        galeriosEN.SetActive(true);
        scene20DarkEN.SetActive(true);
        scene20EN.SetActive(false);
        yield return new WaitForSeconds(1);
        narsisEN.SetActive(true);
        yield return new WaitForSeconds(3);
        aetosEN.SetActive(true);
        yield return new WaitForSeconds(22);
        rectangleHighlight1.SetActive(false);
        anaglufo20EN.SetActive(false);
    }

    IEnumerator Anaglufo16GR()
    {
        rectangleHighlight2.SetActive(true);
        anaglufo16GR.SetActive(true);
        yield return new WaitForSeconds(2);
        persesGR.SetActive(true);
        scene16DarkGR.SetActive(true);
        scene16GR.SetActive(false);
        yield return new WaitForSeconds(3);
        galerios16GR.SetActive(true);
        yield return new WaitForSeconds(7);
        persiaGR.SetActive(true);
        yield return new WaitForSeconds(6);
        rwmiGR.SetActive(true);
        yield return new WaitForSeconds(4);
        rectangleHighlight2.SetActive(false);
        anaglufo16GR.SetActive(false);
    }

    IEnumerator Anaglufo16EN()
    {
        rectangleHighlight2.SetActive(true);
        anaglufo16EN.SetActive(true);
        yield return new WaitForSeconds(2);
        persesEN.SetActive(true);
        scene16DarkEN.SetActive(true);
        scene16EN.SetActive(false);
        yield return new WaitForSeconds(2);
        galerios16EN.SetActive(true);
        yield return new WaitForSeconds(10);
        persiaEN.SetActive(true);
        yield return new WaitForSeconds(5);
        rwmiEN.SetActive(true);
        yield return new WaitForSeconds(6);
        rectangleHighlight2.SetActive(false);
        anaglufo16EN.SetActive(false);
    }

    IEnumerator Anaglufo17GR()
    {
        rectangleHighlight3.SetActive(true);
        anaglufo17GR.SetActive(true);
        yield return new WaitForSeconds(1);
        dioklitianos17GR.SetActive(true);
        scene17DarkGR.SetActive(true);
        scene17GR.SetActive(false);
        yield return new WaitForSeconds(1);
        galerios17GR.SetActive(true);
        yield return new WaitForSeconds(2);
        thisiaGR.SetActive(true);
        yield return new WaitForSeconds(7);
        oikoumeniGR.SetActive(true);
        yield return new WaitForSeconds(1);
        omonoiaGR.SetActive(true);
        yield return new WaitForSeconds(1);
        eiriniGR.SetActive(true);
        yield return new WaitForSeconds(1);
        aiwnasGR.SetActive(true);
        yield return new WaitForSeconds(6);
        rectangleHighlight3.SetActive(false);
        anaglufo17GR.SetActive(false);
    }

    IEnumerator Anaglufo17EN()
    {
        rectangleHighlight3.SetActive(true);
        anaglufo17EN.SetActive(true);
        yield return new WaitForSeconds(1);
        dioklitianos17EN.SetActive(true);
        scene17DarkEN.SetActive(true);
        scene17EN.SetActive(false);
        yield return new WaitForSeconds(1);
        galerios17EN.SetActive(true);
        yield return new WaitForSeconds(2);
        thisiaEN.SetActive(true);
        yield return new WaitForSeconds(13);
        oikoumeniEN.SetActive(true);
        yield return new WaitForSeconds(1);
        omonoiaEN.SetActive(true);
        yield return new WaitForSeconds(1);
        eiriniEN.SetActive(true);
        yield return new WaitForSeconds(1);
        aiwnasEN.SetActive(true);
        yield return new WaitForSeconds(3);
        rectangleHighlight3.SetActive(false);
        anaglufo17EN.SetActive(false);
    }

    IEnumerator Anaglufo21GR()
    {
        rectangleHighlight4.SetActive(true);
        anaglufo21GR.SetActive(true);
        yield return new WaitForSeconds(3);
        dioklitianosGR.SetActive(true);
        scene21DarkGR.SetActive(true);
        scene21GR.SetActive(false);
        yield return new WaitForSeconds(1);
        maximianosGR.SetActive(true);
        yield return new WaitForSeconds(4);
        ouranosGR.SetActive(true);
        yield return new WaitForSeconds(4);
        galerios21GR.SetActive(true);
        yield return new WaitForSeconds(1);
        konstantiosGR.SetActive(true);
        yield return new WaitForSeconds(19);
        rectangleHighlight4.SetActive(false);
        anaglufo21GR.SetActive(false);
    }

    IEnumerator Anaglufo21EN()
    {
        rectangleHighlight4.SetActive(true);
        anaglufo21EN.SetActive(true);
        yield return new WaitForSeconds(3);
        dioklitianosEN.SetActive(true);
        scene21DarkEN.SetActive(true);
        scene21EN.SetActive(false);
        yield return new WaitForSeconds(1);
        maximianosEN.SetActive(true);
        yield return new WaitForSeconds(4);
        ouranosEN.SetActive(true);
        yield return new WaitForSeconds(7);
        galerios21EN.SetActive(true);
        yield return new WaitForSeconds(1);
        konstantiosEN.SetActive(true);
        yield return new WaitForSeconds(18);
        rectangleHighlight4.SetActive(false);
        anaglufo21EN.SetActive(false);
    }
}
