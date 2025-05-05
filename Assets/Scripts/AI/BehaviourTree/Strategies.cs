using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.BehaviourTree {
    public interface IStrategy {
        Node.Status Process();
        void Reset();
    }

    public class MoveStrategy : IStrategy {
        private Transform target;
        private readonly NavMeshAgent agent;
        private readonly float attackRange;
        private bool isPathCalculated;
        
        public MoveStrategy(NavMeshAgent agent, Transform target,float attackRange) {
            this.agent = agent;
            this.target = target;
            this.attackRange = attackRange;
        }

        
        public Node.Status Process() {
            Debug.Log(target);
            if (target == null) {
                return Node.Status.Failure;
            }
            
            if(CheckDistance()) {
                return Node.Status.Success;
            }

            agent.SetDestination(target.position);
            
            return Node.Status.Running;
        }

        public void Reset() {
            target = null;
        }
        
        private bool CheckDistance() {
            var distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
            return distanceToTarget < attackRange;
        }
        
    }
    
    
}