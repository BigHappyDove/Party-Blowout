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
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        PhotonNetwork.Instantiate(Path.Combine("Entities", "Player/Player"), spawnpoint.position, spawnpoint.rotation);
    }
}
