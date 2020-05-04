using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private NavMeshAgent _navMeshAgent;
        private Fighter _fighter;
        private Mover _mover;
        private GameObject _player;
        private Health _health;

        private Vector3 _guardLocation;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();

            _guardLocation = transform.position;
        }

        private void Update()
        {
            if(_health.GetIsDead()) { return; }
            
            if (InAttackRange() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                //stop fighting, and move back to starting guard position
                _mover.StartMoveAction(_guardLocation);
            }
        }
        

        private bool InAttackRange()
        {
            float v = Vector3.Distance(transform.position, _player.transform.position);
            return v < chaseDistance;
        }

        //Called by Unity Editor
        private void OnDrawGizmosSelected()
        {
            //Gizmo for representing the enemy AI chase radius
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
