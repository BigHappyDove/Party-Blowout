using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Gamemode
{
    public static int[] Score; // BLUE, RED
    public static int ScoreLimit = 10;

    protected void Awake()
    {
        Score = new[] {0, 0};
        CurGamemode = CurrentGamemode.Shooter;
        RedRatio = 0.5;
        onPlayerDeathHook += UpdateScore;
    }

    protected override void Start()
    {
        base.Start();
    }


    protected override void OnDestroy() => onPlayerDeathHook -= UpdateScore;

    private void UpdateScore(AliveEntity victim, object origin)
    {
        if (IsEnded || !(victim is Player p) || p is Spectator) return;
        int index = p.playerTeam == PlayerTeam.Blue ? 1 : 0;
        Score[index]++;
        if (Score[index] < ScoreLimit) return;
        IsEnded = true;
        onRoundEnded((PlayerTeam) index);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
