using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _textPlayerList;

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateListPlayers();
        DebugTools.PrintOnGUI("OK1");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdateListPlayers();
        DebugTools.PrintOnGUI("OK2");
    }

    public override void OnJoinedRoom()
    {
        DebugTools.PrintOnGUI("OK3");
    }


    public override void OnEnable() { UpdateListPlayers();}


    private void UpdateListPlayers()
    {
        _textPlayerList.text = "";
        foreach (KeyValuePair<int, Photon.Realtime.Player> val in PhotonNetwork.CurrentRoom.Players)
            _textPlayerList.text += val.Value.NickName + "\n";
    }

}
