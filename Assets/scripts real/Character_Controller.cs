﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;
    float camRayLenght = 100f;

    void Start() { rb = GetComponent<Rigidbody>(); }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        Turning();

    }
    void Turning() { Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); }
}
