using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!_pv.IsMine || _linkedCheckPointsList.Count <= 0) return;
        AgentScript agentScript = collision.gameObject.GetComponent<AgentScript>();
        if (agentScript != null)
            agentScript.UpdatePath(_linkedCheckPointsList, this);
    }
}
