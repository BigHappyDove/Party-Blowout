using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoard : AliveEntity
{
    private string lastSound;


    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) 
            return;

        if (Gamemode.CurGamemode == Gamemode.CurrentGamemode.GuessWho)
        {
            if (Input.GetKeyDown("p"))
            {
                _audioManager.Stop(lastSound);
                _audioManager.Play("Nani");
                lastSound = "Nani";
            }
            
            if (Input.GetKeyDown("o"))
            {
                _audioManager.Stop(lastSound);
                _audioManager.Play("Deja-vu");
                lastSound = "Deja-vu";
            }
            
            if (Input.GetKeyDown("i"))
            {
                _audioManager.Stop(lastSound);
                _audioManager.Play("Hello");
                lastSound = "Hello";
            }
            
            if (Input.GetKeyDown("u"))
            {
                _audioManager.Stop(lastSound);
                _audioManager.Play("French");
                lastSound = "French";
            }
        }

    }
}
