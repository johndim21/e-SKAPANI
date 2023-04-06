using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefabRotationForAr : MonoBehaviour
{
    void Update()
    {
        GameObject monument = GameObject.FindGameObjectWithTag("Monument");
        if (monument != null)
        {
            monument.transform.eulerAngles = new Vector3(0f, monument.transform.eulerAngles.y, 0f);
        }
    }
}
