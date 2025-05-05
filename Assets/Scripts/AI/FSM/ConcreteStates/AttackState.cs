using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM.ConcreteStates {
    public class AttackState : EnemyState{
        private Transform targetTransform;
        private float attackSpeed = 1.0f;
        private float damage = 10f;

        private float timer;
        private bool canAttack = true;
        public AttackState(Enemy enemy, EnemyStateMachine stateMachine, NavMeshAgent agent) : base(enemy, stateMachine, agent) {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void EnterState() {
            base.EnterState();
        }

        public override void ExitState() {
        }

        public override void FrameUpdate() {
            if (!enemy.isWithinStrikingDistance) {
                Debug.Log("Why do you not exit correctly");
                enemy.stateMachine.ChangeState(enemy.chaseState);
            }
            
            Attack();

            if (timer > attackSpeed) {
                timer = 0f;
                canAttack = true;
            }
            
            timer += Time.deltaTime;
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return base.ToString();
        }
        
        private void Attack() {
            if(!canAttack) return;

            var damageable = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamagable>();
            damageable?.Damage(damage);
            
            canAttack = false;
        }
        
        
    }
}