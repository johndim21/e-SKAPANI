using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] private OVRScreenFade cameraFade;
    [SerializeField] private GameObject oktagonoBtn;
    [SerializeField] private GameObject vasilikiBtn;
    [SerializeField] private GameObject ippodromosBtn;
    [SerializeField] private GameObject oktagonoHighlighted;
    [SerializeField] private GameObject vasilikiHighlighted;
    [SerializeField] private GameObject ippodromosHighlighted;
    [SerializeField] private Image backgroundMap;
    [SerializeField] private Image kamaraText;
    [SerializeField] private Image kamaraHereText;
    [SerializeField] private Image oktagonoText;
    [SerializeField] private Image oktagonoHereText;
    [SerializeField] private Image vasilikiText;
    [SerializeField] private Image vasilikiHereText;
    [SerializeField] private Image ippodromosText;
    [SerializeField] private Image ippodromosHereText;
    [SerializeField] private Image representationText;
    [SerializeField] private Sprite imageBackgroundEN;
    [SerializeField] private Sprite imageKamaraText;
    [SerializeField] private Sprite imageKamaraHereText;
    [SerializeField] private Sprite imageOktagonoText;
    [SerializeField] private Sprite imageOktagonoHereText;
    [SerializeField] private Sprite imageVasilikiText;
    [SerializeField] private Sprite imageVasilikiHereText;
    [SerializeField] private Sprite imageIppodromosText;
    [SerializeField] private Sprite imageIppodromosHereText;

    private void Awake()
    {
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            backgroundMap.sprite = imageBackgroundEN;
            kamaraText.sprite = imageKamaraText;
            kamaraHereText.sprite = imageKamaraHereText;
            oktagonoText.sprite = imageOktagonoText;
            oktagonoHereText.sprite = imageOktagonoHereText;
            vasilikiText.sprite = imageVasilikiText;
            vasilikiHereText.sprite = imageVasilikiHereText;
            ippodromosText.sprite = imageIppodromosText;
            ippodromosHereText.sprite = imageIppodromosHereText;
            representationText.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        if(!SceneManager.GetActiveScene().name.Equals("Oktagono") && OktagonoLevelManager.isOktagonoVisited)
        {
            oktagonoBtn.SetActive(false);
            oktagonoHighlighted.SetActive(true);
        }

        if (!SceneManager.GetActiveScene().name.Equals("Vasiliki") && VasilikiLevelManager.isVasilikiVisited)
        {
            vasilikiBtn.SetActive(false);
            vasilikiHighlighted.SetActive(true);
        }

        if (!SceneManager.GetActiveScene().name.Equals("Ippodromos") && IppodromosLevelManager.isIppodromosVisited)
        {
            ippodromosBtn.SetActive(false);
            ippodromosHighlighted.SetActive(true);
        }
    }

    public void LoadOctagonScene()
    {
        StartCoroutine(LoadAsyncScene("Oktagono"));
    }

    public void LoadVasilikiScene()
    {
        StartCoroutine(LoadAsyncScene("Vasiliki"));
    }

    public void LoadIppodromosScene()
    {
        StartCoroutine(LoadAsyncScene("Ippodromos"));
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
