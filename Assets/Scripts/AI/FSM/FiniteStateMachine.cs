using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM {
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField] private AIState currentState;
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackSpeed = 1.0f;
        
        private NavMeshAgent agent;
        private GameObject target;
        private float attackRange = 1.5f;
        private bool isAttacking;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
        }
        
        private void OnTriggerStay(Collider other) {
            Debug.Log("Trigger entered");
            target = other.gameObject;
            currentState = AIState.Walking;
            SwitchState(currentState);
            CheckDistance();
        }

        private void OnTriggerExit(Collider other) {
            Debug.Log("Trigger exited");
            target = null;
            currentState = AIState.Idle;
            SwitchState(currentState);
        }

        private void SwitchState(AIState newState) {
            switch (newState) {
                case AIState.Idle:
                    SetIdleState();
                    break;
                case AIState.Walking:
                    CalculateMovement(target);
                    break;
                case AIState.Attacking:
                    if(!isAttacking) {
                        Attack();
                        StartCoroutine(nameof(Wait));
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetIdleState() {
            agent.isStopped = true;
        }

        private void CalculateMovement(GameObject targetToAttack) {
            agent.isStopped = false;
            agent.destination = targetToAttack.transform.position;
        }

        private void Attack() {
            if(isAttacking) return;
            var damageable = target.GetComponent<IDamagable>();
            damageable?.Damage(damage);
        }

        private void CheckDistance() {
            var distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
            currentState = distanceToTarget < attackRange ? AIState.Attacking : AIState.Walking;

            SwitchState(currentState);
        }

        private IEnumerator Wait() {
            isAttacking = true;
            Debug.Log("before wait");
            yield return new WaitForSeconds(attackSpeed);
            Debug.Log("after wait");
            isAttacking = false;
        }


       
    }

    public enum AIState {
        Idle,
        Walking,
        Attacking
    }
}