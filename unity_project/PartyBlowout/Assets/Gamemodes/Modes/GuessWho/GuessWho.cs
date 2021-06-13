using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWho : Gamemode
{

    public static int[] alivePlayers = {0, 0, 0}; // BLUE, RED, ALONE

    // RED => SEEKERS
    // BLUE => HIDING

    // Start is called before the first frame update
    protected override void Start()
    {
        CurGamemode = CurrentGamemode.GuessWho;
        CanRespawn = false;
        RedRatio = 0.3;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
