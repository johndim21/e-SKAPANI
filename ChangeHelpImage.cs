using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHelpImage : MonoBehaviour
{
    public Sprite controlsGR;
    public Sprite controlsEN;

    private Image controlsImage;

    // Start is called before the first frame update
    void Awake()
    {
        controlsImage = GetComponent<Image>();
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            controlsImage.sprite = controlsEN;
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            controlsImage.sprite = controlsGR;
        }
    }

    public void SetControlsImageEN()
    {
        controlsImage.sprite = controlsEN;
    }

    public void SetControlsImageGR()
    {
        controlsImage.sprite = controlsGR;
    }
}
