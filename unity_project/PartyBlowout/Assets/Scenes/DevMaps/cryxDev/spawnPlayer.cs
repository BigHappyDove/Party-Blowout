using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class spawnPlayer : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    private Vector3 pos;
    public GameObject myAvatar;


    public override void OnJoinedRoom()
    {
        PV = GetComponent<PhotonView>();
        pos = new Vector3(Random.Range(-10f, 10f), 0.5f, -2);

        PhotonNetwork.Instantiate(Path.Combine("CryxDevPrefabs", "Player"), pos, Quaternion.identity);

    }

}
