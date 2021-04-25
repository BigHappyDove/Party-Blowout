using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;

public class SimpleCarController : MonoBehaviour
{
    [NonSerialized] public PhotonView PV;
    [NonSerialized] public PhotonTransformView TPV;
    [NonSerialized] public LapTimeManager CarUI;

    public Rigidbody theRB;
    public float forwardAccel, reverseAccel, maxSpeed, turnStrength, gravityForce, dragGround;

    private float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn;


    // public float BestLapTime { get; private set; } = Mathf.Infinity;
    // public float LastLapTime { get; private set; } = 0;
    // public float CurrentLapTime { get; private set; } = 0;
    // public int CurrentLap { get; private set; } = 0;
    // private float lapTimer;
    // private int lastCheckpointPassed;
    //
    // private Transform checkpointsParent;
    // private int checkpointCount;
    // private int checkpointLayer;


    void Awake()
    {
        // PV = GetComponent<PhotonView>();
        // TPV = GetComponent<PhotonTransformView>();

        //
        // checkpointsParent = GameObject.Find("CHECKPOINTS").transform;
        // checkpointCount = checkpointsParent.childCount;
        // checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        TPV = GetComponent<PhotonTransformView>();

        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        theRB.transform.parent = null;
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        speedInput = 0;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 500;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 500;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        //leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) -180,  leftFrontWheel.localRotation.eulerAngles.z);
        //rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn,  rightFrontWheel.localRotation.eulerAngles.z);


        //CurrentLapTime = lapTimer > 0 ? Time.time - lapTimer : 0;
        // if (CurrentLap == 4)
        // {
        //     EndRace();
        // }

        transform.position = theRB.transform.position;
    }

    void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }


        if (grounded)
        {
            theRB.drag = dragGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                theRB.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            theRB.drag = 0.1f;
            theRB.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }
}
