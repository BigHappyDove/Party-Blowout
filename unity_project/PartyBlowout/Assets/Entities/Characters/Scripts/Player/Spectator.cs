using System;
using System.Collections.Generic;
using Photon.Pun;
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
            RecoverFreeCam();
        }
        if(boundPlayer == null || boundPlayer.cameraObj == null)
            RecoverFreeCam();
        SelectPlayer();
    }

    protected override void FixedUpdate()
    {
        transform.Translate(moveAmount * Time.fixedDeltaTime, Space.World);
    }

    private void RecoverFreeCam()
    {
        if (boundPlayer != null && boundPlayer.cameraObj != null)
            boundPlayer = null;
        freeCam.gameObject.SetActive(true);
        selectedPlayer = 0;
    }

    //TODO: REFACTOR ME!!!
    private void SelectPlayer()
    {
        bool atLeastOneIsEligible = false;
        int i = 0;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            alivePlayers = FindObjectsOfType<Player>();
            do
            {
                selectedPlayer++;
                if (selectedPlayer > alivePlayers.Length - 1)
                    selectedPlayer = 0;
                atLeastOneIsEligible = atLeastOneIsEligible || CanSpecHim(alivePlayers[selectedPlayer])
                    && alivePlayers[selectedPlayer].cameraObj != null;
            } while (i++ < alivePlayers.Length && !CanSpecHim(alivePlayers[selectedPlayer])
                                               && alivePlayers[selectedPlayer].cameraObj == null);
            if(atLeastOneIsEligible)
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
                atLeastOneIsEligible = atLeastOneIsEligible || CanSpecHim(alivePlayers[selectedPlayer])
                    && alivePlayers[selectedPlayer].cameraObj != null;
            } while (i++ < alivePlayers.Length && !CanSpecHim(alivePlayers[selectedPlayer])
                                               && alivePlayers[selectedPlayer].cameraObj == null);
            if(atLeastOneIsEligible)
                SwitchPlayer();
        }
    }

    private void SwitchPlayer()
    {
        bool atLeatOneIsSelected = false;
        for (int i = 0; i < alivePlayers.Length; i++)
        {
            Player p = alivePlayers[i];
            if(p.cameraObj == null) continue;
            p.cameraObj.SetActive(i == selectedPlayer);
            boundPlayer = i == selectedPlayer ? p : boundPlayer;
            atLeatOneIsSelected = atLeatOneIsSelected || i == selectedPlayer;
        }

        if (!atLeatOneIsSelected)
            boundPlayer = null;

        freeCam.gameObject.SetActive(false);
    }


    protected override void Move()
    {
        Vector3 moveDir = Input.GetAxisRaw("Vertical") * cameraHolder.transform.forward + Input.GetAxis("Horizontal") * cameraHolder.transform.right;
        moveAmount = moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
    }

    private bool CanSpecHim(Player player)
    {
        return !(player is Spectator) && (Gamemode.CurGamemode != Gamemode.CurrentGamemode.GuessWho
                                          || playerTeam == Gamemode.PlayerTeam.Blue
                                          || playerTeam == player.playerTeam);
    }
}