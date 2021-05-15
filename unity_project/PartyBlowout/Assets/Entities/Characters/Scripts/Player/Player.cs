using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Player : AliveEntity, IPunInstantiateMagicCallback
{

    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime, doubleJumpMultiplier; // smoothTime smooth out our movement

    float verticalLookRotation;
    public bool grounded;
    bool canDoubleJump;
    private AudioManager _audioManager;

    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;


    [Header("Team settings")]
    public Gamemode.PlayerTeam playerTeam;
    [SerializeField] private Material[] _materialsTeam = new Material[3];


    void Start()
    {
        _audioManager = GetComponent<AudioManager>();
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }

    }

    [PunRPC]
    void RPC_SyncAttributes(int team)
    {
        playerTeam = (Gamemode.PlayerTeam) team;
        ApplyTeamMaterial();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (!PV.IsMine) return;
        Hashtable playerProperties = PV.Owner.CustomProperties;
        if (playerProperties.ContainsKey("team") && playerProperties["team"] is Gamemode.PlayerTeam team)
        {
            playerTeam = team;
            DebugTools.PrintOnGUI($"Team found in custom properties of the player! {playerTeam}");
        }
        else
        {
            playerTeam = (Gamemode.PlayerTeam) Random.Range(0, 2);
            DebugTools.PrintOnGUI($"Team not found in custom properties of the player! {playerTeam}", DebugTools.LogType.WARNING);
        }

        PV.RPC("RPC_SyncAttributes", RpcTarget.All, (int)playerTeam);
    }

    void ApplyTeamMaterial()
    {
        foreach (Transform t in transform)
        {
            Renderer r = t.gameObject.GetComponent<Renderer>();
            if (r != null)
                r.material = _materialsTeam[(int) playerTeam];
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
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        // FixedUpdate runs on a fixed interval -> Important to do all physics and movement calculations in the fixed update method so that movement speed isn't impacted by our fps
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
                _audioManager.Play("Jump");
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
}
