using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;


public class EntitiesManager : MonoBehaviour
{
    [System.Serializable]
    public struct EntityObjects
    {
        public string path;
        public Vector3 pos;
        public Quaternion rotation;
    }
    // EntityObject abc = new EntitiesManager.EntityObjects(
    // {
    //     path = "abc/aaaa",
    //     pos = new Vector3(0,0,0),
    //     rotation = new Quaternion(0,0,0)
    //
    // }));
    // OR YOU CAN INITIALIZE THIS STRUCT FROM THE CONTEXT MENU OF UNITY WITH A SERIALIZEFIELD


    /// <summary>
    /// Instantiate multiplayer objects from a list of EntitiesObjects structs
    /// </summary>
    /// <param name="objs">A list EntitiesObjects structs (example of constructor in EntitiesManager.cs)</param>
    public static void CreateEntity(List<EntityObjects> objs)
    {
        foreach (var obj in objs)
            PhotonNetwork.Instantiate(obj.path, obj.pos, obj.rotation);
    }

    /// <summary>
    /// Instantiate a multiplayer object from a EntitiesObjects struct
    /// </summary>
    /// <param name="obj">An EntitiesObjects struct (example of constructor in EntitiesManager.cs)</param>
    public static void CreateEntity(EntityObjects obj)
    {
        PhotonNetwork.Instantiate(obj.path, obj.pos, obj.rotation);
    }
}
