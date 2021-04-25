using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    public CheckpointSingle previousCheckpoint;
    public CheckpointSingle nextCheckpoint;
    [NonSerialized] public CheckpointSingle lastCheckpointPassed = null;
    [NonSerialized] public CheckpointSingle checkpointToPass = null;

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SimpleCarController car))
        {
            trackCheckpoints.PlayerThroughCheckpoint(this, car);
        }
    }
}
