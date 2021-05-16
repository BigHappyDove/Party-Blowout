using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PhotonPlayer = Photon.Realtime.Player;

public abstract class Gamemode : MonoBehaviourPunCallbacks
{

    public enum CurrentGamemode
    {
        Race,
        Shooter,
        GuessWho
    }

    public enum PlayerTeam : uint
    {
        Blue = 0,
        Red = 1,
        Alone = 2 // For Racing gamemode
    }

    public int timeLimit; //Seconds
    protected static List<PhotonPlayer> PlayersList;
    protected static List<PhotonPlayer> AlivePlayersList;
    [SerializeField] protected static double redRatio = 0.5;


    protected virtual void Start()
    {
        PlayersList = new List<PhotonPlayer>();
        foreach (KeyValuePair<int, PhotonPlayer> p in PhotonNetwork.CurrentRoom.Players) PlayersList.Add(p.Value);
        if(!PhotonNetwork.IsMasterClient) return;
        CreateTeams();
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
        bool alone = Math.Abs(redRatio - 1) < 0.01 || redRatio == 0;
        int redToFill = 0;
        if (!alone)
        {
            redToFill = (int)Math.Round(PlayersList.Count * redRatio);
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

    public static event Action<AliveEntity, object> onTakeDamageHook;
    public static void onTakeDamage(AliveEntity victim, object origin) {onTakeDamageHook?.Invoke(victim, origin);}

    public static event Action<AliveEntity, object> onPlayerDeathHook;
    public static void onPlayerDeath(AliveEntity victim, object origin) {onPlayerDeathHook?.Invoke(victim, origin);}

    public static event Action onPlayerSpawnHook;
    public static void onPlayerSpawn() {onPlayerSpawnHook?.Invoke();}

}