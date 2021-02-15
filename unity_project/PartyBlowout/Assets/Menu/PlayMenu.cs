using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playMenu, roomMenu, loadingScreen;

    public void CreateMultiplayerGame()
    {
        if (PhotonNetwork.InLobby)
        {
            playMenu.SetActive(false);
            roomMenu.SetActive(false);
            loadingScreen.SetActive(true);
            RoomMatch.TryCreateRoom();
        }
    }

    public void LeaveRoom()
    {
        playMenu.SetActive(true);
        roomMenu.SetActive(false);
        loadingScreen.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        playMenu.SetActive(false);
        loadingScreen.SetActive(false);
        roomMenu.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        DebugTools.PrintOnGUI($"Couldn't join the room!\n" +
                              $"Code:{returnCode}\n" +
                              $"Msg:{message}",DebugTools.LogType.ERROR);
        playMenu.SetActive(true);
        loadingScreen.SetActive(false);
        roomMenu.SetActive(false);
    }
}
