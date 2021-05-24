using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWho : Gamemode
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _currentGamemode = CurrentGamemode.GuessWho;
        redRatio = 0.3;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
