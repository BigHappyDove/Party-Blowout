using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void LoadScene(int indexSceneToLoad)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(indexSceneToLoad);
        }
    }
}
