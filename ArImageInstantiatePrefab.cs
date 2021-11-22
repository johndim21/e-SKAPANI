using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ArImageInstantiatePrefab : MonoBehaviour
{
    public GameObject prefab;

    private GameObject spawnedPrefab;

    private ARTrackedImageManager _aRTrackedImageManager;

    void Start()
    {
        _aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        text = FindObjectOfType<TextMeshProUGUI>();
        StartCoroutine(setPrefabsTransformAfterTimeWithOutRotation(1));
    }

    IEnumerator setPrefabsTransformAfterTimeWithOutRotation(float time)
    {
        yield return new WaitForSeconds(time);
        
        spawnedPrefab = Instantiate(prefab,this.transform.position , Quaternion.identity) as GameObject;

        spawnedPrefab.transform.parent = this.transform;

        spawnedPrefab.transform.localPosition += new Vector3(0.5f,-0.1f,0.1f);
        
        var lookPos = this.transform.position - spawnedPrefab.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        spawnedPrefab.transform.rotation = rotation;

        Destroy(_aRTrackedImageManager);
    }
}
