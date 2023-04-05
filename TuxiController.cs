using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuxiController : MonoBehaviour
{
    [SerializeField] GameObject grabHandCanvas;
    [SerializeField] GameObject leftHand;

    private BoxCollider leftHandCollider;

    private void Awake()
    {
        leftHandCollider = leftHand.GetComponent<BoxCollider>();
    }

    private void ShowGrabHandCanvas()
    {
        grabHandCanvas.SetActive(true);
    }

    private void HideGrabHandCanvas()
    {
        grabHandCanvas.SetActive(false);
    }

    private void EnableLeftHandCollider()
    {
        leftHandCollider.enabled = true;
    }

    private void DisableLeftHandCollider()
    {
        leftHandCollider.enabled = false;
    }

    public void EnableGrabHand()
    {
        EnableLeftHandCollider();
        ShowGrabHandCanvas();
    }

    public void DisableGrabHand()
    {
        DisableLeftHandCollider();
        HideGrabHandCanvas();
    }
}
