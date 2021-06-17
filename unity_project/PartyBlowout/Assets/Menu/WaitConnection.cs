using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WaitConnection : MonoBehaviour
{
    public GameObject connectingText;
    public GameObject playMenu;

    private void OnEnable()
    {
        connectingText.SetActive(true);
        playMenu.SetActive(false);
    }

    private void Update()
    {
        ShowUIConnection();
    }

    void ShowUIConnection()
    {
        connectingText.SetActive(!PhotonNetwork.IsConnectedAndReady);
        playMenu.SetActive(PhotonNetwork.IsConnectedAndReady);
    }
}
