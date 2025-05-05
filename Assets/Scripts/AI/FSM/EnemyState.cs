using UnityEngine.AI;

namespace AI.FSM {
    public class EnemyState {
        protected Enemy enemy { get; }
        protected EnemyStateMachine stateMachine { get; }
        protected NavMeshAgent agent { get; }

        public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, NavMeshAgent agent) {
            this.enemy = enemy;
            this.stateMachine = stateMachine;
            this.agent = agent;
        }
        
        public virtual void EnterState() {}
        public virtual void ExitState() {}
        public virtual void FrameUpdate() {}
        public virtual void PhysicsUpdate() {}
    }
}