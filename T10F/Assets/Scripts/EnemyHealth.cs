using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public AudioSource source;
    public AudioClip dieSound;
    public bool isDead = false;
    Animator enemyAnim;
    private void Start()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        enemyAnim = gameObject.GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        enemyAnim.SetTrigger("isDeadTrigger");
        isDead = true;
        source.PlayOneShot(dieSound, 0.3f);
    }
}
