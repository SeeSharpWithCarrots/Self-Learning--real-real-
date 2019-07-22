using UnityEngine;
using System.Collections;
using System;

public class lerptest: MonoBehaviour
{
    private Movement_Player _MovementFunction;

    public float DiveUnderWaterValue;

	public float smooth =2;
    public float watersmooth;
	private Vector3 newPosition;

	public float Hamount = 0.7f;
	public float HLamount =1f;

    public GameObject IBIS;


    void Awake ()
	{
		newPosition = transform.localPosition;
	}
    void Start()
    {
        _MovementFunction = FindObjectOfType<Movement_Player>();

        _MovementFunction.StuckPlayer();
        _MovementFunction.Stuck1Player();

    }
    void Update ()
	{
		PositionChanging();


	}

    void PositionChanging ()
	{
		Vector3 position1 = new Vector3(0, 1.8f, 0); //coordinates of 1
		Vector3 position2 = new Vector3(0, 0.5f, 0); //coordinates of 2

		//COORDINATES FOR LEAN

		Vector3 positionA = new Vector3(-(Hamount), 1.8f, 0); //how much you can lean Q 
        Vector3 positionB = new Vector3(0, 1.8f, 0); //neutral position                //actually the Y value of all of these should be the camera height. When we merge there won't be glitches
        Vector3 positionC = new Vector3((Hamount), 1.8f, 0); //how much you can lean E

		Vector3 positionE = new Vector3(0, 2, 0); //ALT lean

		//COORDINATES FOR CROUCH LEAN
		Vector3 positionA1 = new Vector3(-(HLamount), 0.5f, 0); //how much you can lean Q 
        Vector3 positionB1 = new Vector3(0, 0.5f, 0); //neutral position                //actually the Y value of all of these should be the camera height. When we merge there won't be glitches
        Vector3 positionC1 = new Vector3((HLamount), 0.5f, 0); //how much you can lean E

		Vector3 positionE1 = new Vector3(0, 1.5f, 0); //ALT lean

		//COORDINATES FOR STUCK
		Vector3 positionE2 = new Vector3(0, 0.5f, 0); //can't lean up when stuck


        if (gameObject.transform.position.y > DiveUnderWaterValue) // IF ABOVE THE WATER ACT NORMAL
        {
            if (Input.GetAxis("Crouch") == 1) //CROUCHED
                newPosition = positionB1;
          if (Input.GetKey(KeyCode.Q)) //if you press Q
                newPosition = positionA1;
            if (Input.GetKey(KeyCode.E)) //if you press E
                newPosition = positionC1;
            if (Input.GetKey(KeyCode.LeftAlt) && !Stuck() && !Stuck() && !_MovementFunction.StuckPlayer() && !_MovementFunction.Stuck1Player() ) //if you press ALT
                newPosition = positionE1;

            if (Input.GetAxis("Crouch") == 0 )
            {
                newPosition = positionB;
                if (Input.GetKey(KeyCode.Q)) //if you press Q
                    newPosition = positionA;
                if (Input.GetKey(KeyCode.E)) //if you press E
                    newPosition = positionC;
                if (Input.GetKey(KeyCode.LeftAlt)) //if you press ALT
                    newPosition = positionE;

                if (Stuck() || Stuck1() || _MovementFunction.StuckPlayer() || _MovementFunction.Stuck1Player())
                {
                    newPosition = positionB1;
                    if (Input.GetKey(KeyCode.Q)) //if you press Q
                        newPosition = positionA1;
                    if (Input.GetKey(KeyCode.E)) //if you press E
                        newPosition = positionC1;

                    if (Input.GetKey(KeyCode.LeftAlt)) //no ALT
                        newPosition = positionE2;
                }
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * smooth);
        }
        else
        {
            newPosition = positionB1;
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * watersmooth);
        }


       
	}

    bool Stuck()   //returns true if the distance between the head of the character and the collider is less that green value
    {
        RaycastHit hit;
        Vector3 center = transform.position;
        return Physics.SphereCast(center, 0.5f, transform.TransformDirection(Vector3.up), out hit, 2.5f); //RAYCAST DISTANCE
    }
    bool Stuck1()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 2f);
    }
}


