﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
            onCheckpointPassed(posCheckpoint - _lapCount *checkpointsManager.checkpointSingles.Count,_lapCount);
    }

}
