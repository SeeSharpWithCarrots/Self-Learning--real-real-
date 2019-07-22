using UnityEngine;
using System.Collections;
// TO KEEP IN MIND: When merging, make sure there are no conflicting Vector 3s. Else: camera vibrates a bit
public class lean : MonoBehaviour
{
    private Vector3 newPosition;
    public float Leantime = 3f;
    public float LeanUp = 5f;

    void Awake()
    {
        newPosition = transform.localPosition;
    }

    void Update ()
    {
        PositionChanging();
    }

    void PositionChanging ()
    {
        if (Input.GetAxis("Crouch") == 0)
        {
            Vector3 positionA = new Vector3(-3.5f, 1.8f, 0); //how much you can lean Q 
            Vector3 positionB = new Vector3(0, 1.8f, 0); //neutral position                //actually the Y value of all of these should be the camera height. When we merge there won't be glitches
            Vector3 positionC = new Vector3(3.5f, 1.8f, 0); //how much you can lean E

            Vector3 positionE = new Vector3(0, 2.5f, 0); //ALT lean 
            // start of Q lean
            if (Input.GetKey(KeyCode.Q)) //later we change this to an input value
                newPosition = positionA;
            else
                newPosition = positionB;

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Leantime * Time.deltaTime);
            // start of E lean
            if (Input.GetKey(KeyCode.E)) //change this to input value
                newPosition = positionC;
            else
                newPosition = positionB;
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Leantime * Time.deltaTime);
            //start of ALT lean
            if (Input.GetKey(KeyCode.LeftAlt))
                newPosition = positionE;
            else
                newPosition = positionB;

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Leantime * Time.deltaTime);
        }
        else
           {
            Vector3 positionA = new Vector3(-5.2f, 0.5f, 0); //how much you can lean Q 
            Vector3 positionB = new Vector3(0, 0.5f, 0); //neutral position                //actually the Y value of all of these should be the camera height. When we merge there won't be glitches
            Vector3 positionC = new Vector3(5.2f, 0.5f, 0); //how much you can lean E

            Vector3 positionE = new Vector3(0, LeanUp, 0); //ALT lean 
                                                         // start of Q lean
            if (Input.GetKey(KeyCode.Q)) //later we change this to an input value
                newPosition = positionA;
            else
                newPosition = positionB;

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Leantime * Time.deltaTime);
            // start of E lean
            if (Input.GetKey(KeyCode.E)) //change this to input value
                newPosition = positionC;
            else
                newPosition = positionB;
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Leantime * Time.deltaTime);
            //start of ALT lean
            if (Input.GetKey(KeyCode.LeftAlt))
                newPosition = positionE;
            else
                newPosition = positionB;

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Leantime * Time.deltaTime);
        }

    }
}