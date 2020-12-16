using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    float pickupRange = 100f;
    int pickupLayerMask;
    public AudioSource source;
    public AudioClip pickUpSound;
    GameObject gameController;
    GameObject primaryWeapon, secondaryWeapon, meleeWeapon;
    WeaponDatabase database;
    PlayerInventory inventory;
    HealthPackDatabase healthPackDatabase;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<WeaponDatabase>();
        healthPackDatabase = gameController.GetComponent<HealthPackDatabase>();
        inventory = gameController.GetComponent<PlayerInventory>();
        pickupLayerMask = LayerMask.GetMask("PickUp");
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 3f, true);
        
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange,pickupLayerMask))
        {
            if(Input.GetKey(KeyCode.E))
            {
                int id = hit.transform.GetComponent<ItemID>().itemID;
             
                if (database.weapons[id].weaponType == 1)
                {
                    if(inventory.inventory[0] == id)
                    {
                        Debug.Log("You already have a primary weapon");
                    }
                    else if(inventory.inventory[0] == 0)
                    {
                        source.pitch = Random.Range(0.8f, 1.2f);
                        source.PlayOneShot(pickUpSound, 0.3f);
                        inventory.inventory[0] = id;
                        primaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[0].gameObject.transform.position, inventory.weaponSlot[0].gameObject.transform.rotation);
                        primaryWeapon.transform.SetParent(inventory.weaponSlot[0].transform);
                    }
                    else if(inventory.inventory[0] != id)
                    {
                        Destroy(primaryWeapon.gameObject);
                        inventory.inventory[0] = id;
                        primaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[0].gameObject.transform.position, inventory.weaponSlot[0].gameObject.transform.rotation);
                        primaryWeapon.transform.SetParent(inventory.weaponSlot[0].transform);
                    }
                }


                if (database.weapons[id].weaponType == 2)
                {
                    if (inventory.inventory[1] == id)
                    {
                        Debug.Log("You alread have a secondary weapon");
                    }
                    else if (inventory.inventory[1] == 0)
                    {
                        source.pitch = Random.Range(0.8f, 1.2f);
                        source.PlayOneShot(pickUpSound, 0.3f);
                        inventory.inventory[1] = id;
                        secondaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[1].gameObject.transform.position, inventory.weaponSlot[1].gameObject.transform.rotation);
                        secondaryWeapon.transform.SetParent(inventory.weaponSlot[1].transform);
                    }
                    else if (inventory.inventory[1] != id)
                    {
                        Destroy(secondaryWeapon.gameObject);
                        inventory.inventory[1] = id;
                        secondaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[1].gameObject.transform.position, inventory.weaponSlot[1].gameObject.transform.rotation);
                        secondaryWeapon.transform.SetParent(inventory.weaponSlot[1].transform);
                    }
                }


                if (database.weapons[id].weaponType == 3)
                {
                    if (inventory.inventory[2] == id)
                    {
                        Debug.Log("You already have a melee weapon!");
                    }
                    else if (inventory.inventory[2] == 0)
                    {
                        source.pitch = Random.Range(0.8f, 1.2f);
                        source.PlayOneShot(pickUpSound, 0.3f);
                        inventory.inventory[2] = id;
                        meleeWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[2].gameObject.transform.position, inventory.weaponSlot[2].gameObject.transform.rotation);
                        meleeWeapon.transform.SetParent(inventory.weaponSlot[2].transform);
                    }
                    else if (inventory.inventory[2] != id)
                    {
                        Destroy(meleeWeapon.gameObject);
                        inventory.inventory[2] = id;
                        meleeWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[2].gameObject.transform.position, inventory.weaponSlot[2].gameObject.transform.rotation);
                        meleeWeapon.transform.SetParent(inventory.weaponSlot[2].transform);
                    }
                }
// health packs
                
            }
            if(Input.GetKey(KeyCode.J))
            {
                int packID = hit.transform.GetComponent<HealthPackItemID>().itemID;
                if (healthPackDatabase.healthPacks[packID].packType == 1)
                {
                    if (inventory.inventory[3] == packID)
                    {
                        Debug.Log("No more place for extra primary health package");
                    }
                    else if (inventory.inventory[3] == 0)
                    {
                        source.pitch = Random.Range(0.8f, 1.2f);
                        source.PlayOneShot(pickUpSound, 0.3f);
                        inventory.inventory[3] = packID;
                        Destroy(hit.transform.gameObject);
                    }
                    else if (inventory.inventory[3] != packID)
                    {
                        source.pitch = Random.Range(0.8f, 1.2f);
                        source.PlayOneShot(pickUpSound, 0.3f);
                        inventory.inventory[3] = packID;
                    }
                }
            }
        }
    }
}
