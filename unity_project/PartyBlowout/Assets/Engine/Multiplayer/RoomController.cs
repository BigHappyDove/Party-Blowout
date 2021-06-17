using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public static void StaticLoadScene(int indexSceneToLoad)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(indexSceneToLoad);
        }
    }

    public void LoadScene(int indexSceneToLoad) => StaticLoadScene(indexSceneToLoad);

    public void LoadScene() => StaticLoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
}
