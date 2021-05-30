using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class AliveEntity : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    protected Rigidbody rb;
    public float health = 100;
    private object originDamage = null;
    [System.NonSerialized] public SpawnEntity spawnEntity;



    protected virtual void Awake()
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

    public static Gamemode.PlayerTeam? GetTeam(PhotonView PV)
    {
        if (PV == null || PV.Owner == null) return null;
        Hashtable playerProperties = PV.Owner.CustomProperties;
        if (playerProperties.ContainsKey("team") && playerProperties["team"] is int team)
            return (Gamemode.PlayerTeam) team;
        return null;
    }

    [PunRPC]
    public void RPC_TakeDamage(float amount)
    {
        health -= amount;
        Gamemode.onTakeDamage(this, originDamage, amount);
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
