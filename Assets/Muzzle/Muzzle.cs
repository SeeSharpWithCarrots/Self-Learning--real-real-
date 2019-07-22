using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z;
    
	void Update ()
    {
        GetComponent<Renderer>().transform.Rotate(new Vector3(X, Y, Z)*Time.deltaTime); //x y and z
	}
}
