using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmaksaController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    [SerializeField] private float maxMotorTorque = 80f;
    [SerializeField] private float maxBrakeTorque = 150f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float brakingDistance = 100f;
    [SerializeField] private GameObject horse1;
    [SerializeField] private GameObject horse2;
    [SerializeField] private Vector3 centerOfMass;

    private Rigidbody rb;
    private bool isBraking = false;
    private Animator horse1Animator;
    private Animator horse2Animator;
    private AudioSource audioSource;
    private int isMovingHash;
    private int isIdleHash;
    private int VelocityHash;
    private float horse1Velocity;
    private float horse2Velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        audioSource = GetComponent<AudioSource>();
        horse1Animator = horse1.GetComponent<Animator>();
        horse2Animator = horse2.GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
        isIdleHash = Animator.StringToHash("isIdle");
        VelocityHash = Animator.StringToHash("Velocity");
    }


    // finds the corresponding visual wheel
    // correctly applies the transform
    private void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        if (isBraking && rb.velocity.magnitude < 0.05)
        {
            horse1Animator.SetBool(isMovingHash, false);
            horse1Animator.SetBool(isIdleHash, true);
            horse2Animator.SetBool(isMovingHash, false);
            horse2Animator.SetBool(isIdleHash, true);
            audioSource.Stop();
            Destroy(rb);
            Destroy(this);
        }
        else
        {
            Drive();
            ApplyLocalPositionToVisuals(wheelFL);
            ApplyLocalPositionToVisuals(wheelFR);
            ApplyLocalPositionToVisuals(wheelRL);
            ApplyLocalPositionToVisuals(wheelRR);
            CheckPlayerDistance();
            Braking();
        }

    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        horse1Velocity = currentSpeed;
        horse1Animator.SetFloat(VelocityHash, horse1Velocity);
        horse2Velocity = currentSpeed;
        horse2Animator.SetFloat(VelocityHash, horse2Velocity);

        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void CheckPlayerDistance()
    {
        if(Vector3.Distance(transform.position, player.position) < brakingDistance)
        {
            isBraking = true;
        }
    }

    private void Braking()
    {
        if (isBraking)
        {
            Debug.Log("Is Braking");
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }
}
