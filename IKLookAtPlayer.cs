using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKLookAtPlayer : MonoBehaviour
{
    protected Animator animator;

    [SerializeField] private bool ikActive = false;
    [SerializeField] private Transform lookObj = null;
    [SerializeField] private float lookWeight;
    [SerializeField] private float maxLookWeight;

    //dummy pivot
    GameObject objPivot;

    void Start()
    {
        animator = GetComponent<Animator>();

        objPivot = new GameObject("DummyPivot");
        objPivot.transform.parent = transform;
        objPivot.transform.localPosition = new Vector3(0, 1.7f, 0);
        objPivot.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        // target position
        objPivot.transform.LookAt(lookObj);
        float pivotRotY = objPivot.transform.localRotation.y;

        // target distance
        float dist = Vector3.Distance(objPivot.transform.position, lookObj.position);
        
        if(pivotRotY < 0.65f && pivotRotY > -0.65f && dist < 4.5f)
        {
            // Target tracking
            lookWeight = Mathf.Lerp(lookWeight, maxLookWeight, Time.deltaTime * 2.5f);
        }
        else
        {
            // Target release
            lookWeight = Mathf.Lerp(lookWeight, 0, Time.deltaTime * 2.5f);
        }
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(lookWeight);
                    animator.SetLookAtPosition(lookObj.position);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
