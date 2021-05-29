using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Spectator : Player
{
    private Player[] alivePlayers;
    private Player boundPlayer = null;

    private Camera freeCam;
    private int selectedPlayer = 0;

    protected override void Start()
    {
        alivePlayers = FindObjectsOfType<Player>();
        _pauseMenu = GetComponentInChildren<PauseMenu>();
        freeCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DebugTools.PrintOnGUI(alivePlayers.Length);
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

        if (freeCam.gameObject.activeSelf)
        {
            Look();
            Move();
        }

        if (Input.GetKeyDown(KeyCode.R) && boundPlayer != null)
        {
            transform.position = boundPlayer.cameraObj.transform.position;
            freeCam.transform.rotation = boundPlayer.cameraObj.transform.rotation;
            boundPlayer.cameraObj.SetActive(false);
            boundPlayer = null;
            RecoverFreeCam();
        }
        RecoverFreeCam();
        SelectPlayer();
    }

    protected override void FixedUpdate()
    {
        transform.Translate(moveAmount * Time.fixedDeltaTime, Space.World);
    }

    private void RecoverFreeCam()
    {
        if (boundPlayer != null && boundPlayer.cameraObj != null) return;
        freeCam.gameObject.SetActive(true);
        selectedPlayer = 0;
    }

    private void SelectPlayer()
    {
        int i = 0;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            alivePlayers = FindObjectsOfType<Player>();
            do
            {
                selectedPlayer++;
                if (selectedPlayer > alivePlayers.Length - 1)
                    selectedPlayer = 0;
            } while (i++ < alivePlayers.Length && alivePlayers[selectedPlayer].cameraObj == null);
            SwitchPlayer();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            alivePlayers = FindObjectsOfType<Player>();
            do
            {
                selectedPlayer--;
                if (selectedPlayer < 0)
                    selectedPlayer = alivePlayers.Length - 1;
            } while (i++ < alivePlayers.Length && alivePlayers[selectedPlayer].cameraObj == null);
            SwitchPlayer();
        }
    }

    private void SwitchPlayer()
    {
        for (int i = 0; i < alivePlayers.Length; i++)
        {
            Player p = alivePlayers[i];
            p.cameraObj.SetActive(i == selectedPlayer);
            boundPlayer = i == selectedPlayer ? p : boundPlayer;

        }
        freeCam.gameObject.SetActive(false);
    }


    protected override void Move()
    {
        Vector3 moveDir = Input.GetAxisRaw("Vertical") * cameraHolder.transform.forward + Input.GetAxis("Horizontal") * cameraHolder.transform.right;
        moveAmount = moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
    }


}