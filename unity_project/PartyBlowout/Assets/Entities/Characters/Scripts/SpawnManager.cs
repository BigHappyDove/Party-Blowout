using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    SpawnPoint[] spawnpoints;

    void Awake() => spawnpoints = GetComponentsInChildren<SpawnPoint>();

    public Transform GetSpawnpoint()
    {
        int maxIter = 20;
        int i = 0;
        bool test;
        SpawnPoint s;
        do
        {
            s = spawnpoints[Random.Range(0, spawnpoints.Length)];
            test = s.IsSomeoneInRange(5);
            i++;
        } while (test && i < maxIter);

        return s.transform;
    }
}