using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutOfBounds : MonoBehaviour
{

    private GameObject player;
    private HUDController hudController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudController = player.GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hudController.EnableHUDCanvas();
            hudController.EnableMessagePromptPanel();
            hudController.SetMessagePromptText("Out of play area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hudController.DisableHUDCanvas();
            hudController.DisableMessagePromptPanel();
            hudController.SetMessagePromptText("");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hudController.IsMessagePromptPanelActive())
            {
                hudController.EnableHUDCanvas();
                hudController.EnableMessagePromptPanel();
                hudController.SetMessagePromptText("Out of play area");
            }
        }
    }
}
