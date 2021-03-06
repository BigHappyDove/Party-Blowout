﻿using System;
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
    public WeaponBase origin;

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
        if (args[0] is float f)
            _rb.AddRelativeForce(Vector3.forward * f);
        if (args[1] is int damage_arg)
            _damage = damage_arg;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!_pv.IsMine) return;
        AliveEntity target = other.gameObject.GetComponent<AliveEntity>();
        if (target != null)
        {
            Player shooter = origin != null ? origin.GetComponentInParent<Player>() : null;
            Gamemode.PlayerTeam? teamSource = AliveEntity.GetTeam(_pv);
            Gamemode.PlayerTeam? teamTarget = AliveEntity.GetTeam(target.PV);
            if (target is AgentScript || teamTarget == null || teamSource != teamTarget)
                target.TakeDamage(_damage, shooter);
            if (target is AgentScript && shooter != null && Gamemode.CurGamemode == Gamemode.CurrentGamemode.GuessWho)
                shooter.TakeDamage(10f, null);
        }
        _pv.RPC("RPC_DestroyBullet", RpcTarget.All); // We could use PhotonNetwork.Destroy(..) but it doesn't work
    }

    [PunRPC]
    void RPC_DestroyBullet()
    {
        if(this != null && gameObject != null)
            Destroy(gameObject);
    }
}
