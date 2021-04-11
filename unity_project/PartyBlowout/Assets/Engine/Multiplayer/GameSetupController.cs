using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using NUnit.Framework;


public class GameSetupController : MonoBehaviour
{
    public List<EntitiesManager.EntityObjects> entitesToCreate = new List<EntitiesManager.EntityObjects>();
    // entitesToCreate.Add(new EntitiesManager.EntityObjects(
    // {
    //     path = "abc/aaaa",
    //     pos = new Vector3(0,0,0),
    //     rotation = new Quaternion(0,0,0)
    //
    // }));

    void Start() => EntitiesManager.CreateEntity(entitesToCreate);
}

