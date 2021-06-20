using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    [NonSerialized] public CheckpointSingle lastCheckpoint;
    [NonSerialized] public CheckpointsManager checkpointsManager;
    [NonSerialized] public int posCheckpoint = 0;
    public SimpleCarController _simpleCarController;
    private int _lapCount = 0;
    private PhotonView _pv;


    public static event Action<int, int> onCheckpointPassedHook;
    public static void onCheckpointPassed(int checkpoint, int loop) {onCheckpointPassedHook?.Invoke(checkpoint, loop);}

    private void Start()
    {
        _pv = GetComponent<PhotonView>();
        if(!_pv.IsMine) return;
        lastCheckpoint = checkpointsManager.checkpointSingles[0];
    }

    public void PassedCheckpoint(CheckpointSingle checkpointSingle)
    {
        if(!_pv.IsMine) return;
        lastCheckpoint = checkpointSingle;
        _pv.RPC("RPC_PassedCheckpoint", RpcTarget.All, checkpointsManager.checkpointSingles[0] == lastCheckpoint);
    }

    [PunRPC]
    private void RPC_PassedCheckpoint(bool newLap)
    {
        if (newLap) _lapCount++;
        posCheckpoint++;
        if(_pv.IsMine)
            onCheckpointPassed(posCheckpoint - _lapCount * checkpointsManager.checkpointSingles.Count, _lapCount);
        if(_lapCount == 3 && !Gamemode.IsEnded)
            Gamemode.onRoundEnded(Gamemode.PlayerTeam.Alone, _pv);
    }

    // [PunRPC]
    // private void TeleportOnCP(int playerID)
    // {
    //     var pv = PhotonView.Find(playerID);
    //     pv.transform.position = lastCheckpoint.transform.position;
    //     pv.transform.rotation = lastCheckpoint.transform.rotation;
    // }

    void Update()
     {
         if (Input.GetKeyDown(KeyCode.K) && _pv.IsMine)
         {
             _pv.transform.position = lastCheckpoint.transform.position;
             _simpleCarController.PV.transform.rotation = lastCheckpoint.transform.rotation;
             _simpleCarController.theRB.velocity = Vector3.zero;
             // GetComponent<PhotonView>().RPC("TeleportOnCP", RpcTarget.All, playerID);
         }
     }
}
