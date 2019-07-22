using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //get navmesh

public class NavFollow : MonoBehaviour
{
    public GameObject target = null;
    private NavMeshAgent nma = null;
    public float speed;

    private void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        nma.SetDestination(target.transform.position);
    }
} 