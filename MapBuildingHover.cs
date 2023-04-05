using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuildingHover : MonoBehaviour
{
    [SerializeField] private GameObject buildingName;
    
    public void ShowBuildingName()
    {
        buildingName.SetActive(true);
    }

    public void HideBuildingName()
    {
        buildingName.SetActive(false);
    }
}
