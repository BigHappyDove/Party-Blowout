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
    [SerializeField] private TransitionManager _tm;
    private PhotonView _pv;

    void Start()
    {
        UIHandler.SetActive(false);
        _pv = GetComponent<PhotonView>();
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


    [PunRPC]
    void RPC_CallBackShowTransition(int index)
    {
        if(_tm && index != 0)
            _tm.PlaySelected((Gamemode.CurrentGamemode) (index-1));
    }

    public void ShowTransition(int index)
    {
        _pv.RPC("RPC_CallBackShowTransition", RpcTarget.All, index);
    }
}
