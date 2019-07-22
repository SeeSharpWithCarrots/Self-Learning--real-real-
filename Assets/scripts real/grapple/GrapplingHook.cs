using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float HookTimer;
    public float CoolDown;

    public GameObject Hook;
    public GameObject HookHolder;
    public GameObject Camera;
    


    public GameObject Player;

    public float HookTravelSpeed;
    public float PlayerTravelSpeed;

    public static bool Fired;
    public bool Hooked;
    public GameObject HookedObject;

    public float MaxDistance;
    private float CurrentDistance;
    public float NewGravity;
    
    bool CanFire = true;

    private Vector3 velocity = Vector3.zero;






    void Update()
    {
        Hook.transform.rotation = HookHolder.transform.rotation;
        Hook.transform.localScale = new Vector3(1, 1, 1);


        if (Hooked == true && Input.GetKey("x"))
        {
            ReturnHook();
        }

        if (Input.GetKey("x") && Fired == false && CanFire == true) //we can change this later
            Fired = true;
            Hook.transform.rotation = HookHolder.transform.rotation;

    }

    void FixedUpdate()
    {

        

        Vector3 HookPosition = new Vector3(Hook.transform.position.x, transform.position.y, Hook.transform.position.z);

        //firing the hook



        if (Fired == true && Hooked == false) 
        {
            
            Hook.transform.localScale = HookHolder.transform.localScale;
            HookHolder.transform.localScale = new Vector3(1,1,1);
            HookHolder.transform.parent = null;
            



            Hook.transform.Translate(Vector3.forward * Time.deltaTime * HookTravelSpeed);
            CurrentDistance = Vector3.Distance(transform.position, Hook.transform.position);
            Hook.transform.localScale = new Vector3(1, 1, 1);

            if (CurrentDistance >= MaxDistance)
                ReturnHook();
            Hooked = false;
        }


        if (Hooked == true)
        {
            StartCoroutine(CoolDownTimer());
            StartCoroutine(Timer());

            Player.GetComponent<Rigidbody>().AddForce(transform.forward * 30); //LATER WE MAKE THESE VALUES DYNAMIC
            Player.GetComponent<Rigidbody>().AddForce(transform.up * 20);



            Player.transform.LookAt(HookPosition);

            //ENABLE GRAPPLE PHYSICS
            Player.GetComponent<CharacterController>().enabled = false;

            Player.GetComponent<Rigidbody>().useGravity = true;
            Player.GetComponent<Rigidbody>().isKinematic = false;


            //rest

            Hook.transform.parent = HookedObject.transform;
            Hook.transform.localScale = new Vector3(1, 1, 1);
            transform.position = Vector3.SmoothDamp(transform.position, Hook.transform.position, ref velocity, PlayerTravelSpeed);
            float DistanceToHook = Vector3.Distance(transform.position, Hook.transform.position); //calculates distance between player and hook

            Player.GetComponent<Movement_Player>().gravity = NewGravity; //we can change this for different effects


            if (DistanceToHook < 5) //when do you detach?
                ReturnHook();
                HookHolder.transform.parent = Camera.transform;
                HookHolder.transform.localPosition = new Vector3(0, 0, 0);

       

        }
        else
        {
            Hook.transform.localScale = new Vector3(1, 1, 1);
            Hook.transform.rotation = HookHolder.transform.rotation;
            Hook.transform.parent = HookHolder.transform;
            Debug.Log("UnHooked"); //AND reENABLE GRAVITY ^

        }

    }



    void ReturnHook()
    {
        // reset scale (TO ADD) (24:34)
        Hook.transform.rotation = HookHolder.transform.rotation; // glitches without this line
        Hook.transform.position = HookHolder.transform.position; //when returning go back to origin point
        Fired = false;
        Hooked = false;

        HookHolder.transform.parent = Camera.transform;
        HookHolder.transform.rotation = Camera.transform.rotation;
        HookHolder.transform.position = Camera.transform.position;
    }
    IEnumerator Timer()
    {

        yield return new WaitForSeconds(HookTimer); //after a while the hook detaches
        ReturnHook();


    }
    IEnumerator CoolDownTimer()
    {
        CanFire = false;
        yield return new WaitForSeconds(CoolDown); //cooldown before you can hook again
        CanFire = true;

    }
}
