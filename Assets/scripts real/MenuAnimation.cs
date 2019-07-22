using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject Reference; //drag what you want to refer to here
    Animator Panel;

    void Start()
    {
        Panel = Reference.GetComponent<Animator>();
    }

    public void Slide()
    {
        Panel.SetTrigger("Slide");
        Debug.Log("we got here");
    }
} 

