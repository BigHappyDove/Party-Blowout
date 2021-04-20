using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckpointSingle> checkpointSingleList;
    
    private List<int> nextCheckpointSingleIndexList;
    
    private List<SimpleCarController> carTransformList;

    private int nextCheckpointSingleIndex;

    public CarManager carManager;

    public GameObject checkpoints;
    
    private void Start()
    {
        checkpointSingleList = new List<CheckpointSingle>();
        carTransformList = carManager.listCars;

        for (int i = 0; i < checkpoints.transform.childCount; i++)
        {
            GameObject checkpoint = checkpoints.transform.GetChild(i).gameObject;
            CheckpointSingle checkpointSingle = checkpoint.GetComponent<CheckpointSingle>();
            //DebugTools.PrintOnGUI(checkpoint.name + " :" + (checkpointSingle != null) + " " + (checkpoint.transform!= null));
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }
    
        nextCheckpointSingleIndexList = new List<int>();
        
        foreach (SimpleCarController carTransform in carTransformList)
        {
            nextCheckpointSingleIndexList.Add(0);
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle, SimpleCarController carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        
        CheckpointSingle correctcheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
        
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
            
            
        }
        else
        {
            //faire une fonction qui tp au checkpoint précédent
            //et affiche sur le UI que c'est pas le bon checkpoint.
        }
    }
}
