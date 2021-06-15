using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITime : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeTmp;

    private void Update()
    {
        if(timeTmp)
            timeTmp.SetText(ConvertFloatToTime(Gamemode.time));
    }

    string ConvertFloatToTime(float milliC)
    {
        int secondCount = (int) milliC % 60;
        int minuteCount = (int) (milliC / 60);
        string secondDisplay = (secondCount <= 9 ? "0" : "") + secondCount;
        string minuteDisplay = (minuteCount <= 9 ? "0" : "") + minuteCount + ":";
        return minuteDisplay + secondDisplay;
    }
}
