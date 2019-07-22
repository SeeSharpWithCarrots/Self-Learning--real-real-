using UnityEngine;
using System.Collections;
using System;
using UnityEngine.PostProcessing;

public class Moving_Platform : MonoBehaviour {

    public GameObject Player;
    public GameObject Platform;
	

	private void OnTriggerStay(Collider other)
	{
        if (other.tag == "Player")
        {
            Debug.Log("Entered");
            Player.transform.parent = Platform.transform;
        }

	}
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        Player.transform.parent = null;
    }
}