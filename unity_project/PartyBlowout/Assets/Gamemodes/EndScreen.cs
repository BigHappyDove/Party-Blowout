using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject UIHandler;
    [SerializeField] private TextMeshProUGUI winnerTMP;

    void Start()
    {
        UIHandler.SetActive(false);
        Gamemode.onRoundEndedHook += ShowScreen;
    }

    private void OnDestroy()
    {
        Gamemode.onRoundEndedHook -= ShowScreen;
    }

    void ShowScreen(Gamemode.PlayerTeam pt, Player p)
    {
        UIHandler.SetActive(true);
        string userName = p != null && p.PV != null && p.PV.Owner != null ? p.PV.Owner.NickName : "undefined";
        string winner = pt == Gamemode.PlayerTeam.Alone ? userName : pt + " team";
        winnerTMP.SetText(winner + " won the round!");
    }
}
