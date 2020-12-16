using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int weaponSelected;

    //[SerializeField]
    GameObject primary, secondry, melee;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weaponSelected != 1)
            {
                SwapWeapon(1);
             
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponSelected != 2)
            {
                SwapWeapon(2);
              
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weaponSelected != 3)
            {
                SwapWeapon(3);
                
            }
        }

    }

    void SwapWeapon(int weaponType)
    {
        if (weaponType == 1)
        {
            primary.SetActive(true);
            secondry.SetActive(false);
            melee.SetActive(false);
            weaponSelected = 1;
        }

        if (weaponType == 2)
        {
            primary.SetActive(false);
            secondry.SetActive(true);
            melee.SetActive(false);
            weaponSelected = 2;
        }

        if (weaponType == 3)
        {
            primary.SetActive(false);
            secondry.SetActive(false);
            melee.SetActive(true);
            weaponSelected = 3;
        }
    }
}
