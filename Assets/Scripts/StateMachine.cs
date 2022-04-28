using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    [Header("Patrol Variables")]
    public Transform[] _waypoints;
    public int _speed;
    private NavMeshAgent agent;
    private int _waypointIndex;
    private float _dist;
    private Vector3 target;

    [Header("AI Variables")]
    public Transform _player;
    public float _moveSpeed = 10f;
    public float _maxDistance = 20f;
    public float _minDistance = 10f;

    private float _distanceToPlayer; 

    private enum State
    {
        onPatrol,
        chasing, 
        attack
    }

    private State _curState;

    void Start()
    {
        _waypointIndex = 0;
        transform.LookAt(_waypoints[_waypointIndex].position);

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _distanceToPlayer = Vector3.Distance(_player.position, transform.position); // Calculate the distance of the player from current position.

        switch (_curState)
        {
            case State.onPatrol:
                UpdatePatrolState();
                break;

            case State.chasing:
                UpdateChaseState();
                break;

            case State.attack:
                UpdateAttackState();
                break;
        }
    }

    void UpdatePatrolState()
    {
        Patrol();

        if (_distanceToPlayer > _maxDistance)
            _curState = State.onPatrol;

        else if (_distanceToPlayer < _maxDistance && _distanceToPlayer > _minDistance)
            _curState = State.chasing;

        else if (_distanceToPlayer < _minDistance)
            _curState = State.attack;
    }

    void UpdateChaseState()
    {
        Chase();

        if (_distanceToPlayer > _maxDistance)
            _curState = State.onPatrol;

        else if (_distanceToPlayer < _maxDistance && _distanceToPlayer > _minDistance)
            _curState= State.chasing;

        else if (_distanceToPlayer < _minDistance)
            _curState = State.attack;
    }

    void UpdateAttackState()
    {
        Attack();

        if (_distanceToPlayer > _maxDistance)
            _curState = State.onPatrol;

        else if (_distanceToPlayer < _maxDistance && _distanceToPlayer > _minDistance)
            _curState = State.chasing;

        else if (_distanceToPlayer < _minDistance)
            _curState = State.attack;
    }

    //////////////////////
    /// Patrol Methods ///
    //////////////////////         
    void Patrol()
    {
        Debug.Log("What a crappy day I wanna destory this place!");
        
    }

    ///////////////////////
    /// Chasing Methods ///
    ///////////////////////

    void Chase()
    {
        Debug.Log("Who The FUUCK THIS BITCH");
        agent.SetDestination(_player.position);
    }

    void Attack()
    {
        Debug.Log("LEMME CLAP THEM CHEEKS BOI");
    }
}
