using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            InstantiateCar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateCar()
    {
        PhotonNetwork.Instantiate(Path.Combine("Entities", "car_root"), Vector3.zero, Quaternion.identity);
    }
}
