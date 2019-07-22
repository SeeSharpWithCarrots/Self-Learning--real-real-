using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAn: MonoBehaviour 
{

    CharacterController characterCollider;
    CapsuleCollider capCol;

	void Start ()
    {
        characterCollider = gameObject.GetComponent<CharacterController>();
        capCol = gameObject.GetComponent<CapsuleCollider>();
	}
	
	
	void Update ()
    {
        if (Input.GetKey (key: KeyCode.LeftControl))
        {
            characterCollider.height = 0.5f;
            capCol.height = 0.5f;
        }
        else
        {
            characterCollider.height = 2.0f;
            capCol.height = 2.0f;
        }
	}
}
