using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Used to manage rooms.
/// </summary>
public class RoomMatch : MonoBehaviourPunCallbacks
{
    public byte maxPlayersInRoom = 10; // 0 <= x <= 255

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        TryJoinRoom();
    }

    public void TryJoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void CancelJoinRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void TryCreateRoom()
    {
        string roomName = Random.Range(0, 2147483647).ToString("X");
        RoomOptions roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = maxPlayersInRoom};
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        DebugTools.PrintOnGUI($"Created room {roomName}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        DebugTools.PrintOnGUI($"Failed to join a room!", DebugTools.LogType.WARNING);
        TryCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        DebugTools.PrintOnGUI($"Joined room {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        DebugTools.PrintOnGUI($"Failed to create a room!", DebugTools.LogType.WARNING);
        TryCreateRoom();
    }
}
