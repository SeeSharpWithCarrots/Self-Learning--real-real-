using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject This;
    public GameObject CamReference;
    public GameObject AnimatorGet; //camera animation
    public GameObject GunAnimatorGet; //gun animation (same pattern below)
    Animator anim;
    Animator GunAnim;

    bool Repeat = false;

    public float TimeBeforeAnimationEnable =0f; // how long does it have to pass before you can "fall?"

    private bool SpawnAnimation;
    public bool tester = false;

    public float SideRaycasts;

    public float UnderWaterValue = 0f;
    public float DiveValue = 0f; //where does the dive animation play?
    float speedDive = 0f;
    public float SwimUpDownSpeed =20f;

    public bool IsUnderWater = false;

    public float height = 2.0f;
    public float timespeed = 3f;
    public float timespeedslow = 1.0f;
    public float speed = 10f;               // walking speed
    public float gravity = -10f;            // value to be subtracted from deltaY

    public bool CanClimb = false;

    public float accel_const = 0.6f;        // constant at which gravity increases while falling
    public float terminal_v = -100f;
    public float jumpforce = 22f;
    public float fallforce = 6f;
    public float crouchheight = 0.5f;       //crouch base height
    public float crouchspeed = 1f;
    float normalcrouchspeed = 1.5f;
    public float crouchupspeed = 10f;
    public float runspeed = 20f;
    public float swimspeed = 4f;
    float TransitionSpeed = 5f; // lerp time when pressing shift

    public float Timer = 1f;



    public Vector3 left;
    public Vector3 right;
    public Vector3 forward;
    public Vector3 back;

    public bool crouched = false;


    

    public float deltaY = 0;
    public bool duringjump = false;
    public bool spaceunpress = true;
    private CharacterController _charCont;
    private CharacterController characterController;
    private CapsuleCollider capCol;




    // Use this for initialization
    void Start()
    {
        anim = AnimatorGet.GetComponent<Animator> ();
        GunAnim = GunAnimatorGet.GetComponent<Animator> ();

        _charCont = GetComponent<CharacterController>();
        characterController = gameObject.GetComponent<CharacterController>();
        capCol = gameObject.GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        rb = GetComponent<Rigidbody>();

        // THIS LINE ALLOWS PLAYER TO ROTATE WITH CAMERA
        if (This.GetComponent<GrapplingHook>().Hooked == false)
        {
            This.transform.localEulerAngles = new Vector3(0.0f, CamReference.transform.localEulerAngles.y, 0.0f);
        }


        

        Vector3 center = transform.position + characterController.center;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.red);



