using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgentScript : AliveEntity
{
    private Vector3 _target;
    public float maxDist;
    public NavMeshAgent agent;
    private CheckPointsAI _lastCheckpoint;

    private void Start()
    {
        DebugTools.PrintOnGUI(PV.IsMine);
        if(!PV.IsMine) return;
        _target = GetRandomPosOnNavMesh();
    }

    /// <summary>
    /// Generate a random position on the NavMesh
    /// </summary>
    /// <returns>A Vector3 position on the NavMesh</returns>
    private Vector3 GetRandomPosOnNavMesh()
    {
        Vector2 circleRand = Random.insideUnitCircle * maxDist;
        Vector3 randPos = agent.transform.position + new Vector3(circleRand.x, 0, circleRand.y);
        NavMesh.SamplePosition(randPos, out NavMeshHit hit, maxDist, 1);
        return hit.position;
    }

    public void UpdatePath(List<CheckPointsAI> checkPoints, CheckPointsAI curCheckpoint)
    {
        if(!PV.IsMine) return;
        CheckPointsAI checkpoint = null;
        // while (checkpoint == null || checkpoint != _lastCheckpoint)
        while (checkpoint == null)
            checkpoint = checkPoints[Random.Range(0, checkPoints.Count)];
        _lastCheckpoint = curCheckpoint;
        Bounds cpBounds = checkpoint.GetComponent<Collider>().bounds;
        _target = _randomPosVector3(cpBounds.min, cpBounds.max);
    }

    private Vector3 _randomPosVector3(Vector3 min, Vector3 max)
        => new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));

    private void FixedUpdate()
    {
        if(!PV.IsMine) return;
        agent.SetDestination(_target);
    }
}
