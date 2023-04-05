using UnityEngine;

public class EnterAmaksa : MonoBehaviour
{
    [SerializeField] private DialoguePoint dialoguePoint3;
    [SerializeField] private HUDController hudController;

    // Update is called once per frame
    void Update()
    {
        if (dialoguePoint3.IsPlayerIn)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                hudController.LoadAmaksaLevel();
            }
        }
    }
}
