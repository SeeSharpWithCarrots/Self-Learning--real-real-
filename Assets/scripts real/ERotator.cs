﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERotator : MonoBehaviour
{
    //deltaTime is for making the rotation per second rather than per frame which makes it slower [(x, y, z) * Time.deltaTime);]
    //the three numbers are for x, y and z values for the rotation and the larger the number the faster it spins
    private void Update()
    {
        if (Input.GetKeyDown(key: KeyCode.E))
        {
            GetComponent<Renderer>().transform.Rotate(new Vector3(0, -10, 0));
        }
        
    }

}
