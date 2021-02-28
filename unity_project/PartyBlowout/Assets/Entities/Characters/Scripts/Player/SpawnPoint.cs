using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject graphics; 

    /// <summary>
    /// Hide visuals of the spawn points
    /// </summary>
    void Awake()
    {
        graphics.SetActive(false);
    }
}
