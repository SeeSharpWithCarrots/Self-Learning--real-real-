using UnityEngine;
using System.Collections;
using System;
using UnityEngine.PostProcessing;

public class TriggerWater : MonoBehaviour {

    private Movement_Player WaterChange;
	public Water_Effect Water_Effect;
	

	void Start()
	{
        WaterChange = FindObjectOfType<Movement_Player>();
        WaterChange.UnderWaterValue = 0f;
        WaterChange.DiveValue = -10f;

		Water_Effect = FindObjectOfType<Water_Effect>();
		Water_Effect.UnderWaterValueEffect = 10f;
	}

	private void OnTriggerEnter(Collider other)
	{
			Debug.Log("Entered");
	}
	private void OnTriggerStay(Collider other)
	{
			Debug.Log("Staying");
			WaterChange.UnderWaterValue = 37f;
			WaterChange.DiveValue = 27f;

			Water_Effect.UnderWaterValueEffect = 37f;
	}
	private void OnTriggerExit(Collider other)
	{
			Debug.Log("Exit");
			WaterChange.UnderWaterValue = 0f;
			WaterChange.DiveValue = 10f;

			Water_Effect.UnderWaterValueEffect = 27f;

	}
}