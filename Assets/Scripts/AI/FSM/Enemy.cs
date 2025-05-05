using System;
using AI.FSM.ConcreteStates;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM {
    public class Enemy : MonoBehaviour, ITriggerCheckable {
        public bool isAggroed { get; set; }
        public bool isWithinStrikingDistance { get; set; }
        

        public EnemyStateMachine stateMachine;
        
        public IdleState idleState;
        public ChaseState chaseState;
        public AttackState attackState;
        
        private NavMeshAgent agent;
        
        private void Awake() {
            stateMachine = new EnemyStateMachine();
            agent = GetComponent<NavMeshAgent>();
            
            idleState = new IdleState(this, stateMachine, agent);
            chaseState = new ChaseState(this, stateMachine, agent);
            attackState = new AttackState(this, stateMachine, agent);
        }

        private void Start() {
            stateMachine.Initialize(idleState);
        }

        private void Update() {
            stateMachine.currentState.FrameUpdate();
        }

        private void FixedUpdate() {
            stateMachine.currentState.PhysicsUpdate();
        }


        #region Distance Checks
        
        public void SetAggroStatus(bool isAggroed) {
            this.isAggroed = isAggroed;
        }

        public void SetStrikingDistance(bool isWithinStrikingDistance) {
            this.isWithinStrikingDistance = isWithinStrikingDistance;
        }
        
        #endregion
        
        
    }
}