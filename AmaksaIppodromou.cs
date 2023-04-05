using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaksaIppodromou : MonoBehaviour
{
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private float maxMotorTorque = 80f;
    [SerializeField] private float maxBrakeTorque = 120f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed = 80f;
    [SerializeField] private float maxSteerAngle = 44f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private GameObject horse1;
    [SerializeField] private GameObject horse2;
    [SerializeField] private GameObject horse3;
    [SerializeField] private GameObject horse4;
    [SerializeField] private Transform path;
    [SerializeField] private ParticleSystem smoke;

    private Rigidbody rb;
    private bool isBraking = false;
    private Animator horse1Animator;
    private Animator horse2Animator;
    private Animator horse3Animator;
    private Animator horse4Animator;
    private int isMovingHash;
    private int isIdleHash;
    private int VelocityHash;
    private float horse1Velocity;
    private float horse2Velocity;
    private float horse3Velocity;
    private float horse4Velocity;
    private List<Transform> nodes;
    private int currentNode = 0;
    private float targetSteerAngle = 0;
    private AudioSource audioSource;
    private bool isMoving = false;

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        horse1Animator = horse1.GetComponent<Animator>();
        horse2Animator = horse2.GetComponent<Animator>();
        horse3Animator = horse3.GetComponent<Animator>();
        horse4Animator = horse4.GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
        isIdleHash = Animator.StringToHash("isIdle");
        VelocityHash = Animator.StringToHash("Velocity");
    }

    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
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
        if (IsMoving)
        {
            if (isBraking && rb.velocity.magnitude < 0.05)
            {
                StopMoving();
            }
            else
            {
                Drive();
                ApplySteer();
                ApplyLocalPositionToVisuals(wheelRL);
                ApplyLocalPositionToVisuals(wheelRR);
                CheckWaypointDistance();
                LerpToSteerAngle();
                Braking();
                SmokeEmission();
            }
        }
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        horse1Velocity = rb.velocity.magnitude;
        horse1Animator.SetFloat(VelocityHash, horse1Velocity);
        horse2Velocity = rb.velocity.magnitude;
        horse2Animator.SetFloat(VelocityHash, horse2Velocity);
        horse3Velocity = rb.velocity.magnitude;
        horse3Animator.SetFloat(VelocityHash, horse3Velocity);
        horse4Velocity = rb.velocity.magnitude;
        horse4Animator.SetFloat(VelocityHash, horse4Velocity);

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

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetSteerAngle = newSteer;
    }

    private void Braking()
    {
        if (isBraking)
        {
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 1f)
        {
            currentNode++;

            if(currentNode == 2)
            {
                isBraking = true;
            }
            if(currentNode == 10)
            {
                isBraking = false;
            }
            if (currentNode == 12)
            {
                isBraking = true;
            }
        }
    }

    private void LerpToSteerAngle()
    {
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }

    private void SmokeEmission()
    {
        var emission = smoke.emission;
        emission.rateOverTime = rb.velocity.magnitude * 3;
    }

    public void StartMoving()
    {
        horse1Animator.SetBool(isMovingHash, true);
        horse1Animator.SetBool(isIdleHash, false);
        horse2Animator.SetBool(isMovingHash, true);
        horse2Animator.SetBool(isIdleHash, false);
        horse3Animator.SetBool(isMovingHash, true);
        horse3Animator.SetBool(isIdleHash, false);
        horse4Animator.SetBool(isMovingHash, true);
        horse4Animator.SetBool(isIdleHash, false);
        audioSource.Play();
        smoke.Play();
        IsMoving = true;
    }

    public void StopMoving()
    {
        horse1Animator.SetBool(isMovingHash, false);
        horse1Animator.SetBool(isIdleHash, true);
        horse2Animator.SetBool(isMovingHash, false);
        horse2Animator.SetBool(isIdleHash, true);
        horse3Animator.SetBool(isMovingHash, false);
        horse3Animator.SetBool(isIdleHash, true);
        horse4Animator.SetBool(isMovingHash, false);
        horse4Animator.SetBool(isIdleHash, true);
        Destroy(smoke);
        audioSource.Stop();
        IsMoving = false;
    }
}
