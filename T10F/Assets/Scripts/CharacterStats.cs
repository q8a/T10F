using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float currentHelath;
    public float maxHealth;

    public bool isDead = false;

    public virtual void CheckHealth()
    {
        if (currentHelath >= maxHealth)
            currentHelath = maxHealth;
        if(currentHelath <= 0)
        {
            currentHelath = 0;
            isDead = true;
        }
    }

    public virtual void Die()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHelath -= damage;
    }
}
