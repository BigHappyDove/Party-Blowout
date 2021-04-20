using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerRaceGame : MonoBehaviour
{
    public GameObject UIRacePanel;

    public Text UITextCurrentLap;
    public Text UITextCurrentTime;
    public Text UITextLastLapTime;
    public Text UITextBestLapTime;

    public SimpleCarController UpdateUIForPlayer;

    private int currentLap;
    private float currentTime;
    private float lastLapTime;
    private float bestLapTime;

    void Update()
    {
        if (UpdateUIForPlayer == null)
        {
            return;
        }

        if (UpdateUIForPlayer.PV.IsMine)
        {
            if (UpdateUIForPlayer.CurrentLap != currentLap)
            {
                currentLap = UpdateUIForPlayer.CurrentLap;
                UITextCurrentLap.text = $"LAP: {currentLap}";
            }
            if (UpdateUIForPlayer.CurrentLapTime != currentTime)
            {
                currentTime = UpdateUIForPlayer.CurrentLapTime;
                UITextCurrentTime.text = $"TIME: {(int) currentTime/60}:{(currentTime) % 60:00.000}";
            }
            if (UpdateUIForPlayer.LastLapTime != lastLapTime)
            {
                lastLapTime = UpdateUIForPlayer.LastLapTime;
                UITextLastLapTime.text = $"TIME: {(int) lastLapTime/60}:{(lastLapTime) % 60:00.000}";
            }
            if (UpdateUIForPlayer.BestLapTime != bestLapTime)
            {
                bestLapTime = UpdateUIForPlayer.BestLapTime;
                UITextBestLapTime.text = bestLapTime < 100000000 ? $"TIME: {(int) bestLapTime/60}:{(bestLapTime) % 60:00.000}" : "BEST: NONE";
            }
        }

    }
}