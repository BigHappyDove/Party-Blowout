using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckpointSingle> _checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;
    private List<Transform> carTransformList;
    private PhotonView _pv;

    private void Start()
    { 
        _pv = GetComponent<PhotonView>();
    }

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("CHECKPOINTS");

        _checkpointSingleList = new List<CheckpointSingle>();

        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckpointSingle checkpointSingle = checkpointsTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            _checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointSingleIndexList.Add(0);
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        
        CheckpointSingle correctcheckpointSingle = _checkpointSingleList[nextCheckpointSingleIndex];
        
        if (_checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex && _pv.IsMine)
        {
            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % _checkpointSingleList.Count;
            
            correctcheckpointSingle.Hide();
            
        }
        else
        {
            //faire une fonction qui tp au checkpoint précédent
            //et affiche sur le UI que c'est pas le bon checkpoint.
            correctcheckpointSingle.Show();
        }
    }
}
