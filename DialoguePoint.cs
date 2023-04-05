using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePoint : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private OVRPlayerController playerController;
    private bool isPlayerIn;
    private GameObject sparksRising;

    public bool IsPlayerIn { get => isPlayerIn; set => isPlayerIn = value; }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sparksRising = transform.GetChild(0).gameObject;
        IsPlayerIn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerController = other.gameObject.GetComponent<OVRPlayerController>();
            playerController.EnableLinearMovement = false;
            meshRenderer.enabled = false;
            sparksRising.SetActive(false);
            IsPlayerIn = true;
        }
    }

    public void DestroyDialoguePoint()
    {
        Destroy(this.GetComponent<CapsuleCollider>());
        IsPlayerIn = false;
        playerController.EnableLinearMovement = true;
        Destroy(this.gameObject);
    }

    public void DisableDialoguePoint()
    {
        this.gameObject.SetActive(false);
        IsPlayerIn = false;
        playerController.EnableLinearMovement = true;
    }

    public void EnableDialoguePoint()
    {
        this.gameObject.SetActive(true);
        meshRenderer.enabled = true;
        sparksRising.SetActive(true);
    }
}
