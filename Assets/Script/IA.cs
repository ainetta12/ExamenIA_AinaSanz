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

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float detectionRange = 9;
    [SerializeField] private float attackRange = 5;

    State currentState;
    
    private NavMeshAgent agent;
    private Transform player;
    
   
   
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    
    
    void Start()
    {
        SetRandomPoint();
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

    void Chase()
    {
         if(IsInRange(detectionRange) == false)
        {
            SetRandomPoint();
            currentState = State.Chasing;
        }

        if(IsInRange(attackRange) == true)
        {
            currentState = State.Attacking;
        }
        agent.destination = player.position;
    }

     void Attack()
    {
        Debug.Log("Atacar");
        currentState = State.Chasing;
    }



     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(Transform point in patrolPoints)
        {
            Gizmos.DrawWireSphere(point.position, 0.5f);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }

}
