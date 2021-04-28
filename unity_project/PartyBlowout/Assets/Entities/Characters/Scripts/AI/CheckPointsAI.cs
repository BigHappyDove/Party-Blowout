using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckPointsAI : MonoBehaviour
{
    private PhotonView _pv;
    public List<CheckPointsAI> _linkedCheckPointsList;

    void Start()
    {
        if (_linkedCheckPointsList.Count <= 1)
        {
            string errorMsg = $"({name}, {transform.position}): Waiting for 2 or more checkpoints in _linkCheckPointsList, received {_linkedCheckPointsList.Count}";
            DebugTools.PrintOnGUI(errorMsg, DebugTools.LogType.WARNING);
        }

        _pv = GetComponent<PhotonView>();
        foreach (CheckPointsAI c in _linkedCheckPointsList)
            Debug.DrawLine(transform.position, c.transform.position, Color.red, 900 ,false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!_pv.IsMine || _linkedCheckPointsList.Count <= 0) return;
        AgentScript agentScript = col.gameObject.GetComponent<AgentScript>();
        if (agentScript != null) agentScript.UpdatePath(_linkedCheckPointsList, this);
    }

}
