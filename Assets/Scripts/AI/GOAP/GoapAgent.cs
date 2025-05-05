using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace AI.GOAP {
    [RequireComponent(typeof(NavMeshAgent))]
    public class GoapAgent : MonoBehaviour {
        [Header("Sensors")] 
        [SerializeField] private Sensor chaseSensor;
        [SerializeField] private Sensor attackSensor;
        
        private NavMeshAgent agent;
        private GameObject target;
        private Vector3 destination;

        private AgentGoal lastGoal;
        private AgentGoal currentGoal;
        public ActionPlan actionPlan;
        private AgentAction currentAction;
        
        private Dictionary<string, AgentBelief> beliefs;
        public HashSet<AgentAction> actions;
        public HashSet<AgentGoal> goals;

        private IGoapPlanner gPlanner;

        private void Awake() {
            agent = GetComponent<NavMeshAgent>();
            
            gPlanner = new GoapPlanner();
        }

        private void Start() {
            SetupBeliefs();
            SetupActions();
            SetupGoals();
        }

        private void SetupBeliefs() {
            beliefs = new Dictionary<string, AgentBelief>();
            var factory = new BeliefFacotry(this, beliefs);
            
            factory.AddBelief("Nothing", () => false);
            factory.AddBelief("Idle", () => !agent.hasPath);
            factory.AddBelief("Moving", () => agent.hasPath);
        }
        
        
        private void SetupActions() {
            actions = new HashSet<AgentAction>();

            actions.Add(new AgentAction.Builder("Relax")
                .WithStrategy(new IdleStrategy(5))
                .AddEffect(beliefs["Nothing"])
                .Build());
            
            actions.Add(new AgentAction.Builder("Wander Around")
                .WithStrategy(new WanderStrategy(agent, 10))
                .AddEffect(beliefs["Moving"])
                .Build());

            
            
        }

        private void SetupGoals() {
            goals = new HashSet<AgentGoal>();

            goals.Add(new AgentGoal.Builder("Chill Out")
                .WithPriority(1).WithDesiredEffects(beliefs["Nothing"])
                .Build());
            
            goals.Add(new AgentGoal.Builder("Wander")
                .WithPriority(1)
                .WithDesiredEffects(beliefs["Moving"])
                .Build());
        }

        

        void OnEnable() => chaseSensor.OnTargetChanged += HandleTargetChanged;
        void OnDisable() => chaseSensor.OnTargetChanged += HandleTargetChanged;

        private void HandleTargetChanged() {
            Debug.Log("Target changed, clearing current action and goal");
            
            // Force the planner to re-evaluate the plan
            currentAction = null;
            currentGoal = null;
        }

        private void Update() {
            if (currentAction == null) {
                Debug.Log("Calculating any potential new plan");
                CalculatePlan();

                if (actionPlan != null && actionPlan.actions.Count > 0) {
                    agent.ResetPath();

                    currentGoal = actionPlan.agentGoal;
                    Debug.Log($"Goal: {currentGoal.name} with {actionPlan.actions.Count} actions in plan");
                    currentAction = actionPlan.actions.Pop();
                    Debug.Log($"Popped action: {currentAction.name}");
                    currentAction.Start();
                    
                }
            }
            
            // If we have a current action, execute it
            if (actionPlan != null && currentAction != null) {
                currentAction.Update(Time.deltaTime);

                if (currentAction.Complete) {
                    Debug.Log($"{currentAction.name} completed");
                    currentAction.Stop();

                    if (actionPlan.actions.Count == 0) {
                        Debug.Log("Plan Completed");
                        lastGoal = currentGoal;
                        currentGoal = null;
                    }
                }
            }
        }

        private void CalculatePlan() {
            var priorityLevel = currentGoal?.priority ?? 0;

            var goalsToCheck = goals;

            if (currentGoal != null) {
                Debug.Log("current goal exists, checking goals with higher priority");
                goalsToCheck = new HashSet<AgentGoal>(goals.Where(g => g.priority > priorityLevel));
            }

            var potentialPlan = gPlanner.Plan(this, goalsToCheck, lastGoal);
            if (potentialPlan != null) {
                actionPlan = potentialPlan;
            }
        }
    }
}