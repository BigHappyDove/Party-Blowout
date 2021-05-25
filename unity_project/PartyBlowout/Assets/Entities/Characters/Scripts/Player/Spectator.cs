using System;
using UnityEngine;

public class Spectator : Player
{
    protected override void Start()
    {
        _pauseMenu = GetComponentInChildren<PauseMenu>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected override void Awake() { }

    protected override void Update()
    {
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
    }

    protected override void Move()
    {
        Vector3 moveDir = Input.GetAxisRaw("Vertical") * cameraHolder.transform.forward + Input.GetAxis("Horizontal") * cameraHolder.transform.right;
        moveAmount = moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
    }

    protected override void FixedUpdate()
    {
        transform.Translate(moveAmount * Time.fixedDeltaTime, Space.World);
    }

}