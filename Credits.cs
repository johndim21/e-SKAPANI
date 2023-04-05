using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject creditsEN;
    [SerializeField] private GameObject creditsGR;
    // Start is called before the first frame update
    private void Awake()
    {
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            creditsEN.SetActive(true);
            creditsGR.SetActive(false);
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            creditsEN.SetActive(false);
            creditsGR.SetActive(true);
        }
    }
}
