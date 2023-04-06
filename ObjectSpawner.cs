using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private PlacementIndicator placementIndicator;
    private ToggleArPlanes toggleArPlanes;
    Rect screenWithOutToolBar = new Rect(0, 0, Screen.width, Screen.height-200);

    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        toggleArPlanes = FindObjectOfType<ToggleArPlanes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && transform.GetChild(0).gameObject.active)
        {
            var touchPos = Input.GetTouch(0).position;
            print (touchPos);
            if (screenWithOutToolBar.Contains(touchPos))
            {
                Debug.Log("topLeft touched");
                GameObject monument  = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
                
                monument.transform.GetChild(0).transform.GetChild(0).transform.localPosition -= monument.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).localPosition;

                monument.transform.GetChild(0).transform.localRotation = Quaternion.Inverse(monument.transform.GetChild(0).transform.localRotation * monument.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).localRotation);

                this.gameObject.active = false;
                toggleArPlanes.TogglePlanes(false);
            }
        }
    }
}
