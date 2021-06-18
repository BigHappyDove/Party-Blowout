using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

    void ShowScreen(Gamemode.PlayerTeam pt, PhotonView PV)
    {
        UIHandler.SetActive(true);
        string userName = PV != null && PV.Owner != null ? PV.Owner.NickName : "No one";
        string winner = pt == Gamemode.PlayerTeam.Alone ? userName : pt + " team";
        winnerTMP.SetText(winner + " won the round!\n" +
                          "Waiting for a new round to start...");
    }
}
