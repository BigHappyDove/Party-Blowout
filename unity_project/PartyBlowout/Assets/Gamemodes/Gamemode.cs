using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using NUnit.Framework.Constraints;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] private float timeLimit; //Seconds
    public static float time;

    private float timeLimitSync = 1;
    private float _timeLeftBeforeSync;
    private PhotonView _photonView;
    private float _timeBeforeNewRound = 10;
    public static CurrentGamemode? CurGamemode = null;
    public static bool CanRespawn = true;
    protected static List<PhotonPlayer> PlayersList;
    public static bool IsEnded;
    [SerializeField] private TransitionManager _tm;
    [SerializeField] protected static double RedRatio = 0.5;


    protected virtual void Start()
    {
        IsEnded = false;
        time = timeLimit;
        _timeLeftBeforeSync = timeLimitSync;
        _photonView = GetComponent<PhotonView>();
        PlayersList = new List<PhotonPlayer>();
        foreach (KeyValuePair<int, PhotonPlayer> p in PhotonNetwork.CurrentRoom.Players) PlayersList.Add(p.Value);
        if (!PhotonNetwork.IsMasterClient) return;
        CreateTeams();
        onRoundEndedHook += StartCountdownNewRound;
    }

    protected virtual void OnDestroy()
    {
        if(!PhotonNetwork.IsMasterClient) return;
        onRoundEndedHook -= StartCountdownNewRound;
    }

    protected virtual void FixedUpdate()
    {
        if(!_photonView.IsMine) return;
        time -= Time.fixedDeltaTime;
        _timeLeftBeforeSync -= Time.fixedDeltaTime;
        if(_photonView.IsMine && _timeLeftBeforeSync < 0)
            _photonView.RPC("RPC_PleaseSyncTime", RpcTarget.All, time);
    }

    [PunRPC]
    protected void RPC_PleaseSyncTime(float t)
    {
        time = t;
        _timeLeftBeforeSync = timeLimitSync;
        // DebugTools.PrintOnGUI($"Time left is {timeLimit}", DebugTools.LogType.LOG);

    }

    private void StartCountdownNewRound(PlayerTeam pt, PhotonView photonView)
    {
        StartCoroutine(NewRound());
    }

    private IEnumerator NewRound()
    {
        if(!PhotonNetwork.IsMasterClient) yield break;
        int index = GetIndexNextScene();
        if(index != 0)
            _tm.PlaySelected((CurrentGamemode) (index-1));
        yield return new WaitForSeconds(_timeBeforeNewRound);
        ChangeScene(index);
    }

    private int GetIndexNextScene()
    {
        if(SceneManager.sceneCountInBuildSettings - 1 == 1) return 0;
        int index;
        do
            index = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        while ( index == SceneManager.GetActiveScene().buildIndex);
        return index;
    }

    private void ChangeScene(int index)
    {
        if(index == 0) return;
        RoomController.StaticLoadScene(index);
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

    private static void CreateTeams(bool forceAlone = false)
    {
        bool alone = Math.Abs(RedRatio - 1) < 0.01 || RedRatio == 0;
        int redToFill = 0;
        if (!alone && !forceAlone)
        {
            redToFill = (int)Math.Round(PlayersList.Count * RedRatio);
            ShuffleList(PlayersList);
        }
        foreach (PhotonPlayer p in PlayersList)
        {
            Hashtable h = new Hashtable();
            if (alone || forceAlone)
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
    public static event Action<PlayerTeam, PhotonView> onRoundEndedHook;

    public static void onRoundEnded(PlayerTeam pt, PhotonView pv = null)
    {
        IsEnded = true;
        onRoundEndedHook?.Invoke(pt, pv);
        CreateTeams(true);
    }

    public static event Action<AliveEntity, object, float> onTakeDamageHook;
    public static void onTakeDamage(AliveEntity victim, object origin, float dmg) {onTakeDamageHook?.Invoke(victim, origin, dmg);}

    public static event Action<AliveEntity, object> onPlayerDeathHook;
    public static void onPlayerDeath(AliveEntity victim, object origin) {onPlayerDeathHook?.Invoke(victim, origin);}

    public static event Action onPlayerSpawnHook;
    public static void onPlayerSpawn() {onPlayerSpawnHook?.Invoke();}

}