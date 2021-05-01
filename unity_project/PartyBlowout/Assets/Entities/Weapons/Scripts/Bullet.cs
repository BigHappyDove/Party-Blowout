using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour, IPunInstantiateMagicCallback
{
    private Rigidbody _rb;
    private PhotonView _pv;
    private int _damage = 0;
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
        // For an unknown reason, we shouldn't use _pv.isMine on this function, it works better withtout it
        object[] args = info.photonView.InstantiationData;
        if (args[0] is Vector3 v && args[1] is float f)
            _rb.AddRelativeForce(Vector3.forward * f);
        if (args[2] is int damage_arg)
            _damage = damage_arg;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!_pv.IsMine) return;
        AliveEntity target = other.gameObject.GetComponent<AliveEntity>();
        if (target != null)
            target.TakeDamage(_damage, GetComponentInParent<Player>());
        _pv.RPC("RPC_DestroyBullet", RpcTarget.All); // We could use PhotonNetwork.Destroy(..) but it doesn't work
    }

    [PunRPC]
    void RPC_DestroyBullet()
    {
        if(this != null && gameObject != null)
            Destroy(gameObject);
    }
}
