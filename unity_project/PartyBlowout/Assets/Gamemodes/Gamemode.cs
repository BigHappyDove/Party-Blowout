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
    public event Action onRoundEndedHook;
    public void onRoundEnded() { onRoundEndedHook?.Invoke(); }

    public event Action onRoundStartedHook;
    public void onRoundStarted() {onRoundStartedHook?.Invoke();}

    public event Action onGamemodeEndedHook;
    public void onGamemodended() { onRoundEndedHook?.Invoke(); }

    public event Action onGamemodeStartedHook;
    public void onGamemodeStarted() {onRoundStartedHook?.Invoke();}

    public event Action onTakeDamageHook;
    public void onTakeDamageDeath() {onPlayerDeathHook?.Invoke();}

    public event Action onPlayerDeathHook;
    public void onPlayerDeath() {onPlayerDeathHook?.Invoke();}

    public event Action onPlayerSpawnHook;
    public void onPlayerSpawn() {onPlayerSpawnHook?.Invoke();}

}