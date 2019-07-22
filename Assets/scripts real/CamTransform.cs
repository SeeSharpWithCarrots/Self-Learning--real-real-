using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTransform : MonoBehaviour {

    public GameObject This;
    public GameObject Reference;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        This.transform.position = Reference.transform.position;
	}
}
