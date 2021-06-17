using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnEntity : MonoBehaviour
{
    public List<string> paths;
    public SpawnManager _spawnManager;
    public bool doSpawnOnStart = true;

    void Start()
    {
        if (_spawnManager == null)
        {
            DebugTools.PrintOnGUI("SpawnEntity: Initiated without a SpawnManager object! Looking for the component in the object.", DebugTools.LogType.WARNING);
            _spawnManager = GetComponent<SpawnManager>();
            if (_spawnManager == null)
                DebugTools.PrintOnGUI("SpawnEntity: Couldn't find any SpawnManager component! Entities will not be created!", DebugTools.LogType.ERROR);
        }

        if(doSpawnOnStart && _spawnManager != null)
            CreateController();
    }

    public GameObject CreateController(object[] args = null)
    {
        string rndPath = paths[Random.Range(0, paths.Count)];
        GameObject g = EntitiesManager.CreateEntity(rndPath, _spawnManager.GetSpawnpoint(), args);
        AliveEntity aliveEntity = g.GetComponent<AliveEntity>();
        if(aliveEntity != null)
            aliveEntity.spawnEntity = this;
        return g;
    }

    public void RespawnController(GameObject gameObject, object[] args = null)
    {
        PhotonNetwork.Destroy(gameObject);
        CreateController(args);
    }
}
