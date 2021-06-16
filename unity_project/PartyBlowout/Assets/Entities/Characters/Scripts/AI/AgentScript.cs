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
    public float maxDist;
    private Vector3 _target;
    private Animator animator;
    private NavMeshAgent agent;
    private CheckPointsAI _lastCheckpoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if(!PV.IsMine) return;
        agent.speed = sprintSpeed;
        InvokeRepeating(nameof(RandomMaxSpeed), 1, 1.5f);
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

    internal void Die(bool respawn = true)
    {
        PhotonNetwork.Destroy(gameObject);
        if(!respawn) return;
        EntitiesManager.CreateEntity("Entities/AI/Agent", GetRandomPosOnNavMesh(), Quaternion.identity);
    }

    /// <summary>
    /// Set randomly the agent.speed to walkSpeed or sprintSpeed
    /// </summary>
    private void RandomMaxSpeed()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                agent.speed = walkSpeed;
                return;
            case 1:
                agent.speed = sprintSpeed;
                return;
        }
    }

    /// <summary>
    /// Update the path of the agent for a new checkpoint.
    /// </summary>
    /// <param name="checkPoints">The checkpoints where the ai can go</param>
    /// <param name="curCheckpoint">The checkpoint he's currently on</param>
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

    /// <summary>
    /// Get a random Vector3 obj between two Vector3
    /// </summary>
    /// <param name="min">The minimum Vector3</param>
    /// <param name="max">The maximum Vector3</param>
    /// <returns>The random Vector3</returns>
    private Vector3 _randomPosVector3(Vector3 min, Vector3 max)
        => new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));

    private void Update()
    {
        if (!PV.IsMine)
            return;
        if(!agent.hasPath)
            _target = GetRandomPosOnNavMesh();


        float velocity = agent.velocity.magnitude;
        bool isRunning = velocity >= sprintSpeed * 0.9;
        bool isWalking = velocity >= walkSpeed * 0.1;

        switch (animator.GetBool("isWalking"))
        {
            case false when isWalking:
                animator.SetBool("isWalking", true);
                _audioManager.Play("Walk");
                break;
            case true when !isWalking:
                animator.SetBool("isWalking", false);
                _audioManager.Stop("Walk");
                break;
        }


        switch (animator.GetBool("isRunning"))
        {
            case false when isRunning && isWalking:
                animator.SetBool("isRunning", true);
                break;
            case true when !isRunning || !isWalking:
                animator.SetBool("isRunning", false);
                break;
        }
    }

    private void FixedUpdate()
    {
        if(!PV.IsMine) return;
        agent.SetDestination(_target);
    }
}
