using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    int id;
    [SerializeField]
    int currAmmo;
    float reloadSpeed = 3;
    public AudioSource source;
    public AudioClip gunSound;
    public AudioClip reloadSound;
    float impactForce = 30f;
    public float fireRate = 0.1f;
    public GameObject healthPackage;

    public int coins;
    public Text coinsText;

    GameObject gameController;
    WeaponDatabase database;

    Animator animator;
    Animator enemyAnim;

    EnemyHealth enemyStat;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    bool reloading = false;

    private float timer;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<WeaponDatabase>();
        id = GameObject.FindGameObjectWithTag("Weapon").GetComponent<ItemID>().itemID;
        currAmmo = database.weapons[id].maxAmmo;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        enemyAnim = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();
        coins = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            if (currAmmo == database.weapons[id].maxAmmo)
            {
                return;
            }
            else
            {
                
                StartCoroutine(Reload());
                //stop firing while reloading
                return;
            }
        }
        if(currAmmo<=0)
        {
            StartCoroutine(Reload());
            //stop firing while reloading
            return;
        }
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0;
                Fire();
            }
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        source.PlayOneShot(reloadSound, 0.3f);
        animator.SetBool("reloading", true);
        yield return new WaitForSeconds(reloadSpeed);
        currAmmo = database.weapons[id].maxAmmo;
        reloading = false;
        animator.SetBool("reloading", false);
    }

    void Fire()
    {
        muzzleFlash.Play();
        source.PlayOneShot(gunSound, 0.3f);
        currAmmo--;
        animator.SetInteger("condition", 6);

        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * 100, out hit, database.weapons[id].range))
        {
            
            enemyStat = hit.transform.gameObject.GetComponent<EnemyHealth>();
            if (hit.transform.gameObject.tag == "Enemy")
            {
                if (!enemyStat.isDead)
                {
                    enemyStat.TakeDamage(database.weapons[id].damage);

                    if (enemyStat.currentHealth <= 0)
                    {

                        enemyStat.isDead = true;
                        Instantiate(healthPackage, hit.transform.position, hit.transform.rotation);
                        coins = coins + 10;
                        coinsText.text = coins.ToString();
                        enemyStat.Die();
                        //hit.transform.gameObject.SetActive(false);
                    }
                }
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
            
        }
    }


}
