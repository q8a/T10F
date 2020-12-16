using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Detection aggroDetection;
    private NavMeshAgent navMeshAgent;
    private Transform target;
    public float walkSpeed;
    public float runSpeed;
    private Animator anim;
    private void Awake()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        aggroDetection = GetComponent<Detection>();
        aggroDetection.onAggro += AggroDetection_onAggro;
        anim = GetComponentInChildren<Animator>();
    }

    private void AggroDetection_onAggro(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            anim.SetBool("Aware", true);
            EnemyHealth enemyStat = GetComponent<EnemyHealth>();
            if (!enemyStat.isDead)
            {
                navMeshAgent.SetDestination(target.position);
                float speed = navMeshAgent.velocity.magnitude;
            }
            else
                // if he is dead. should leave in that place.
                navMeshAgent.SetDestination(navMeshAgent.transform.position);
        }
    }
}
