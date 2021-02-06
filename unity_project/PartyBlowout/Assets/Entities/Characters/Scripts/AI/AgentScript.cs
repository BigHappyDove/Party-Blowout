using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

//TODO SYNC MOVEMENTS FOR ALL PLAYERS
public class AgentScript : MonoBehaviour
{
    private Vector3 _target;
    public float maxDist;
    public NavMeshAgent agent;
    public PhotonView PV;

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

    void Update()
    {
        if (!PV.IsMine) return;
        if (agent.velocity.magnitude == 0 || !float.IsPositiveInfinity(agent.remainingDistance) &&
            agent.pathStatus == NavMeshPathStatus.PathComplete &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            _target = GetRandomPosOnNavMesh();
        }
        agent.SetDestination(_target);
    }
}
