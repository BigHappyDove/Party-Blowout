using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckpointSingle : MonoBehaviour
{
    public CheckpointSingle previousCheckpoint;
    public CheckpointSingle nextCheckpoint;
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CheckpointTracker checkpointTracker))
        {
            if(checkpointTracker.lastCheckpoint.nextCheckpoint == this)
                checkpointTracker.PassedCheckpoint(this);
            //TODO: Clovis fais un truc quand le gars se plante de checkpoint stppppp
        }
    }
}
