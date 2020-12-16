using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseHP : MonoBehaviour
{
    PlayerInventory inventory;
    CharacterStats ps;
    GameObject gameController;
    HealthPackDatabase database;
    public Text htext;
    public Text stext;
    public GameObject uiPanel;
    public AudioSource source;
    public AudioClip hpSound;
    Animator anim;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        inventory = gameController.GetComponent<PlayerInventory>();
        ps = GetComponent<CharacterStats>();
        database = gameController.GetComponent<HealthPackDatabase>();
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        IncreaseHealth();
        ps.CheckHealth();
    }
    void IncreaseHealth()
    {
        if (Input.GetKey(KeyCode.H))
        {

            if (inventory.inventory[3] != 0)
            {
                if (ps.currentHelath < ps.maxHealth)
                {
                    ps.currentHelath += database.healthPacks[1].healthAmount;
                    inventory.inventory[3] = 0;
                    stext.text = database.healthPacks[0].name + " used successfully!";
                    source.PlayOneShot(hpSound, 0.3f);
                    anim.SetTrigger("healthTrigger");
                    StartCoroutine(SuccessRoutine());
                    
                }
                else
                {
                    stext.text = "You Have full Health as of Now, Take Care of Yourself!";
                    StartCoroutine(SuccessRoutine());
                }
            }
            else
            {
                StartCoroutine(ActivationRoutine());
            }
        }
    }

    private IEnumerator ActivationRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        htext.gameObject.SetActive(true);
        uiPanel.gameObject.SetActive(true);
        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(1);
        //Game object will turn off
        htext.gameObject.SetActive(false);
        uiPanel.gameObject.SetActive(false);
    }

    private IEnumerator SuccessRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        //Turn My game object that is set to false(off) to True(on).
        stext.gameObject.SetActive(true);
        uiPanel.gameObject.SetActive(true);
        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(1);
        //Game object will turn off
        stext.gameObject.SetActive(false);
        uiPanel.gameObject.SetActive(false);
    }
}
