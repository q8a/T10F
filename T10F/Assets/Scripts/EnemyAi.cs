
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    private CharacterStats healthTarget;
    public Transform player;
    PlayerStats playerHealth;
    public LayerMask whatIsGround, whatIsPlayer;
    public AudioSource gunSound;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public GameObject firePoint;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject bullet;

    Animator anim;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        healthTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    public void Patroling()

    {
        EnemyHealth enemyStat = GetComponent<EnemyHealth>();
        if (!enemyStat.isDead)
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        EnemyHealth enemyStat = GetComponentInParent<EnemyHealth>();
        if (!enemyStat.isDead)
        {
            agent.SetDestination(player.position);
        }
        else
        agent.SetDestination(transform.position);
    }

    private void AttackPlayer()
    {

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        
        EnemyHealth enemyStat = GetComponent<EnemyHealth>();
        if (!enemyStat.isDead)
        {
            transform.LookAt(player);
            if (!alreadyAttacked)
            {
                if (healthTarget.currentHelath > 0)
                {
                    GameObject eBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                    gunSound.Play();
                    Destroy(eBullet, 1f);
                    anim.SetInteger("condition", 1);
                    RaycastHit hit;
                    if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward * 1000, out hit, 1000f))
                    {
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            healthTarget.TakeDamage(5);
                        }
                    }
                    playerHealth.CheckHealth();
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
                else
                {
                    Patroling();
                    anim.SetInteger("condition", 2);
                }
                if (healthTarget.currentHelath <= 0)
                {
                    healthTarget.isDead = true;
                    PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                    ps.Die();
                    anim.SetInteger("condition", 0);
                }
            }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
