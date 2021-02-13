using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static void CreatePlayer(Vector3 bottomLeft, Vector3 upRight)
    {
        float xPos = Random.Range(bottomLeft.x, upRight.x);
        float yPos = Random.Range(bottomLeft.y, upRight.y);
        float zPos = Random.Range(bottomLeft.z, upRight.z);
        Vector3 pos = new Vector3(xPos, yPos, zPos);

        PhotonNetwork.Instantiate(Path.Combine("Entities", "Player"), pos, Quaternion.identity);
    }

    public static void CreatePlayer(Vector3[] spawnPoints)
    {
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
        PhotonNetwork.Instantiate(Path.Combine("Entities", "Player"), pos, Quaternion.identity);
    }
}
