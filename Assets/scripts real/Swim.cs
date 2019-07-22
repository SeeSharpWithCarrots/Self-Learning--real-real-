using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{

    public float UnderWaterValue = 2000f;


    public float height = 0.5f;
    public float timespeed = 3f;
    public float timespeedslow = 1.0f;
    public float speed = 10f;               // walking speed
    public float speedDive = 10f;
    public float gravity = -10f;            // value to be subtracted from deltaY
    public float accel_const = 0.6f;        // constant at which gravity increases while falling
    public float terminal_v = -100f;
    public float jumpforce = 22f;
    public float fallforce = 6f;
    public float crouchheight = 0.5f;       //crouch base height
    public float crouchspeed = 1f;
    public float crouchupspeed = 10f;
    public float runspeed = 20f;
    public float TransitionSpeed = 1f; // lerp time when pressing shift


    public Vector3 left = new Vector3 (0f,-0.2f,0f);
    public Vector3 right;
    public Vector3 forward;
    public Vector3 back;

    bool crouched = false;
    bool uncrouched = false;
    

    public float deltaY = 0;
    bool duringjump = false;
    bool spaceunpress = true;
    private CharacterController _charCont;
    private CharacterController characterController;
    private CapsuleCollider capCol;



    // Use this for initialization
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        characterController = gameObject.GetComponent<CharacterController>();
        capCol = gameObject.GetComponent<CapsuleCollider>();
    }
    void Update()
    {
           

        Vector3 center = transform.position + characterController.center;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 5, Color.red);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float deltaDive = Input.GetAxis("Dive") * speedDive;
        Vector3 movement = new Vector3(deltaX, deltaDive, deltaZ);

        if (gameObject.transform.position.y < UnderWaterValue) 
        {
            duringjump = true;

            crouched = true;
            accel_const = 0f;
            terminal_v = -0f;

            // the speed

            if (gameObject.transform.position.y > UnderWaterValue -1f)
            {
                speedDive = 0f;
                
            }
            else
            {
                speedDive = 10f;
            }
            if (Input.GetAxis("Crouch") == 1)
            {
                speedDive = 10f;
            }
            else
            {
                gravity = -29f;

            }

                Debug.Log("Underwater");
        }
        else
        {
            //ADD A DURING JUMP HERE

            accel_const = 1f;
            crouched = false;
            terminal_v = -100f;

        }
        //add here

        if (Input.GetAxisRaw("Jump") == 1 && spaceunpress && (Grounded() || Grounded1() || Grounded2() || Grounded3() || Grounded4()))
        {
            duringjump = true;
            spaceunpress = false;
        }

        else if ((Grounded() || Grounded1() || Grounded2() || Grounded3() || Grounded4()) && !spaceunpress)
            duringjump = false;

        if (Input.GetAxisRaw("Jump") == 0 && (Grounded() || Grounded1() || Grounded2() || Grounded3() || Grounded4()))
            spaceunpress = true;


        DynGravity();
        deltaY = gravity;
        if (duringjump)
            deltaY += jumpforce;
        else if (!duringjump && (!Grounded() || !Grounded1() || !Grounded2() || !Grounded3() || !Grounded4()))
            deltaY += fallforce;

        Vector3 jumping = new Vector3(deltaX, deltaY, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);
        jumping *= Time.deltaTime;
        jumping = transform.TransformDirection(jumping);
        _charCont.Move(jumping);

        // START OF CROUCHING SCRIPT

        if (Input.GetAxis("Crouch") == 1)
        {
            //adding duringjump = true means that if crouching, player will jump when falling off edges
            //adding duringjump = false means that if crouching in the air, the player's fall speed is increased
            spaceunpress = false;
            crouched = true;
        }
        else if (Input.GetAxis("Crouch") == 0)
        {
            characterController.height = Mathf.Lerp(characterController.height, height, timespeed * Time.deltaTime);
            capCol.height = Mathf.Lerp(capCol.height, height, timespeed * Time.deltaTime);
            crouched = false;
            if (Stuck() || Stuck1())        //checks if the character is under a surface
            {
                crouched = true;
                characterController.height = crouchheight;
                capCol.height = crouchheight;
                spaceunpress = false;
            }
            else
            {
                crouched = false;
            }

        }
        if (crouched)           //if crouched bool is true
        {
            characterController.height = Mathf.Lerp(characterController.height, crouchheight, timespeed * Time.deltaTime);
            capCol.height = Mathf.Lerp(capCol.height, crouchheight, timespeed * Time.deltaTime);
            speed = Mathf.Lerp(crouchspeed, crouchupspeed, timespeedslow * Time.deltaTime); //speed variable can be modified above
        }
        else
        {

            speed = Mathf.Lerp(crouchupspeed, crouchspeed, timespeedslow * Time.deltaTime);
            
        }

        if ((!Grounded() || !Grounded1() || !Grounded2() || !Grounded3() || !Grounded4()) && Input.GetAxis("Crouch") == 1)    //if not on the ground and pressing "crouch" button
        {
            characterController.height = Mathf.Lerp(characterController.height, crouchheight, timespeed * Time.deltaTime);
            capCol.height = Mathf.Lerp(capCol.height, crouchheight, timespeed * Time.deltaTime);
            speed = crouchupspeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(crouchupspeed, runspeed, TransitionSpeed * TransitionSpeed * Time.deltaTime);
            if (Stuck() || crouched) //|| is OR logic
            {
                speed = crouchspeed;
            }
            if (crouched && !Grounded() || !Grounded1() || !Grounded2() || !Grounded3() || !Grounded4())
            {
                speed = runspeed;
            }
        }
        if (Grounded1())
        {
            Debug.Log("Ground1 yes");
        }
        if (Grounded2())
        {
            Debug.Log("Ground2 yes");
        }
        if (Grounded3())
        {
            Debug.Log("Ground3 yes");
        }
        if (Grounded4())
        {
            Debug.Log("Ground4 yes");
        }
    }

    bool Grounded()                     // returns true if on the ground
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f);
    }
    bool Grounded1()
    {
        return Physics.Raycast(transform.position + left, transform.TransformDirection(Vector3.down), 1.2f);
    }
    bool Grounded2()
    {
        return Physics.Raycast(transform.position + right, transform.TransformDirection(Vector3.down), 1.2f);
    }
    bool Grounded3()
    {
        return Physics.Raycast(transform.position + forward, transform.TransformDirection(Vector3.down), 1.2f);
    }
    bool Grounded4()
    {
        return Physics.Raycast(transform.position + back, transform.TransformDirection(Vector3.down), 1.2f);
    }

    void DynGravity()                   // function for dynamic gravity, its acceleration
    {
        if (Grounded())
        {
            gravity = -10f;
        }
        else
        {
            if (deltaY < terminal_v)
                deltaY = terminal_v;
            else
                gravity -= accel_const;
        }
    }
    bool Stuck()   //returns true if the distance between the head of the character and the collider is less that green value
    {
        RaycastHit hit;
        Vector3 center = transform.position + characterController.center;
        return Physics.SphereCast(center, 0.5f, transform.TransformDirection(Vector3.up), out hit, 2f); //RAYCAST DISTANCE
    }
    bool Stuck1()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 2f);
    }
    
}