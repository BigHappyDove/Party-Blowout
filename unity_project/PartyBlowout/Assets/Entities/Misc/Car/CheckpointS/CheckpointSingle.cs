using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckpointSingle : MonoBehaviour
{
    // private TrackCheckpoints trackCheckpoints;
    public CheckpointSingle previousCheckpoint;
    public CheckpointSingle nextCheckpoint;
    public int id;
    [NonSerialized] public CheckpointSingle lastCheckpointPassed = null;
    [NonSerialized] public CheckpointSingle checkpointToPass = null;

    // public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    // {
    //     this.trackCheckpoints = trackCheckpoints;
    // }


    private void OnTriggerEnter(Collider other)
    {
        DebugTools.PrintOnGUI(id + "passed owo" + other.name);
        if (other.TryGetComponent(out CheckpointTracker checkpointTracker))
        {
            // TODO: Vérifier que c'est le bon checkpoint
            checkpointTracker.PassedCheckpoint(this);
        }
    }
}
