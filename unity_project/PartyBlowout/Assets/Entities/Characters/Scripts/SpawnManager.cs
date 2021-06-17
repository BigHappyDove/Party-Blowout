using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    SpawnPoint[] spawnpoints;

    void Awake() => spawnpoints = GetComponentsInChildren<SpawnPoint>();

    public Transform GetSpawnpoint()
    {
        SpawnPoint s;
        int maxIter = 20;
        int i = 0;
        while ((s = spawnpoints[Random.Range(0, spawnpoints.Length)]).isOccupied && i < maxIter)
        {
            DebugTools.PrintOnGUI(i);
            i++;
        }
        return s.transform;
    }
}