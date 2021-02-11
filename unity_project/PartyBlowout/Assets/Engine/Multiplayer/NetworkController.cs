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
        DebugTools.PrintOnGUI($"CONNECTED TO {PhotonNetwork.CloudRegion}");
    }
}
