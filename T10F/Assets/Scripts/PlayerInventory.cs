using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int[] inventory;
    //public int[] healthInventory;
    public GameObject[] weaponSlot;

    public void Start()
    {
        inventory = new int[4];
        //healthInventory = new int[1];
        weaponSlot = new GameObject[3];

        weaponSlot[0] = GameObject.FindGameObjectWithTag("Primary");
        weaponSlot[1] = GameObject.FindGameObjectWithTag("Secondary");
        weaponSlot[2] = GameObject.FindGameObjectWithTag("Melee");
    }
}
