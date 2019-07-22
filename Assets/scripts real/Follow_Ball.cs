﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Ball : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;
	
	void Start ()
    {
        offset = transform.position - target.position;
	}
	
	
	void FixedUpdate ()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Time.deltaTime * smoothing);
	}
}
