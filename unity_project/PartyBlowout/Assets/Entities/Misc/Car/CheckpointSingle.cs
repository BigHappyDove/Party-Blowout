using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckpointSingle : MonoBehaviour
{
    // public float BestLapTime { get; private set; } = Mathf.Infinity;
    // public float LastLapTime { get; private set; } = 0;
    // public float CurrentLapTime { get; private set; } = 0;
    // public int CurrentLap { get; private set; } = 0;
    // private float lapTimer;
    // private int lastCheckpointPassed;
    //
    // private Transform checkpointsParent;
    // private int checkpointCount;
    // private int checkpointLayer;
    //
    // void Awake()
    // {
    //     checkpointsParent = GameObject.Find("CHECKPOINTS").transform;
    //     checkpointCount = checkpointsParent.childCount;
    //     checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    // }
    // private void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.gameObject.layer != checkpointLayer)
    //     {
    //         return;
    //     }
    //
    //     if (collider.gameObject.name == "1")
    //     {
    //         if (lastCheckpointPassed == checkpointCount)
    //         {
    //             EndLap();
    //         }
    //
    //         if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
    //         {
    //             StartLap();
    //         }
    //         return;
    //     }
    //
    //     if (collider.gameObject.name == (lastCheckpointPassed+1).ToString())
    //     {
    //         lastCheckpointPassed++;
    //     }
    // }
    // void StartLap()
    // {
    //     CurrentLap++;
    //     lastCheckpointPassed = 1;
    //     lapTimer = Time.time;
    // }
    //
    // void EndLap()
    // {
    //     LastLapTime = Time.time - lapTimer;
    //     BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    // }
    //
    // private void Update()
    // {
    //     CurrentLapTime = lapTimer > 0 ? Time.time - lapTimer : 0;
    // }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // private PhotonView _pv;
    // private SimpleCarController car;
    //
    // private TrackCheckpoints _trackCheckpoints;
    // private MeshRenderer _meshRenderer;
    //
    // private void Awake()
    // {
    //     _meshRenderer = GetComponent<MeshRenderer>();
    // }
    //
    // private void Start()
    // {
    //     Hide();
    //     _pv = GetComponent<PhotonView>();
    //     car = GetComponent<SimpleCarController>();
    //
    // }
    //
    // private void OnCollisionEnter(Collision collision)
    // {
    //     SimpleCarController car = collision.gameObject.GetComponent<SimpleCarController>();
    //     if (!car.PV.IsMine)
    //     {
    //         return;
    //     }
    //     _trackCheckpoints.PlayerThroughCheckpoint(this, car.transform);
    // }
    //
    // public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    // {
    //     this._trackCheckpoints = trackCheckpoints;
    // }
    //
    // public void Hide()
    // {
    //     _meshRenderer.enabled = false;
    // }
    //
    // public void Show()
    // {
    //     _meshRenderer.enabled = true;
    // }
}
