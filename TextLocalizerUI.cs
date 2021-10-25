using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]

public class TextLocalizerUI : MonoBehaviour
{
    TextMeshProUGUI textField;

    public string key;

    void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.GetLocalizedValue(key);
        textField.text = value;
    }

    public void EnglishButton()
    {
        LocalizationSystem.SetEnglish();
        string value = LocalizationSystem.GetLocalizedValue(key);
        textField.text = value;
    }

    public void GreekButton()
    {
        LocalizationSystem.SetGreek();
        string value = LocalizationSystem.GetLocalizedValue(key);
        textField.text = value;
    }
}
