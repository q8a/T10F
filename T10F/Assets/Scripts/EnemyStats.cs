using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private void Start()
    {
        maxHealth = 100;
        currentHelath = maxHealth;
    }

    public override void Die()
    {
        Debug.Log("You Died!");
    }
}
