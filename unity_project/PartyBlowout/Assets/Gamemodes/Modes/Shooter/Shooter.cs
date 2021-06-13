using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Gamemode
{
    public static readonly int[] Score = {0, 0}; // BLUE, RED

    protected override void Start()
    {
        CurGamemode = CurrentGamemode.Shooter;
        RedRatio = 0.5;
        onPlayerDeathHook += updateScore;
        base.Start();
    }

    private void OnDestroy() => onPlayerDeathHook -= updateScore;

    private void updateScore(AliveEntity victim, object origin)
    {
        if (!(victim is Player p)) return;
        int index = p.playerTeam == PlayerTeam.Blue ? 1 : 0;
        Score[index]++;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
