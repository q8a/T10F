using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    Animator animator;
    HealthBar playerHealthBar;
    Color redcolor = Color.red;
    Color greencolor = Color.green;
    Color orangecolor = Color.yellow;
    private void Start()
    {
        playerHealthBar = GetComponent<HealthBar>();
        maxHealth = 100;
        currentHelath = maxHealth;
        SetStates();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void Die()
    {
        animator.SetTrigger("isDeathTrigger");
    }

    void SetStates()
    {
        playerHealthBar.userHealthSlider.value = currentHelath;
        if(currentHelath >= 50 && currentHelath <= 100)
        {
            playerHealthBar.userHealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = greencolor;
        }

        else if (currentHelath < 50 && currentHelath >= 20)
        {
            playerHealthBar.userHealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = orangecolor;
        }

        else
        {
            playerHealthBar.userHealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = redcolor;
        }
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        SetStates();
    }

    

}
