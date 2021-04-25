using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    public SimpleCarController _simpleCarController;
    public CheckpointSingle lastCheckpoint;
    public CheckpointsManager checkpointsManager;
    public int posCheckpoint = 0;
    // public GameObject checkpointsManager;
    private int _lapCount = 0;
    private PhotonView _pv;


    public static event Action<int, int> onCheckpointPassedHook;
    public static void onCheckpointPassed(int checkpoint, int loop) {onCheckpointPassedHook?.Invoke(checkpoint, loop);}

    private void Start()
    {
        _pv = GetComponent<PhotonView>();
    }

    public void PassedCheckpoint(CheckpointSingle checkpointSingle)
    {
        lastCheckpoint = checkpointSingle;
        if(_pv.IsMine)
            RPC_PassedCheckpoint(checkpointsManager.checkpointSingles[0] == lastCheckpoint);
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
