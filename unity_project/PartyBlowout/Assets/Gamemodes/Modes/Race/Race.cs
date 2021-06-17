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
        base.Start();
    }

}
