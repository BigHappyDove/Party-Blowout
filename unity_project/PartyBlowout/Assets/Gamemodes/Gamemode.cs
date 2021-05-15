using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PhotonPlayer = Photon.Realtime.Player;

public abstract class Gamemode : MonoBehaviour
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

    public CurrentGamemode gamemodeToLaunch;
    public int timeLimit; //Seconds
    protected List<PhotonPlayer> PlayersList;
    protected List<PhotonPlayer> AlivePlayersList;
    private Dictionary<CurrentGamemode, Func<int>> gamemodesStartup = new Dictionary<CurrentGamemode, Func<int>>
    {
        {CurrentGamemode.Race, Race.LoadGamemode},
        {CurrentGamemode.Shooter, Shooter.LoadGamemode},
        {CurrentGamemode.GuessWho, GuessWho.LoadGamemode}
    };

    private void Start()
    {
        foreach (KeyValuePair<int, PhotonPlayer> p in PhotonNetwork.CurrentRoom.Players) PlayersList.Add(p.Value);
        gamemodesStartup[gamemodeToLaunch]();
    }


    private void ShuffleList<T>(List<T> list)
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

    protected void CreateTeams(bool alone, double propToBeRed = 0)
    {
        int redToFill = 0;
        if (alone)
        {
            redToFill = (int)Math.Round(PlayersList.Count * propToBeRed);
            ShuffleList(PlayersList);
        }
        foreach (PhotonPlayer p in PlayersList)
        {
            Hashtable h = new Hashtable();
            if (alone)
                h["team"] = PlayerTeam.Alone;
            else if (redToFill > 0)
            {
                h["team"] = PlayerTeam.Red;
                redToFill--;
            }
            else
                h["team"] = PlayerTeam.Blue;
            p.CustomProperties = h;
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