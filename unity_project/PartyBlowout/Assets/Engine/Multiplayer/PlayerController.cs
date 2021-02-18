using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime, doubleJumpMultiplier; // smoothTime smooth out our movement 

    float verticalLookRotation;
    bool grounded;
    bool canDoubleJump;
    
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    
    Rigidbody rb;
    PhotonView PV;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    
    private void Update()
    {
        if (!PV.IsMine)
            return;
        
        Look();
        Move();
        Jump();
    }

    void Look()
    {
        transform.Rotate(Vector3.up * (Input.GetAxisRaw("Mouse X") * mouseSensitivity));

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation; 
    }
    
    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        //Compact if statement : if "LeftShift" key pressed down (sprint key) then use sprintSpeed value. Otherwise, use walkSpeed value
    }

    void Jump()
    {
        if (grounded)
        {
            canDoubleJump = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                rb.AddForce(transform.up * (jumpForce * doubleJumpMultiplier));
                canDoubleJump = false;
            }
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        // FixedUpdate runs on a fixed interval -> Important to do all physics and movement calculations in the fixed update method so that movement speed isn't impacted by our fps
    }
}
