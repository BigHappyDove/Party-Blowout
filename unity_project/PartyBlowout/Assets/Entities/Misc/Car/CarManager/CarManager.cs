using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class CarManager : MonoBehaviour
{

    [NonSerialized] public List<SimpleCarController> listCars = new List<SimpleCarController>();
    public CheckpointsManager checkpointsManager;
    public SpawnEntity _spawnEntity;


    // Start is called before the first frame update
    void Start()
    {
        InstantiateCar();
    }

    void InstantiateCar()
    {
        if (checkpointsManager == null)
            throw new ArgumentException("Couldn't find checkpointsManager / start checkpoint");

        GameObject g = _spawnEntity.CreateController();
        DebugTools.PrintOnGUI(g != null);

        if (g != null)
        {
            listCars.Add(g.GetComponent<SimpleCarController>());
        }
        g.GetComponentInChildren<CheckpointTracker>().checkpointsManager = checkpointsManager;
    }

    //Regarder le systeme de spawn de titouan pour modif les strings de pathspour varier les voiture.
}
