using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [FormerlySerializedAs("health")] [SerializeField] float healthPoints = 100f;

        private Animator _animator;
        private ActionScheduler _actionScheduler;

        private bool isDead = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        public bool GetIsDead()
        {
            return isDead;
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public void TakeDamage(float damage)
        {
            //if the damage reduces health below 0, 0 will be new value
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            if (healthPoints == 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) { return; }

            isDead = true;
            _animator.SetTrigger("die");
            _actionScheduler.CancelCurrentAction();
        }
    }
}