using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveToCursor is called when mousebutton is down
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        //gets ray based on mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //RaycastHit hit is filled from out hit
        bool hasHit = Physics.Raycast(ray,out hit);

        //if there is a hit, move to hit point
        if (hasHit)
        {
            navMeshAgent.destination = hit.point;
        }
    }

    private void UpdateAnimator()
    {
        //global velocity
        Vector3 velocity = navMeshAgent.velocity;
        
        //take from global and move to local
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;
        
        //sets the forwardSpeed parameter for the animator equal to speed
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);            
    }
}
