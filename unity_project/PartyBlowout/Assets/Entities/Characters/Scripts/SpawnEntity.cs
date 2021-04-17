using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnEntity : MonoBehaviour
{
    public string path;
    public SpawnManager _spawnManager;

    void Start()
    {
        if (_spawnManager == null)
        {
            DebugTools.PrintOnGUI("SpawnEntity: Initiated without a SpawnManager object! Looking for the component in the object.", DebugTools.LogType.WARNING);
            _spawnManager = GetComponent<SpawnManager>();
            if (_spawnManager == null)
                DebugTools.PrintOnGUI("SpawnEntity: Couldn't find any SpawnManager component! Entities will not be created!", DebugTools.LogType.ERROR);
        }

        if(_spawnManager != null)
            CreateController();
    }

    void CreateController()
    {
        EntitiesManager.CreateEntity(path, _spawnManager.GetSpawnpoint());
        // PhotonNetwork.Instantiate(Path.Combine("Entities", "Player/Player"), spawnpoint.position, spawnpoint.rotation);
    }
}
