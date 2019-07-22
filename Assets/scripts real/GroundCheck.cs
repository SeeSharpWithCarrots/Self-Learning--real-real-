using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

public GameObject AnimatorGet;
Animator anim;
public bool Colliding = false;

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Colliding");
		anim.SetTrigger("Jumo");
	}
}
