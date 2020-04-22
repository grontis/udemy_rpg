using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_health.GetIsDead()) { return; }
            if(InteractWithCombat()) { return; }
            if(InteractWithMovement()) { return; }
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<CombatTarget>())
                {
                    CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                    if(target == null) {continue;}
                    
                    if (target == null) continue;
                    if(!GetComponent<Fighter>().CanAttack(target.gameObject)) {continue;}
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target.gameObject);
                    } 
                    
                    return true;
                }
            }
            
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            //RaycastHit hit is filled from out hit
            bool hasHit = Physics.Raycast(GetMouseRay(),out hit);

            //if there is a hit, move to hit point
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
