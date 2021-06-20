using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : Gamemode
{
    // Start is called before the first frame update
    protected override void Start()
    {
        CurGamemode = CurrentGamemode.Race;
        RedRatio = 0;
        CanRespawn = true;
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (time > 0) return;
        if(!IsEnded)
            onRoundEnded(PlayerTeam.Alone);
    }

}
