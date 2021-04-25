using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckpointSingle> checkpointSingleList = new List<CheckpointSingle>();
    
    private List<int> nextCheckpointSingleIndexList;
    
    public static CarManager carManager;
    
    private List<SimpleCarController> carTransformList = carManager.listCars;

    public GameObject checkpoints;
    
    // public GameObject LapCompleteTrig;
    //
    // public GameObject MinuteDisplay;
    // public GameObject SecondDisplay;
    // public GameObject MilliDisplay;
    // public GameObject LapTimeBox;

    private int LapCount = 0;
    
    private void Start()
    {
        for (int i = 0; i < checkpoints.transform.childCount; i++)
        {
            GameObject checkpoint = checkpoints.transform.GetChild(i).gameObject;
            CheckpointSingle checkpointSingle = checkpoint.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }
    
        //nextCheckpointSingleIndexList = new List<int>();

        // DebugTools.PrintOnGUI(carTransformList.Count);
        // foreach (SimpleCarController carTransform in carTransformList)
        // {
        //     nextCheckpointSingleIndexList.Add(0);
        //     DebugTools.PrintOnGUI("player : " + carTransform.theRB.name);
        // }
    }

    public void EndRaceForPlayer(SimpleCarController player)
    {
        player.forwardAccel = 0;
        player.maxSpeed = 0;
        player.reverseAccel = 0;
        DebugTools.PrintOnGUI("Race is Finished you dumbass");
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle, SimpleCarController carTransform)
    {
        if (!carTransform.PV.IsMine)
            return;
        if (checkpointSingle.lastCheckpointPassed != null)
        {
            checkpointSingle.checkpointToPass = checkpointSingle.nextCheckpoint;
            checkpointSingle.lastCheckpointPassed = checkpointSingle;
            if (checkpointSingle.name == "14")
            {
                carTransform.CarUI.Checkpointnumber = 1;
                carTransform.CarUI.LapCountNumber += 1;

                if (carTransform.CarUI.LapCountNumber == 3)
                {
                    EndRaceForPlayer(carTransform);
                }
            }
            else
            {
                carTransform.CarUI.Checkpointnumber += 1;
            }
        }













        // int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        //
        // if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        // {
        //     Debug.Log("Correct Checkpoint");
        //     
        //     //CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
        //     nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
        //
        // }
        // else
        // {
        //     Debug.Log("Wrong Checkpoint");
        //     
        //     //faire une fonction qui tp au checkpoint précédent
        //     //et affiche sur le UI que c'est pas le bon checkpoint.
        // }
        /////////////////////////////////////////////////////////////
        // if (checkpointSingle.gameObject.name == "14")
        // {
        //     carTransform.CarUI.LapCount++;
        //     carTransform.CarUI.LapTime = tempsdecourse;
        //     carTransform.CarUI.BestTime = Mathf.Min(carTransform.CarUI.BestTime, carTransform.LapTime);
        // }
        // checkpointSingle.gameObject.SetActive(false);
        // checkpointSingle.nextCheckpoint.gameObject.SetActive(true);



    }
}
