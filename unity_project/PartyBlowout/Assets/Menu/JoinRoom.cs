using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject joinMenu, roomMenu, loadingScreen;
    [SerializeField] private TMP_InputField _inputField;

    public void TryJoinRoom()
    {
        joinMenu.SetActive(false);
        loadingScreen.SetActive(true);
        roomMenu.SetActive(false);
        if (_inputField.text == "")
            PhotonNetwork.JoinRandomRoom();
        else
            PhotonNetwork.JoinRoom(_inputField.text);
    }

    public override void OnJoinedRoom()
    {
        joinMenu.SetActive(false);
        loadingScreen.SetActive(false);
        roomMenu.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        joinMenu.SetActive(true);
        loadingScreen.SetActive(false);
        roomMenu.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        OnJoinRoomFailed(returnCode, message);
        //TODO: Add message if no room available
    }
}
