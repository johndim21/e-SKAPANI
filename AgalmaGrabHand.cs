using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgalmaGrabHand : MonoBehaviour
{
    [SerializeField] private GameObject leftPlayerHand;
    [SerializeField] private GameObject rightPlayerHand;
    [SerializeField] GameObject grabHandCanvas;

    private BoxCollider leftHandCollider;

    private bool isHandInCollider;

    public bool IsHandInCollider { get => isHandInCollider; set => isHandInCollider = value; }

    private void Awake()
    {
        leftHandCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isHandInCollider = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, leftPlayerHand) || GameObject.ReferenceEquals(other.gameObject, rightPlayerHand))
        {
            isHandInCollider = true;
        }
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
