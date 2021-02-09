using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class movementTest : MonoBehaviour
{
    private CharacterController _CC;
    private PhotonView _PV;

    private void Start()
    {
        _CC = GetComponent<CharacterController>();
        _PV = GetComponent<PhotonView>();
    }

    private void Movements()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _CC.Move(transform.right * Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _CC.Move(-transform.right * Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            _CC.Move(transform.forward * Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _CC.Move(-transform.forward * Time.deltaTime * 5);
        }

    }

    private void FixedUpdate()
    {
        if(_PV.IsMine)
        {
            Movements();
        }
    }
}
