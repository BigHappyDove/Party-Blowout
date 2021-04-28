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
            onCheckpointPassed(posCheckpoint - _lapCount *checkpointsManager.checkpointSingles.Count,_lapCount);
    }

    // [PunRPC]
    // private void TeleportOnCP()
    // {
    //     _simpleCarController.transform.position = lastCheckpoint.transform.position;
    //     _simpleCarController.transform.rotation = lastCheckpoint.transform.rotation;
    // }
    //
    // private void OnDestroy()
    // {
    //     if (_pv.IsMine)
    //     {
    //         GameObject g = PhotonNetwork.Instantiate("Entities/" + _simpleCarController.gameObject.name,
    //             lastCheckpoint.transform.position, lastCheckpoint.transform.rotation);
    //     }
    // }
    //
    // void Update()
    // {
    //     if (Input.GetKey(KeyCode.K) && _pv.IsMine)
    //     {
    //         PhotonNetwork.Destroy(_simpleCarController.gameObject);

            // CheckpointTracker checkpointTracker = g.GetComponentInChildren<CheckpointTracker>();
            // checkpointTracker._lapCount = _lapCount;
            // checkpointTracker._simpleCarController = g.GetComponent<SimpleCarController>();
            //PhotonNetwork.RaiseEvent(1, new object[] {lastCheckpoint.transform.position, lastCheckpoint.transform.rotation}, new RaiseEventOptions() {Receivers = ReceiverGroup.All}, SendOptions.SendReliable);
    //     }
    // }

    // public void OnEvent(EventData photonevent)
    // {
    //     object[] data = (object[]) photonevent.CustomData;
    //     _simpleCarController.transform.position = (Vector3) data[0];
    //     _simpleCarController.transform.rotation = (Quaternion) data[1];
    // }
    // private void OnEnable()
    // {
    //     PhotonNetwork.AddCallbackTarget(this);
    // }
    //
    // private void OnDisable()
    // {
    //     PhotonNetwork.RemoveCallbackTarget(this);
    // }
}
