using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiShooter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blueUI;
    [SerializeField] private TextMeshProUGUI redUI;
    void Start()
    {
        if(!blueUI || !redUI)
            DebugTools.PrintOnGUI("Missing UI component in UIShooter.cs", DebugTools.LogType.WARNING);
    }
    void Update()
    {
        if(blueUI != null)
            blueUI.SetText("Red: " + Shooter.Score[1]);
        if(redUI != null)
            redUI.SetText("Blue: " + Shooter.Score[0]);
    }
}
