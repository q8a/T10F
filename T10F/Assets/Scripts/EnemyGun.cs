using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGun : MonoBehaviour
{
    PlayerStats playerHealth;
    private Detection aggroDetection;
    private CharacterStats healthTarget;
    private float attackTimer;
    private float attackRefreshRate = 0.7f;
    Animator anim;
    Animator playerAnim;
    public Transform player;
    private NavMeshAgent agent;
    public GameObject bullet;
    public AudioSource gunSound;
    private void Awake()
    {
        aggroDetection = GetComponentInParent<Detection>();
        aggroDetection.onAggro += AggroDetection_onAggro;
        anim = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        player = GameObject.Find("Player").transform;
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    private void AggroDetection_onAggro(Transform target)
    {
        CharacterStats health = target.GetComponent<CharacterStats>();

        if (health != null)
        {
            healthTarget = health;
        }
    }

    private void Update()
    {
        
        if (healthTarget != null)
        {
            attackTimer += Time.deltaTime;
            if (CanAttack())
            {
                Attack();
            }
        }
    }

    private bool CanAttack()
    {
        return attackTimer >= attackRefreshRate;
    }

    public void Attack()
    {
        attackTimer = 0;
        EnemyHealth enemyStat = GetComponentInParent<EnemyHealth>();
        if (!enemyStat.isDead)
        {
            transform.LookAt(player);
            if (healthTarget.currentHelath > 0)
            {
                agent.SetDestination(player.transform.position);
                GameObject eBullet = Instantiate(bullet, transform.position, transform.rotation);
                gunSound.Play();
                Destroy(eBullet, 1f);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward * 1000, out hit, 1000f))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        healthTarget.TakeDamage(5);
                    }
                }
                playerHealth.CheckHealth();
            }
        }
        if(healthTarget.currentHelath<=0)
        {
            healthTarget.isDead = true;
        }
        if(healthTarget.isDead)
        {
            playerAnim.SetTrigger("isDeathTrigger");
        }
    }

}
