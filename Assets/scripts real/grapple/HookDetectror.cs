using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetectror : MonoBehaviour
{

    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hookable")
        {
            Player.GetComponent<GrapplingHook>().Hooked = true;
            Player.GetComponent<GrapplingHook>().HookedObject = other.gameObject; //setting hook as a child
        }
    }
}
