using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private List<Dialogue> dialogues;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        //{
        //    audioSource.clip = GetDialogueEN("1_1_1");
        //}
        //else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        //{
        //    audioSource.clip = GetDialogueGR("1_1_1");
        //}

        //audioSource.clip = GetDialogueGR("1_1_2");
        //audioSource.Play();
    }

    public AudioClip GetDialogueGR(string dialogueName)
    {
        foreach(Dialogue dialogue in dialogues)
        {
            if (dialogue.name.Equals(dialogueName))
            {
                return dialogue.dialogueGR;
            }
        }

        return null;
    }

    public AudioClip GetDialogueEN(string dialogueName)
    {
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.name.Equals(dialogueName))
            {
                return dialogue.dialogueEN;
            }
        }

        return null;
    }

    public float PlayDialogue(string dialogueName)
    {
        if (LocalizationSystem.GetLanguage().ToString().Equals("English"))
        {
            audioSource.clip = GetDialogueEN(dialogueName);
        }
        else if (LocalizationSystem.GetLanguage().ToString().Equals("Greek"))
        {
            audioSource.clip = GetDialogueGR(dialogueName);
        }
        audioSource.Play();
        return audioSource.clip.length;
    }
}
