using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OktagonoMarble : MonoBehaviour
{
    [SerializeField] DialoguePoint dialoguePoint;

    private bool isMarbleSeen;

    public bool IsMarbleSeen { get => isMarbleSeen; set => isMarbleSeen = value; }

    // Start is called before the first frame update
    void Start()
    {
        isMarbleSeen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialoguePoint.IsPlayerIn)
        {
            isMarbleSeen = true;
            dialoguePoint.DestroyDialoguePoint();
            this.gameObject.SetActive(false);
        }
    }
}
