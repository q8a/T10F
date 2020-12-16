using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float radius;
    EnemyHealth enemyStat;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStat = GetComponent<EnemyHealth>();
    }
    
    private void Update()
    {
        if (!enemyStat.isDead)
        {
            if (!agent.hasPath)
            {
                agent.SetDestination(Patrol.Instance.GetRandomPoint(transform, radius));
            }
        }
    }
}
