using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
    public event Action<Transform> onAggro = delegate { };
    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyStat = GetComponent<EnemyHealth>();
        if (!enemyStat.isDead)
        {
            var player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                onAggro(player.transform);
                agent.SetDestination(player.transform.position);
            }
        }

    }
}

