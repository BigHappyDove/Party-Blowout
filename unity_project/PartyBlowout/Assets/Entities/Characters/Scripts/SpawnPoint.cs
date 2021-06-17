using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    private PhotonView _photonView;


    /// <summary>
    /// Hide visuals of the spawn points
    /// </summary>
    void Awake()
    {
        if(graphics)
            graphics.SetActive(false);
    }


    public bool IsSomeoneInRange(float range)
    {
        DebugTools.PrintOnGUI(FindObjectsOfType<Player>().Length);
        DebugTools.PrintOnGUI(GameObject.FindGameObjectsWithTag("Player").Length);
        return GameObject.FindGameObjectsWithTag("Player").Any(g =>
            Vector3.Distance(g.transform.position, transform.position) <= range);
    }
}
