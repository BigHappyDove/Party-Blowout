using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWho : Gamemode
{

    public static int[] AlivePlayers = {0,0,0}; // BLUE, RED, ALONE

    // RED => SEEKERS
    // BLUE => HIDING

    // Start is called before the first frame update

    protected void Awake()
    {
        CurGamemode = CurrentGamemode.GuessWho;
        CanRespawn = false;
        RedRatio = 0.3;
        onPlayerDeathHook += UpdatePlayerTeam;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onPlayerDeathHook -= UpdatePlayerTeam;
    }

    private void UpdatePlayerTeam(AliveEntity a, object o)
    {
        if (IsEnded || !(a is Player p) || p is Spectator) return;
        AlivePlayers[(int) p.playerTeam]--;
        if (AlivePlayers[(int) p.playerTeam] > 0) return;
        onRoundEnded(p.playerTeam == PlayerTeam.Blue ? PlayerTeam.Red : PlayerTeam.Blue);
        IsEnded = true;
    }

    void Update()
    {

    }

}
