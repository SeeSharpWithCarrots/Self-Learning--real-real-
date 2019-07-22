using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimSwitch : MonoBehaviour {

    public bool UnderWater = false;
    public float UnderWaterValue = 0f;

    //MOVEMENT
    public GameObject GroundMovement;

    // later add references for the lerptest

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.transform.position.y < UnderWaterValue)
        {
            UnderWater = true;
            //movement
            Debug.Log("you are underwater");
            GroundMovement.GetComponent<lerptest>().enabled = false;
        }
        else
        {
            UnderWater = false;
            //movement
            GroundMovement.GetComponent<lerptest>().enabled = true;

        }
		
	}
}
