using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class testScript : MonoBehaviourPunCallbacks
{
    public Vector3[] spawnPoints;
    public override void OnJoinedRoom()
    {
        //PlayerManager.CreatePlayer(spawnPoints);
    }
}
