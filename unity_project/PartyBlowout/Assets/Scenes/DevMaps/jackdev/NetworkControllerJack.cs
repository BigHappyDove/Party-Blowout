using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//Documentation: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
//Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html

public class NetworkControllerJack : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon master servers
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Now connected to the " + PhotonNetwork.CloudRegion + " server !");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
