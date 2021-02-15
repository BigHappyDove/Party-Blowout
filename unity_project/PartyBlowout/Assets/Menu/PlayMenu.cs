using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playMenu, roomMenu, loadingScreen;

    public void CreateMultiplayerGame()
    {
        RoomMatch.TryCreateRoom();
        //TODO: handle when we can't join/create room
    }
}
