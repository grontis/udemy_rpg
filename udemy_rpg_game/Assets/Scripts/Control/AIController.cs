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
        private GameObject _player;
        private Health _health;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();
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
                _fighter.Cancel();
            }
        }
        

        private bool InAttackRange()
        {
            float v = Vector3.Distance(transform.position, _player.transform.position);
            return v < chaseDistance;
        }
    }
}
