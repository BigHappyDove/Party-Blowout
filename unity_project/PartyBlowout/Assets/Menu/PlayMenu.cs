using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playMenu, roomMenu, loadingScreen;
    [SerializeField] private TextMeshProUGUI textIDRoom;

    private void Start()
    {
        AudioListener.volume = 0.5f;
    }

    public static string TryCreateRoom()
    {
        string roomName = Random.Range(0, 2147483647).ToString("X");
        RoomOptions roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = 10};
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        DebugTools.PrintOnGUI($"Created room {roomName}");
        return roomName;
    }

    public void CreateMultiplayerGame()
    {
        if (!PhotonNetwork.IsConnected) return;
        playMenu.SetActive(false);
        roomMenu.SetActive(false);
        loadingScreen.SetActive(true);
        TryCreateRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        playMenu.SetActive(false);
        loadingScreen.SetActive(false);
        roomMenu.SetActive(true);
        if(textIDRoom)
            textIDRoom.SetText("Room ID " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if(!playMenu.activeSelf) return;
        DebugTools.PrintOnGUI($"Couldn't join the room!\n" +
                              $"Code:{returnCode}\n" +
                              $"Msg:{message}",DebugTools.LogType.ERROR);
        playMenu.SetActive(true);
        loadingScreen.SetActive(false);
        roomMenu.SetActive(false);
    }
}
