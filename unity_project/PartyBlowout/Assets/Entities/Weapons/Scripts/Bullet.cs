using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour, IPunInstantiateMagicCallback
{
    private Rigidbody _rb;
    private PhotonView _pv;
    private float timeAlive = 5;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pv = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if(!_pv.IsMine) return;
        timeAlive -= Time.fixedDeltaTime;
        if(timeAlive <= 0) PhotonNetwork.Destroy(gameObject);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if(!_pv.IsMine) return;
        object[] args = info.photonView.InstantiationData;
        if (args[0] is Vector3 v && args[1] is float f)
        {
            _rb.AddRelativeForce(Vector3.forward * f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!_pv.IsMine) return;
        PhotonNetwork.Destroy(gameObject);
    }
}
