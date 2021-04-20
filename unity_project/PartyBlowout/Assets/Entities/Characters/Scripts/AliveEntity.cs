﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    protected PhotonView PV;
    protected Rigidbody rb;
    public float health = 100;
    private object originDamage = null;
    [System.NonSerialized] public SpawnEntity spawnEntity;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    /// <summary>
    /// set health regarding the damage taken.
    /// </summary>
    /// <param name="amount"> damage the target receive</param>
    /// <param name="origin">from what / who</param>
    public void TakeDamage(float amount, AliveEntity origin)
    {
        originDamage = origin;
        PV.RPC("RPC_TakeDamage", RpcTarget.All, amount);
    }

    [PunRPC]
    public void RPC_TakeDamage(float amount)
    {
        health -= amount;
        Gamemode.onTakeDamage(this, originDamage);
        if (health <= 0f)
        {
            Gamemode.onPlayerDeath(this, originDamage);
            if(PV.IsMine)
            {
                if (spawnEntity != null)
                    spawnEntity.RespawnController(gameObject);
                else
                    PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
