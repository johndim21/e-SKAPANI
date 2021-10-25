using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAmaksaRopes : MonoBehaviour
{

    [SerializeField] private Transform leftBigRopeStart;
    [SerializeField] private Transform leftBigRopeEnd;
    [SerializeField] private Transform leftSmallRopeStart;
    [SerializeField] private Transform leftSmallRopeEnd;
    [SerializeField] private Transform rightBigRopeStart;
    [SerializeField] private Transform rightBigRopeEnd;
    [SerializeField] private Transform rightSmallRopeStart;
    [SerializeField] private Transform rightSmallRopeEnd;
    [SerializeField] private Material ropeMaterial;

    private GameObject leftRopeToDriver;
    private GameObject leftRopeToAmaksa;
    private GameObject rightRopeToDriver;
    private GameObject rightRopeToAmaksa;

    // Start is called before the first frame update
    void Awake()
    {
        leftRopeToDriver = new GameObject();
        leftRopeToDriver.transform.position = leftBigRopeStart.position;
        leftRopeToDriver.AddComponent<LineRenderer>();

        leftRopeToAmaksa = new GameObject();
        leftRopeToAmaksa.transform.position = leftSmallRopeStart.position;
        leftRopeToAmaksa.AddComponent<LineRenderer>();

        rightRopeToDriver = new GameObject();
        rightRopeToDriver.transform.position = rightBigRopeStart.position;
        rightRopeToDriver.AddComponent<LineRenderer>();

        rightRopeToAmaksa = new GameObject();
        rightRopeToAmaksa.transform.position = rightSmallRopeStart.position;
        rightRopeToAmaksa.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawRope(leftRopeToDriver, leftBigRopeStart.position, leftBigRopeEnd.position);
        DrawRope(leftRopeToAmaksa, leftSmallRopeStart.position, leftSmallRopeEnd.position);
        DrawRope(rightRopeToDriver, rightBigRopeStart.position, rightBigRopeEnd.position);
        DrawRope(rightRopeToAmaksa, rightSmallRopeStart.position, rightSmallRopeEnd.position);
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
