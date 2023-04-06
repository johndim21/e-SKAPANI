using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;

    void Start ()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        visual.SetActive(false);
    }
    
    void Update ()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane surface, update the position and rotation
        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            

            switch(ApplicationManager.Selected_Monument)
            {
                case "Καμάρα":
                    {
                        transform.eulerAngles = new Vector3(0f,hits[0].pose.rotation.eulerAngles.y + 270f,0f);
                        break;
                    }
                case "Ιππόδρομος":
                    {
                        transform.eulerAngles = new Vector3(0f,hits[0].pose.rotation.eulerAngles.y,0f);
                        break;
                    }
                case "Οκτάγωνο":
                    {
                        transform.eulerAngles = new Vector3(0f,hits[0].pose.rotation.eulerAngles.y,0f);
                        break;
                    }
                case "Βασιλική":
                    {
                        transform.eulerAngles = new Vector3(0f,hits[0].pose.rotation.eulerAngles.y,0f);
                        break;
                    }
            }

            // enable the visual if it's disabled
            if(!visual.activeInHierarchy)
                visual.SetActive(true);
        }
    }
}