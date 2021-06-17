using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    public bool isOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && TestSpawner(other))
        {
            DebugTools.PrintOnGUI("EnteredSpawn, TRUE");
            isOccupied = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isOccupied && TestSpawner(other))
        {
            DebugTools.PrintOnGUI("StayedSpawn, TRUE");
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOccupied && TestSpawner(other))
        {
            DebugTools.PrintOnGUI("LeftSpawn, FALSE");
            isOccupied = false;
        }
    }

    private bool TestSpawner(Collider other)
    {
        bool testHumans = other.TryGetComponent(out Player p) && !(p is Spectator);
        bool testCars = other.TryGetComponent(out SimpleCarController s);
        return testHumans || testCars;
    }

    /// <summary>
    /// Hide visuals of the spawn points
    /// </summary>
    void Awake()
    {
        isOccupied = false;
        if(graphics)
            graphics.SetActive(false);
    }
}
