using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGuessWho : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI seekersUI;
    [SerializeField] private TextMeshProUGUI hidersUI;
    void Start()
    {
        if(!seekersUI || !hidersUI)
            DebugTools.PrintOnGUI("Missing UI component in UIGuessWho.cs", DebugTools.LogType.WARNING);
    }
    void Update()
    {
        if(seekersUI != null)
            seekersUI.SetText("Seekers: " + GuessWho.alivePlayers[1]);
        if(hidersUI != null)
            hidersUI.SetText("Hiders: " + GuessWho.alivePlayers[0]);
    }
}
