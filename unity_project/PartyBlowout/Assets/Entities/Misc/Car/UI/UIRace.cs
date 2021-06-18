using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class UIRace : MonoBehaviour
{
    private PhotonView _pv;

    private float _totalMilliCount;
    private float _lapMilliCount;
    private float _bestLapMilliCount = 0;

    [Header("Time")]
    public Text MinuteBox;
    public Text SecondBox;
    public Text MilliBox;

    [Header("BestTime")]
    public Text bestMinuteBox;
    public Text bestSecondBox;
    public Text bestMilliBox;

    [Header("Lap / Checkpoints")]
    public Text lapsBox;
    public Text checkpointsBox;


    private void Start()
    {
        _pv = GetComponent<PhotonView>();
        if(_pv.IsMine)
        {
            UpdateUiCheckpoints(0,0);
            CheckpointTracker.onCheckpointPassedHook += UpdateUiCheckpoints;
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(_pv.IsMine)
            CheckpointTracker.onCheckpointPassedHook -= UpdateUiCheckpoints;
    }

    void Update()
    {
        _lapMilliCount += Time.deltaTime;
        UpdateBoxes(MilliBox, SecondBox, MinuteBox, _lapMilliCount);
    }

    void UpdateUiCheckpoints(int checkpoint, int lap)
    {
        checkpointsBox.text = (checkpoint + 1).ToString();
        if (lapsBox.text == (lap+1).ToString()) return;
        if (_bestLapMilliCount == 0 || _lapMilliCount < _bestLapMilliCount)
            _bestLapMilliCount = _lapMilliCount;
        _lapMilliCount = 0;
        UpdateBoxes(bestMilliBox, bestSecondBox, bestMinuteBox, _bestLapMilliCount);
        lapsBox.text = (lap+1).ToString();
    }

    void UpdateBoxes(Text milli, Text second, Text minute, float milliC)
    {
        int secondCount = (int) milliC % 60;
        int minuteCount = (int) (milliC / 60);
        string milliDisplay = ((int)((milliC - secondCount)*10)%10).ToString();
        milli.text = "" + milliDisplay;
        second.text = (secondCount <= 9 ? "0" : "") + secondCount + ".";
        minute.text = (minuteCount <= 9 ? "0" : "") + minuteCount + ":";
    }
}
