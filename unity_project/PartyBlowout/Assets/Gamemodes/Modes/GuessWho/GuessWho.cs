using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GuessWho : Gamemode
{

    public static int[] AlivePlayers = {0,0,0}; // BLUE, RED, ALONE

    // RED => SEEKERS
    // BLUE => HIDING

    // Start is called before the first frame update

    protected void Awake()
    {
        AlivePlayers = new[] {0, 0, 0};
        CurGamemode = CurrentGamemode.GuessWho;
        CanRespawn = false;
        RedRatio = 0.3;
        onPlayerDeathHook += UpdatePlayerTeam;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (time > 0) return;
        if(!IsEnded && AlivePlayers[0] > 0)
            onRoundEnded(PlayerTeam.Blue);
    }

    protected override void OnDestroy()
    {
        onPlayerDeathHook -= UpdatePlayerTeam;
        base.OnDestroy();
    }

    private void UpdatePlayerTeam(AliveEntity a, object o)
    {
        if (IsEnded || !(a is Player p) || p is Spectator) return;
        AlivePlayers[(int) p.playerTeam]--;
        if (AlivePlayers[(int) p.playerTeam] > 0) return;
        onRoundEnded(p.playerTeam == PlayerTeam.Blue ? PlayerTeam.Red : PlayerTeam.Blue);
    }

    void Update()
    {

    }

}
