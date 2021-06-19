using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;

public class SimpleCarController : MonoBehaviour
{
    [NonSerialized] public PhotonView PV;
    [NonSerialized] public PhotonTransformView PTV;

    public Rigidbody theRB;
    public float forwardAccel, reverseAccel, maxSpeed, turnStrength, gravityForce, dragGround;

    private float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;
    public PauseMenu ps;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn;

    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (!PV.IsMine)
        {
            Destroy(ps.gameObject);
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
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime /* Input.GetAxis("Vertical")*/, 0f));
        }

        //leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) -180,  leftFrontWheel.localRotation.eulerAngles.z);
        //rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn,  rightFrontWheel.localRotation.eulerAngles.z);

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
