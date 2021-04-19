using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    protected PhotonView PV;
    protected Rigidbody rb;
    public float health = 100;
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
    public void TakeDamage(float amount)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, amount);
    }

    [PunRPC]
    public void RPC_TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f && PV.IsMine)
        {
            if(spawnEntity != null)
                spawnEntity.RespawnController(gameObject);
            else
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
