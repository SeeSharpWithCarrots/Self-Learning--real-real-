﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePickup : MonoBehaviour
{

    public GameObject Player;

    // Use this for initialization
    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Entered");
            Player.GetComponent<GrapplingHook>().enabled = true;
        }
    }
}
