﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;
using PhotonPlayer = Photon.Realtime.Player;

public class Player : AliveEntity, IPunInstantiateMagicCallback
{

    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime, doubleJumpMultiplier; // smoothTime smooth out our movement

    float verticalLookRotation;
    public bool grounded;
    bool canDoubleJump;
    private AudioManager _audioManager;
    private PauseMenu _pauseMenu;

    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;


    [Header("Team settings")]
    public Gamemode.PlayerTeam playerTeam;
    [SerializeField] private Material[] _materialsTeam = new Material[3];


    void Start()
    {
        _pauseMenu = GetComponentInChildren<PauseMenu>();
        _audioManager = GetComponent<AudioManager>();
        if (!PV.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            Destroy(rb);
            Destroy(_pauseMenu.gameObject);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        if (PV.IsMine && PV.Owner.CustomProperties.ContainsKey("team"))
            SetTeam();
    }

    public override void OnPlayerPropertiesUpdate(PhotonPlayer targetPlayer, Hashtable changedProps)
    {
        if (PV.IsMine && PV.Owner == targetPlayer && changedProps.ContainsKey("team")) SetTeam();
    }

    void SetTeam()
    {
        Gamemode.PlayerTeam? team = GetTeam(PV);
        if(team != null)
        {
            playerTeam = (Gamemode.PlayerTeam) team;
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
            if(t.gameObject.name == "UserInfo") return;
            Renderer r = t.gameObject.GetComponent<Renderer>();
            if (r != null)
                r.material = _materialsTeam[(int) playerTeam];
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        if (_pauseMenu != null)
        {
            if (_pauseMenu.GameIsPaused)
            {
                moveAmount = Vector3.zero;
                return;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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
