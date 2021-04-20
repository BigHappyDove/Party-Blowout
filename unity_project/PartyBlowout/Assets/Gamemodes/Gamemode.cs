using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gamemode : MonoBehaviour
{

    public int timeLimit; //Seconds
    private List<Player> _playersList;
    private List<Player> _alivePlayersList;
    // private

    public abstract void LoadGamemode();


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