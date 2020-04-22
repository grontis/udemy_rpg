using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _navMeshAgent;
        private Health _health;
    
        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            _navMeshAgent.enabled = !_health.GetIsDead();
            
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            //global velocity
            Vector3 velocity = _navMeshAgent.velocity;
        
            //take from global and move to local
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
        
            //sets the forwardSpeed parameter for the animator equal to speed
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);            
        }
    }
}