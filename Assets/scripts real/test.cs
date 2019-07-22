using UnityEngine;

public class test : MonoBehaviour
{
    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OK WE ARE IN");
    }
}