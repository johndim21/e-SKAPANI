using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAmaksa : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas;

    private HUDController hudController;
    private bool isPlayerNearDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        hudController = player.GetComponent<HUDController>();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearDoor)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                SceneManager.LoadScene("KamaraNoEdit");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //hudController.EnableHUDCanvas();
            //hudController.EnableMessagePromptPanel();
            //hudController.SetMessagePromptText("Press A to enter");
            canvas.SetActive(true);
            isPlayerNearDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hudController.DisableHUDCanvas();
            //hudController.DisableMessagePromptPanel();
            //hudController.SetMessagePromptText("");
            canvas.SetActive(false);
            isPlayerNearDoor = false;
        }
    }
}
