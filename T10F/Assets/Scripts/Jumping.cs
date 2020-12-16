using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public GameObject theNPC;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JumpOver"))
        {
            theNPC.GetComponent<Animator>().Play("Jump Over");
        }
        if (Input.GetButtonDown("Punch"))
        {
            theNPC.GetComponent<Animator>().Play("Right Hook");
        }
        if (Input.GetButtonDown("Kick"))
        {
            theNPC.GetComponent<Animator>().Play("Drop Kick");
        }
    }
}
