using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguageButton : MonoBehaviour
{
    [SerializeField] private List<TextLocalizerUI> textLocalizers;

    public void EnglishButton()
    {
        foreach(TextLocalizerUI textLocalizer in textLocalizers)
        {
            textLocalizer.EnglishButton();
        }
    }

    public void GreekButton()
    {
        foreach (TextLocalizerUI textLocalizer in textLocalizers)
        {
            textLocalizer.GreekButton();
        }
    }
}
