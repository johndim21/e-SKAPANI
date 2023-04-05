using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAmaksaRopes : MonoBehaviour
{

    [SerializeField] private Transform rightHandStart;
    [SerializeField] private Transform leftHandStart;
    [SerializeField] private Transform horse1RopeEnd;
    [SerializeField] private Transform horse2RopeEnd;
    [SerializeField] private Transform horse3RopeEnd;
    [SerializeField] private Transform horse4RopeEnd;
    [SerializeField] private Material ropeMaterial;

    private GameObject horse1Rope;
    private GameObject horse2Rope;
    private GameObject horse3Rope;
    private GameObject horse4Rope;

    // Start is called before the first frame update
    void Awake()
    {
        horse1Rope = new GameObject();
        horse1Rope.transform.position = rightHandStart.position;
        horse1Rope.AddComponent<LineRenderer>();

        horse2Rope = new GameObject();
        horse2Rope.transform.position = rightHandStart.position;
        horse2Rope.AddComponent<LineRenderer>();

        horse3Rope = new GameObject();
        horse3Rope.transform.position = leftHandStart.position;
        horse3Rope.AddComponent<LineRenderer>();

        horse4Rope = new GameObject();
        horse4Rope.transform.position = leftHandStart.position;
        horse4Rope.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawRope(horse1Rope, rightHandStart.position, horse1RopeEnd.position);
        DrawRope(horse2Rope, rightHandStart.position, horse2RopeEnd.position);
        DrawRope(horse3Rope, leftHandStart.position, horse3RopeEnd.position);
        DrawRope(horse4Rope, leftHandStart.position, horse4RopeEnd.position);
    }

    private void DrawRope(GameObject rope, Vector3 start, Vector3 end)
    {
        LineRenderer lr = rope.GetComponent<LineRenderer>();
        lr.material = ropeMaterial;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
