using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace AI.GOAP {
    public interface IActionStrategy {
        bool CanPerform { get; }
        bool Complete { get; }

        void Start() {
            // noop
        }

        void Update(float deltatime) {
            // noop
        }

        void Stop() {
            // noop
        }
    }

    public class IdleStrategy : IActionStrategy {
        public bool CanPerform => true;
        public bool Complete { get; private set; }

        private readonly CountdownTimer timer;

        public IdleStrategy(float duration) {
            timer = new CountdownTimer(duration);
            timer.OnTimerStart += ( )=> Complete = false;
            timer.OnTimerStop += ( )=> Complete = true;
        }

        public void Start() => timer.Start();
        public void Update(float deltaTime) => timer.Tick(deltaTime);
    }

    public class WanderStrategy : IActionStrategy {
        private readonly NavMeshAgent agent;
        private readonly float WanderRadius;
        
        public bool CanPerform => !Complete;
        public bool Complete => agent.remainingDistance <= 2f && agent.pathPending;

        public WanderStrategy(NavMeshAgent agent, float wanderRadius) {
            this.agent = agent;
            WanderRadius = wanderRadius;
        }

        public void Start() {
            for (int i = 0; i < 5; i++) {
                Vector3 randomDirection = (UnityEngine.Random.insideUnitCircle * WanderRadius);
                NavMeshHit hit;

                if (NavMesh.SamplePosition(agent.transform.position + randomDirection, out hit, WanderRadius, 1)) {
                    agent.SetDestination(hit.position);
                    return;
                }
            }
        }
    }
}