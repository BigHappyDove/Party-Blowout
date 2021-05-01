using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour, IPunInstantiateMagicCallback
{
    private Rigidbody _rb;
    private PhotonView _pv;
    private float time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pv = GetComponent<PhotonView>();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] args = info.photonView.InstantiationData;
        if (args[0] is Vector3 v && args[1] is float f)
        {
            _rb.AddRelativeForce(Vector3.forward * f);
        }
    }

    private void OnCollisionEnter(Collision other) => PhotonNetwork.Destroy(gameObject);
}
