using UnityEngine;
using System.Collections;
using System;
using UnityEngine.PostProcessing;

public class TriggerWaterEnter : MonoBehaviour {

    private Movement_Player WaterChange;
	private lerptest Water_Effect;

	public float EnterValue;
	public float EnterDiveValue;
	

	void Start()
	{
        WaterChange = FindObjectOfType<Movement_Player>();
		Water_Effect = FindObjectOfType<lerptest>();

        WaterChange.UnderWaterValue = 0f;
		WaterChange.DiveValue =-10f;
        Water_Effect.DiveUnderWaterValue = -0.5f;

		
	}

	private void OnTriggerStay(Collider other)
	{
			Debug.Log("Entered");
			WaterChange.UnderWaterValue = EnterValue;
			WaterChange.DiveValue = (EnterValue -10f);
			Water_Effect.DiveUnderWaterValue = EnterDiveValue;

	}
}