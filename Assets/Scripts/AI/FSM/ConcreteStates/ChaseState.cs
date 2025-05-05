using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM.ConcreteStates {
    public class ChaseState : EnemyState {
        private Transform targetTransform;
        private float movementSpeed = 1.75f;
        private new NavMeshAgent agent;
        
        
        public ChaseState(Enemy enemy, EnemyStateMachine stateMachine, NavMeshAgent agent) : base(enemy, stateMachine, agent) {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            this.agent = agent;
            agent.speed = movementSpeed;
        }

        public override void EnterState() {
            agent.isStopped = false;
        }

        public override void ExitState() {
            agent.isStopped = true;
        }

        public override void FrameUpdate() {
            if (!enemy.isAggroed) {
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
            
            if (enemy.isWithinStrikingDistance) {
                Debug.Log("aggro state enter");
                enemy.stateMachine.ChangeState(enemy.attackState);
            }
            
            agent.destination = targetTransform.position;
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
    }
}