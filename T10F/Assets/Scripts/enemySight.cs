using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemySight : MonoBehaviour
{
    public NavMeshAgent agent;
    public CharacterController character;
    public Animator anim;

    public enum State
    {
        PATROL,
        CHASE,
        INVESTIGATE,
        ATTACK
    }

    public State state;

    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 0.5f;

    public float chaseSpeed = 1f;
    public GameObject target;

    private Vector3 investigateSpot;
    private float timer = 0;
    public float investigateWait = 10;

    public float heightMultiplier;
    public float sightDist;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        CharacterStats enemyStat = GetComponent<CharacterStats>();
        //agent.updatePosition = true;
        //agent.updateRotation = false;

        waypoints = GameObject.FindGameObjectsWithTag("waypoint");

        waypointInd = Random.Range(0, waypoints.Length);

        state = enemySight.State.PATROL;
        heightMultiplier = 1.5f; 
        StartCoroutine(FSM());

        IEnumerator FSM()
        {
            while(!enemyStat.isDead)
            {
                switch(state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                    case State.INVESTIGATE:
                        Investigate();
                        break;
                }
                yield return null;
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            anim.SetInteger("condition", 1);
            if(Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >=2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.SimpleMove(agent.desiredVelocity);

            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position)<= 2)
            {
                waypointInd = Random.Range(0, waypoints.Length);
            }
            else
            {
                character.SimpleMove(Vector3.zero);
            }
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            agent.stoppingDistance = 3f;
            character.SimpleMove(agent.desiredVelocity);
        }

        void Investigate()
        {
            timer += Time.deltaTime;
            

            agent.SetDestination(this.transform.position);
            character.SimpleMove(Vector3.zero);
            transform.LookAt(investigateSpot);
            if(timer >= investigateWait)
            {
                state = enemySight.State.PATROL;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            state = enemySight.State.INVESTIGATE;
            investigateSpot = coll.gameObject.transform.position;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = enemySight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = enemySight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = enemySight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }
    }
}
