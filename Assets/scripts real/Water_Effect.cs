using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using System;

public class Water_Effect : MonoBehaviour 
{	
	private Movement_Player WaterChange1;
    private Vector3 newPosition;
	public float UnderWaterValueEffect = 0f;


    public float value;

	public PostProcessingProfile normal, fx;

	private PostProcessingBehaviour canImageFx;


    void Awake()
    {
        newPosition = transform.localPosition;
    }

    // Use this for initialization
    void Start () 
	{
	    WaterChange1 = FindObjectOfType<Movement_Player>();
        WaterChange1.UnderWaterValue = 0f;
		canImageFx = FindObjectOfType<PostProcessingBehaviour>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 position2 = new Vector3(0, value, 0); //coordinates of crouching


        if (gameObject.transform.position.y < WaterChange1.UnderWaterValue) 
		{
			canImageFx.profile = fx;
		}
		else
		{
			canImageFx.profile = normal;
		}

    }
}
