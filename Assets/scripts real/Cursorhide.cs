using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursorhide : MonoBehaviour {
    public bool Paused = false;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape)) 
        Cursor.lockState = CursorLockMode.Confined; //now when pressing ESC you can move the cursor freely
        Cursor.visible = true; //cursor becomes visible again (this makes no difference unless you make standalone)

          
                

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
