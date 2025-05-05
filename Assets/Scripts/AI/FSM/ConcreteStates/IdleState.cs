using System.Diagnostics;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace AI.FSM.ConcreteStates {
    public class IdleState : EnemyState {
        private new readonly NavMeshAgent agent;
        
        public IdleState(Enemy enemy, EnemyStateMachine stateMachine, NavMeshAgent agent) : base(enemy, stateMachine, agent) {
            this.agent = agent;
        }

        public override void EnterState() {
            agent.isStopped = true;
            agent.destination = agent.transform.position;
        }

        public override void ExitState() {
            agent.isStopped = false;
        }

        public override void FrameUpdate() {
            if (enemy.isAggroed) {
                enemy.stateMachine.ChangeState(enemy.chaseState);
            }
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