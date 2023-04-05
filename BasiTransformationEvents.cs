using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiTransformationEvents : MonoBehaviour
{
    [SerializeField] private GameObject leftMarble;
    [SerializeField] private GameObject rightMarble;
    [SerializeField] private Material opaqueMarble8;

    public void ChangeMaterial()
    {
        leftMarble.GetComponent<MeshRenderer>().material = opaqueMarble8;
        rightMarble.GetComponent<MeshRenderer>().material = opaqueMarble8;
    }
}
