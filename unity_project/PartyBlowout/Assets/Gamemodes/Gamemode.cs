using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PhotonPlayer = Photon.Realtime.Player;
using Random = UnityEngine.Random;

public abstract class Gamemode : MonoBehaviourPunCallbacks
{

    public enum CurrentGamemode : uint
    {
        Race = 0,
        Shooter = 1,
        GuessWho = 2
    }

    public enum PlayerTeam : uint
    {
        Blue = 0,
        Red = 1,
        Alone = 2 // For Racing gamemode
    }

    public float timeLimit; //Seconds
    private float timeLimitSync = 1;
    private float _timeLeftBeforeSync;
    public static CurrentGamemode? CurGamemode = null;
    public static bool CanRespawn = true;
    public static int[] alivePlayers = {0, 0, 0}; // BLUE, RED, ALONE
    protected static List<PhotonPlayer> PlayersList;
    private PhotonView _photonView;
    [SerializeField] protected static double RedRatio = 0.5;


    protected virtual void Start()
    {
        _timeLeftBeforeSync = timeLimitSync;
        _photonView = GetComponent<PhotonView>();
        PlayersList = new List<PhotonPlayer>();
        foreach (KeyValuePair<int, PhotonPlayer> p in PhotonNetwork.CurrentRoom.Players) PlayersList.Add(p.Value);
        if(!PhotonNetwork.IsMasterClient) return;
        CreateTeams();
    }

    protected void FixedUpdate()
    {
        if(!_photonView.IsMine) return;
        timeLimit -= Time.fixedDeltaTime;
        _timeLeftBeforeSync -= Time.fixedDeltaTime;
        if(_photonView.IsMine && _timeLeftBeforeSync < 0)
            _photonView.RPC("RPC_PleaseSyncTime", RpcTarget.All, timeLimit);
    }

    [PunRPC]
    protected void RPC_PleaseSyncTime(float time)
    {
        timeLimit = time;
        _timeLeftBeforeSync = timeLimitSync;
        // DebugTools.PrintOnGUI($"Time left is {timeLimit}", DebugTools.LogType.LOG);

    }

    public void SetRandomGamemode()
    {
        CurrentGamemode? newGamemode = null;
        while (newGamemode == null || CurGamemode == newGamemode)
            newGamemode = (CurrentGamemode) Random.Range(0, 2);
        CurGamemode = newGamemode;
    }

    private static void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private static void CreateTeams()
    {
        bool alone = Math.Abs(RedRatio - 1) < 0.01 || RedRatio == 0;
        int redToFill = 0;
        if (!alone)
        {
            redToFill = (int)Math.Round(PlayersList.Count * RedRatio);
            ShuffleList(PlayersList);
        }
        foreach (PhotonPlayer p in PlayersList)
        {
            Hashtable h = new Hashtable();
            if (alone)
                h["team"] = (int) PlayerTeam.Alone;
            else if (redToFill > 0)
            {
                h["team"] = (int) PlayerTeam.Red;
                redToFill--;
            }
            else
                h["team"] = (int) PlayerTeam.Blue;
            p.SetCustomProperties(h);
        }
    }

    //TODO: Add arguments and documentation for each events
    public static event Action onRoundEndedHook;
    public static void onRoundEnded() { onRoundEndedHook?.Invoke(); }

    public static event Action onRoundStartedHook;
    public static void onRoundStarted() {onRoundStartedHook?.Invoke();}

    public static event Action onGamemodeEndedHook;
    public void onGamemodended() { onRoundEndedHook?.Invoke(); }

    public static event Action onGamemodeStartedHook;
    public static void onGamemodeStarted() {onRoundStartedHook?.Invoke();}

    public static event Action<AliveEntity, object, float> onTakeDamageHook;
    public static void onTakeDamage(AliveEntity victim, object origin, float dmg) {onTakeDamageHook?.Invoke(victim, origin, dmg);}

    public static event Action<AliveEntity, object> onPlayerDeathHook;
    public static void onPlayerDeath(AliveEntity victim, object origin) {onPlayerDeathHook?.Invoke(victim, origin);}

    public static event Action onPlayerSpawnHook;
    public static void onPlayerSpawn() {onPlayerSpawnHook?.Invoke();}

}