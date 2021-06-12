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
    [SerializeField] private Material[] _materialsTeam = new Material[3];
    [SerializeField] private GameObject _spectatorPrefab;




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
                if (this is AgentScript a)
                {
                    a.Die();
                    return;
                }
                if (spawnEntity != null && Gamemode.CanRespawn)
                    spawnEntity.RespawnController(gameObject);
                else
                {
                    Transform rememberTransform = transform;
                    PhotonNetwork.Destroy(gameObject);
                    if (_spectatorPrefab)
                        Instantiate(_spectatorPrefab, rememberTransform.position, rememberTransform.rotation);
                }
            }
        }
    }

    protected void ApplyTeamMaterial()
    {
        foreach (Transform t in transform)
        {
            if(t.gameObject.name == "UserInfo") continue;
            Renderer r = t.gameObject.GetComponent<Renderer>();
            if (r == null) continue;
            Gamemode.PlayerTeam index = this is Player p ? p.playerTeam : 0;
            r.material = _materialsTeam[(int) index];
        }
    }
}