//ANIMATOR
        float move = Input.GetAxis ("Vertical");
        if (!IsUnderWater)
            anim.SetFloat("Speed", move);
            GunAnim.SetFloat("Speed", move);



                
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        if (This.GetComponent<GrapplingHook>().Hooked == true)
            rb.AddForce(0, -1.5f, 0);

        if (This.GetComponent<GrapplingHook>().Hooked == true)
        {
            if (Input.GetButton("Jump"))
                rb.AddForce(0, 3.5f, 0);

            if (Input.GetButton("Crouch"))
                rb.AddForce(0, -10, 0);
        }



        if (Input.GetKey("w"))
            rb.AddRelativeForce(0, 0, 15);
        if (Input.GetKey("s"))
            rb.AddRelativeForce(0, 0, -15);

        if (Input.GetKey("d"))
            rb.AddRelativeForce(15, 0, 0);
        if (Input.GetKey("a"))
            rb.AddRelativeForce(-15, 0, 0);


        bool YoJump = true; //THIS WILL ENABLE THE ANIMATION ONLY THE INSTANCE YOU TOUCH THE GROUND
        if (gravity < -30f & !IsUnderWater)
        {
           tester = false; 
           anim.SetTrigger("Jumo");
           GunAnim.SetTrigger("Jumo");
        }
        else if (Grounded() || Grounded1() || Grounded2() || Grounded3() || Grounded4())
        {
            tester = true;
        }

        
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float deltaDive = Input.GetAxis("Dive") * speedDive;
        Vector3 movement = new Vector3(deltaX, deltaDive, deltaZ);


        // DIVE ANIMATION
        if (gameObject.transform.position.y < DiveValue)
        {
            characterController.height = crouchheight;
        }
        if (gameObject.transform.position.y < UnderWaterValue) 
        {
            anim.SetFloat("RunMultiplier", 0f);
            GunAnim.SetFloat("RunMultiplier", 0f);

            IsUnderWater = true;
            crouchupspeed = swimspeed;
            crouchspeed = swimspeed;
            duringjump = true;
            crouched = true;


            accel_const = (accel_const * 0.95f); //THIS IS RESPONSIBLE FOR THE DIVE
            if (accel_const < 0f)
            {
                accel_const = 0;
            }
            

          


    
            
            terminal_v = -100f;



            // the speed

            if (gameObject.transform.position.y > UnderWaterValue -1f)
            {
                speedDive = 0f;


            }
            else
            {
                speedDive = SwimUpDownSpeed;

            }
            if (Input.GetAxis("Crouch") == 1)
            {
                speedDive = SwimUpDownSpeed;
            }
            else
            {
                gravity = (gravity + 2f);
                if (gravity > -29f)
                {
                    gravity = -29;
                }
            



            

            }

        }
        else
        {
            //ADD A DURING JUMP HERE

            IsUnderWater = false;
            crouchupspeed = 5f;
            crouchspeed = normalcrouchspeed;
            accel_const = 1f;
            crouched = false;
            terminal_v = -100f;

            anim.SetFloat("RunMultiplier", 1.15f);

            if(Grounded())
            {
                GunAnim.SetFloat("RunMultiplier", 1.15f);
            }
            else
                GunAnim.SetFloat("RunMultiplier", 0f);
                




            if (gameObject.transform.position.y < UnderWaterValue + 1)
                {
                    duringjump = false;
                } 


        }


        if (Input.GetAxisRaw("Jump") == 1 && spaceunpress && !IsUnderWater && (Grounded() || Grounded1() || Grounded2() || Grounded3() || Grounded4()))
        {
            duringjump = true;
            spaceunpress = false;
            //ANIMATION
            anim.SetTrigger ("Jumpp");
            GunAnim.SetTrigger ("Jumpp");
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

        //CLIMB
        if (Climb() && Input.GetAxis("Jump") == 1 && CanClimb &&duringjump && (!Grounded() || !Grounded2() || !Grounded3() || !Grounded4()) && deltaY > -6f && Input.GetAxis("Vertical")== 1)
            {


                gravity = (gravity + 1.2f);

                Debug.Log("Climb and Jump OK");


            }
        if (Input.GetAxis("Jump") == 1 && duringjump && (!Grounded() || !Grounded2() || !Grounded3() || !Grounded4()) && deltaY > -4f)
        {

            Timer = (Timer -0.05f);
            if (Timer < 0f)
            {
                CanClimb = true;
            }
            else
            {
                CanClimb = false;
            }

            if (Timer < -3f) //TIME BEFORE STOPPING CLIMB
            {
                CanClimb = false;
            }
        }

        if (Input.GetKeyUp("space") || Input.GetKey(KeyCode.LeftShift)) //SO YOU CAN'T PRESS SPACE TWICE
        {
            CanClimb =false;
        }



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
        else if (Input.GetAxis("Crouch") == 0 & !IsUnderWater)
        {
            characterController.height = Mathf.Lerp(characterController.height, height, timespeed * Time.deltaTime);
            capCol.height = Mathf.Lerp(capCol.height, height, timespeed * Time.deltaTime);
            crouched = false;
            if (StuckPlayer() || Stuck1Player() & !IsUnderWater)        //checks if the character is under a surface
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

            if (gameObject.transform.position.y > UnderWaterValue - 1)
            {
                anim.SetFloat("RunMultiplier", 0.65f);
                GunAnim.SetFloat("RunMultiplier", 0.65f);
            }
        }
        else
        {

            speed = Mathf.Lerp(crouchupspeed, crouchspeed, timespeedslow * Time.deltaTime);
            
        }

        if ((!Grounded() && !Grounded1() && !Grounded2() && !Grounded3() && !Grounded4()) && Input.GetAxis("Crouch") == 1)    //if not on the ground and pressing "crouch" button
        {
            characterController.height = Mathf.Lerp(characterController.height, crouchheight, timespeed * Time.deltaTime);
            capCol.height = Mathf.Lerp(capCol.height, crouchheight, timespeed * Time.deltaTime);
            speed = crouchupspeed;
        }

        if (Grounded() || IsUnderWater)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;   // MOVE THIS 
            GetComponent<CharacterController>().enabled = true;
        }

        float CrouchCounter = 0f;
        //FOR ANIMATION
        if (Input.GetAxis("Crouch") == 1 && Grounded()) //maybe add other groundeds later
        {
            anim.SetTrigger("Crouch");
            GunAnim.SetTrigger("Crouch");

            if (characterController.height < 0.7f)
            {
                anim.ResetTrigger("Crouch");
                GunAnim.ResetTrigger("Crouch");
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runspeed;
            if (!crouched)
            {
                anim.SetFloat("RunMultiplier", 1.75f);
                GunAnim.SetFloat("RunMultiplier", 0f);
            }
            if (StuckPlayer() || crouched) //|| is OR logic
            {
                speed = crouchspeed;
            }
            if (crouched && !Grounded() && !Grounded1() && !Grounded2() && !Grounded3() && !Grounded4())
            {
                speed = runspeed;
            }
        }
    }


    bool Climb()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 1f); //CLIMB
    }
    

    bool Grounded()                     // returns true if on the ground
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f);
    }
    bool Grounded1()
    {
        return Physics.Raycast(transform.position + left, transform.TransformDirection(Vector3.down), SideRaycasts);
    }
    bool Grounded2()
    {
        return Physics.Raycast(transform.position + right, transform.TransformDirection(Vector3.down), SideRaycasts);
    }
    bool Grounded3()
    {
        return Physics.Raycast(transform.position + forward, transform.TransformDirection(Vector3.down), SideRaycasts);
    }
    bool Grounded4()
    {
        return Physics.Raycast(transform.position + back, transform.TransformDirection(Vector3.down), SideRaycasts);
    }



    void DynGravity()                   // function for dynamic gravity, its acceleration
    {
        if (Grounded() || Grounded1() || Grounded2() || Grounded3() || Grounded4() & !IsUnderWater)
        {
            Debug.Log("Grounded");
            gravity = -10f;
            CanClimb = true;
            Timer = 1f;
        }
        else
        {
            if (deltaY < terminal_v)
                deltaY = terminal_v;
            else
                gravity -= accel_const;
        }
    }
    public bool StuckPlayer()   //returns true if the distance between the head of the character and the collider is less that green value
    {
        RaycastHit hit;
        Vector3 center = transform.position + characterController.center;
        return Physics.SphereCast(center, 0.5f, transform.TransformDirection(Vector3.up), out hit, 2f); //RAYCAST DISTANCE
    }
    public bool Stuck1Player()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 2f);
    }

    
}