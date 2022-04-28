using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    [Header("Patrolling Variables")]
    public Transform[] _waypoints;
    private NavMeshAgent _agent;
    private int _waypointIndex;
    private int _currentWaypoint;
    private Vector3 _target;

    [Header("General Variables")]
    public Transform _player;
    public float _moveSpeed = 5f;
    public float _maxSightDistance = 20f;
    public float _minDistance = 7f;

    public bool isPatrolling; 
    public bool isChasing; 
    public bool isAttacking; 
    private float _DistanceToPlayer;

    private enum EnemyState
    {
        onPatrol,
        chasing,
        attack
    }

    private EnemyState _curState;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        _DistanceToPlayer = Vector3.Distance(transform.position, _player.position);

        UpdateState();

        switch (_curState)
        {
            case EnemyState.onPatrol:
                UpdatePatrol();
                break;

            case EnemyState.chasing:
                UpdateChasing();
                break;

            case EnemyState.attack:
                UpdateAttack();
                break;
        }
    }

    void UpdateState()
    {
        if (_DistanceToPlayer >= _maxSightDistance)
        {
            isPatrolling = true;
            isChasing = false;
            isAttacking = false;
            _curState = EnemyState.onPatrol;    
        }
        else if(_DistanceToPlayer <= _maxSightDistance && _DistanceToPlayer >= _minDistance)
        {
            isPatrolling = false;
            isChasing = true;
            isAttacking = false;
            _curState = EnemyState.chasing;
        }
        else if(_DistanceToPlayer <= _maxSightDistance && _DistanceToPlayer <= _minDistance)
        {
            isPatrolling = false;
            isChasing = false;
            isAttacking = false;
            _curState = EnemyState.attack;
        }
    }

    void UpdatePatrol()
    {
        if (isPatrolling == true)
        {
            if (Vector3.Distance(transform.position, _target) < 3f)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }
    }

    void UpdateChasing()
    {
        if(isChasing == true)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    void UpdateAttack()
    {
       if(isAttacking == true)
        {

        }
    }

    // Update NavMesh Patrol
    void UpdateDestination()
    {
        _target = _waypoints[_waypointIndex].position;
        _agent.SetDestination(_target);
    }

    // Change waypoint to move towards
    void IterateWaypointIndex()
    {
        _waypointIndex++;
        _currentWaypoint = _waypointIndex;

        if(_waypointIndex == _waypoints.Length)
        {
            _waypointIndex = 0; 
        }
    }
}
