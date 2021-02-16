using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// Used to connect the client to the Master Server.
/// </summary>
public class NetworkController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        PhotonNetwork.NickName = "Player" + Random.Range(0, 2147483647).ToString("X");
        DebugTools.PrintOnGUI($"CONNECTED TO {PhotonNetwork.CloudRegion} as {PhotonNetwork.NickName}");
    }

    public override void OnJoinedLobby()
    {
        DebugTools.PrintOnGUI($"Joined lobby {PhotonNetwork.CurrentLobby.Name}");
    }

    public override void OnLeftLobby()
    {
        PhotonNetwork.JoinLobby();
    }
}
