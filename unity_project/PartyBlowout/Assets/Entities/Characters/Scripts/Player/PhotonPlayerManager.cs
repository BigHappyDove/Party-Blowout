using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonPlayerManager : MonoBehaviour
{
    void Start()
    {
        CreateController();
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Entities", "Player/Player"), Vector3.zero, Quaternion.identity);
    }
}
