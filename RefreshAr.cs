using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;

public class RefreshAr : MonoBehaviour
{
    private GameObject[] anchors;
    public GameObject arSession;

    public GameObject anchor;
    public XRReferenceImageLibrary myLibrary;

    public void resetImageRecognition()
    {
        anchors = GameObject.FindGameObjectsWithTag("Anchor");

        foreach (GameObject anchor in anchors)
        {
            Destroy(anchor);
        }
        
        var manager = arSession.AddComponent<ARTrackedImageManager>();
        manager.referenceLibrary = myLibrary;
        manager.maxNumberOfMovingImages = 1;
        manager.trackedImagePrefab = anchor;
        manager.enabled = true;
    }
}
