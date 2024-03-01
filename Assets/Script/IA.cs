using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Attacking
   
    }

    State currentState;
    
    private NavMeshAgent agent;
    private Transform player;
    
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float detectionRange = 15;
    [SerializeField] private float attackRange = 5;

   
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    
    
    void Start()
    {
        currentState = State.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
       
        {
            case State.Patrolling:
                Patrol();
            break;

            case State.Chasing:
                Chase();
            break;

            case State.Attacking:
                Attack();
            break;

        }
    }
    
     void SetRandomPoint()
    {
        agent.destination = patrolPoints[Random.Range(0,patrolPoints.Length - 1)].position;
    }

    bool IsInRange(float range)
    {
        if(Vector3.Distance(transform.position, player.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

     void Patrol()
    {
        if(IsInRange(detectionRange) == true)
        {
            currentState = State.Chasing;
        }

        if(agent.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }
    }

     void Attack()
    {
        
    }

     void Chase()
    {
        
    }





}
