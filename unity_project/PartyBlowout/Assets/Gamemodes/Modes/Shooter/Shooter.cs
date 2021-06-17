using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : Gamemode
{
    public static int[] Score = {0,0}; // BLUE, RED
    public static int ScoreLimit = 10;

    protected void Awake()
    {
        CurGamemode = CurrentGamemode.Shooter;
        RedRatio = 0.5;
        onPlayerDeathHook += UpdateScore;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (time > 0) return;
        if(!IsEnded)
            onRoundEnded((PlayerTeam) Array.IndexOf(Score, Score.Max()));
    }

    protected override void OnDestroy()
    {
        onPlayerDeathHook -= UpdateScore;
        Score = new[] {0, 0};
        base.OnDestroy();
    }

    private void UpdateScore(AliveEntity victim, object origin)
    {
        if (IsEnded || !(victim is Player p) || p is Spectator) return;
        int index = p.playerTeam == PlayerTeam.Blue ? 1 : 0;
        Score[index]++;
        if (Score[index] < ScoreLimit) return;
        onRoundEnded((PlayerTeam) index);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
