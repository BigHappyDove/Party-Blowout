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
    /// <param name="customObj">Optional arguments to pass</param>
    public static void CreateEntity(List<EntityObjects> objs, object[] customObj = null)
    {
        foreach (var obj in objs)
            PhotonNetwork.Instantiate(obj.path, obj.pos, obj.rotation, 0, customObj);
    }

    /// <summary>
    /// Instantiate a multiplayer object from a EntitiesObjects struct
    /// </summary>
    /// <param name="obj">An EntitiesObjects struct (example of constructor in EntitiesManager.cs)</param>
    /// <param name="customObj">Optional arguments to pass</param>
    public static GameObject CreateEntity(EntityObjects obj, object[] customObj = null) =>
        PhotonNetwork.Instantiate(obj.path, obj.pos, obj.rotation, 0, customObj);

    /// <summary>
    /// Create an entity from its path and a transform object
    /// </summary>
    /// <param name="path">The path to the entity (relative to ressources/)</param>
    /// <param name="transform">Transform object</param>
    /// <param name="customObj">Optional arguments to pass</param>
    public static GameObject CreateEntity(string path, Transform transform, object[] customObj = null) =>
        PhotonNetwork.Instantiate(path, transform.position, transform.rotation, 0, customObj);

    /// <summary>
    /// Create an entity from its path and a pos / rotation values
    /// </summary>
    /// <param name="path">The path to the entity (relative to ressources/)</param>
    /// <param name="pos">A Vector3 object for the position</param>
    /// <param name="rot">A Quaternion object for the roatation</param>
    /// <param name="customObj">Optional arguments to pass</param>
    public static GameObject CreateEntity(string path, Vector3 pos, Quaternion rot, object[] customObj = null) =>
        PhotonNetwork.Instantiate(path, pos, rot, 0, customObj);
}
