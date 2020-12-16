using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int itemID;
    public int weaponType;
    public string name;
    public GameObject weaponObject;
    public float damage;
    public float fireRate = 0.1f;
    public float range;
    public int maxAmmo;
}
