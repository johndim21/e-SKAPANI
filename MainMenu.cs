using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            HUDCanvas.SetActive(!HUDCanvas.activeSelf);
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
    public void Play()
    {
        Level1();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene("Museum");
    }

    public void Level2()
    {
        SceneManager.LoadScene("ReachingCity");
    }

    public void Level3()
    {
        SceneManager.LoadScene("InsideWagon");
    }

    public void Level4()
    {
        SceneManager.LoadScene("KamaraNoEdit");
    }
}
