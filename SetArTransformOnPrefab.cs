using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class SetArTransformOnPrefab : MonoBehaviour
{
    public GameObject scanImageText;

    public SaveAndLoadTranformForAr saveAndLoadTranformForAr;

    public string currentlyDetectedImage;

    public bool imageDetected = false;

    public ARTrackedImage detectedImage;

    public bool doOnce = false;

    private MutableRuntimeReferenceImageLibrary mutableLibrary;
    void Update()
    {
        ARTrackedImageManager _aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (var trackedImage in _aRTrackedImageManager.trackables)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                SaveTranformFromQr saveTranformFromQr = saveAndLoadTranformForAr.getSavedTransformForPrefab(trackedImage.referenceImage.name);
                if (saveTranformFromQr != null) {
                    if (GameObject.FindGameObjectWithTag("Monument") != null)
                    {
                        if(imageDetected && doOnce)
                        {
                            if(currentlyDetectedImage != trackedImage.referenceImage.name)
                            {
                                GameObject monument = GameObject.FindGameObjectWithTag("Monument");
                                monument.transform.GetChild(0).transform.GetChild(0).transform.localPosition += saveTranformFromQr.position;
                                monument.transform.GetChild(0).transform.GetChild(0).transform.localEulerAngles =
                                    new Vector3(
                                            monument.transform.GetChild(0).transform.GetChild(0).transform.localEulerAngles.x + saveTranformFromQr.rotation.x,
                                            monument.transform.GetChild(0).transform.GetChild(0).transform.localEulerAngles.y + saveTranformFromQr.rotation.y,
                                            monument.transform.GetChild(0).transform.GetChild(0).transform.localEulerAngles.z + saveTranformFromQr.rotation.z
                                    );
                                    currentlyDetectedImage = trackedImage.referenceImage.name;
                                    _aRTrackedImageManager.enabled = false;
                                    scanImageText.SetActive(false);
                            }
                        }
                        else{
                            detectedImage = trackedImage;
                            imageDetected=true;
                        }
                    }
                }
            }
        }

        if(imageDetected && detectedImage != null)
        {
            if(!doOnce)
            {
                _aRTrackedImageManager.referenceLibrary = Instantiate(GetReferenceImageLibraryPath(detectedImage.referenceImage.name)) as XRReferenceImageLibrary;
                _aRTrackedImageManager.requestedMaxNumberOfMovingImages = 1;
                _aRTrackedImageManager.trackedImagePrefab = ApplicationManager.monumentPrefab;
                _aRTrackedImageManager.enabled = true;
                doOnce=true;
            }     
        }
    }

    XRReferenceImageLibrary GetReferenceImageLibraryPath(string imageName)
    {
        switch(imageName)
        {
           case "e-skapani-QRcode-museum" :{
               return Resources.Load("Ar Image Library/Museum/MuseumReferenceImageLibrary") as XRReferenceImageLibrary;
            } 
           case "e-skapani_qr_1" :{
               return Resources.Load("Ar Image Library/Walk/walkReferenceImageLibrary 1") as XRReferenceImageLibrary;
            } 
           case "e-skapani_qr_2" :{
               return Resources.Load("Ar Image Library/Walk/walkReferenceImageLibrary 2") as XRReferenceImageLibrary;
            } 
           case "e-skapani_qr_3" :{
               return Resources.Load("Ar Image Library/Walk/walkReferenceImageLibrary 3") as XRReferenceImageLibrary;
            } 
           default :{
               return  Resources.Load("Ar Image Library/Walk/walkReferenceImageLibrary") as XRReferenceImageLibrary;
           } 
        }
    }

}
