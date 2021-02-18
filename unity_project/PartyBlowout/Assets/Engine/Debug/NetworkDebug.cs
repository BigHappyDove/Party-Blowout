using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Used to debug common issues
/// </summary>
public class NetworkDebug : MonoBehaviourPunCallbacks
{
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        DebugTools.PrintOnGUI($"Failed to join a random room!", DebugTools.LogType.WARNING);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        DebugTools.PrintOnGUI($"Failed to join a custom room!", DebugTools.LogType.WARNING);
    }

    public override void OnJoinedRoom()
    {
        DebugTools.PrintOnGUI($"Joined room {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnLeftRoom()
    {
        DebugTools.PrintOnGUI("No longer in a room.");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        DebugTools.PrintOnGUI($"Failed to create a room!", DebugTools.LogType.WARNING);
    }
}
