using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AI.GOAP {

    public interface IGoapPlanner {
        ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
    }
    
    public class GoapPlanner : IGoapPlanner {
        public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null) {
            // Order goals by priority, descending
            var orderedGoals = goals
                .Where(g => g.desiredEffects.Any(b => !b.Evaluate()))
                .OrderByDescending(g => g == mostRecentGoal ? g.priority - 0.01 : g.priority)
                .ToList();

            foreach (var goal in orderedGoals) {
                var goalNode = new NodeGoap(null, null, goal.desiredEffects, 0);
                
                // if we can find a path to the goal return the plan
                if (FindPath(goalNode, agent.actions)) {
                    if (goalNode.isLeafDead) continue;
                    
                    var actionStack = new Stack<AgentAction>();
                    while (goalNode.leaves.Count > 0) {
                        var cheapestLeaf = goalNode.leaves.OrderBy(leaf => leaf.cost).First();
                        goalNode = cheapestLeaf;
                        actionStack.Push(cheapestLeaf.action);
                    }

                    return new ActionPlan(goal, actionStack, goalNode.cost);
                }
            }
            Debug.Log("No plan found");
            return null;
        }

        private bool FindPath(NodeGoap parent, HashSet<AgentAction> actions) {
            foreach (var action in actions) {
                var requiredEffects = parent.requiredEffects;
                
                // remove any that evaluate to ture, there is no action to take
                requiredEffects.RemoveWhere(b => b.Evaluate());
                
                // if there are no required effects to fulfill we have a plan
                if (requiredEffects.Count == 0) {
                    return true;
                }

                if (action.effects.Any(requiredEffects.Contains)) {
                    var newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
                    newRequiredEffects.ExceptWith(action.effects);
                    newRequiredEffects.UnionWith(action.preconditions);

                    var newAvailableActions = new HashSet<AgentAction>(actions);
                    newAvailableActions.Remove(action);

                    var newNode = new NodeGoap(parent, action, newRequiredEffects, parent.cost + action.cost);
                    
                    // Explore the new node recursively
                    if (FindPath(newNode, newAvailableActions)) {
                        parent.leaves.Add(newNode);
                        newRequiredEffects.ExceptWith(newNode.action.preconditions);
                    }
                    
                    // If all effects at this depth have been satisfied, return true
                    if (newRequiredEffects.Count == 0) {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class NodeGoap {
        public NodeGoap parent { get; }
        public AgentAction action { get; }
        public HashSet<AgentBelief> requiredEffects { get; }
        public List<NodeGoap> leaves { get; }
        public float cost { get; }
        
        public bool isLeafDead => leaves.Count == 0 && action == null;

        public NodeGoap(NodeGoap parent, AgentAction action, HashSet<AgentBelief> requiredEffects, float cost) {
            this.parent = parent;
            this.action = action;
            this.requiredEffects = new HashSet<AgentBelief>(requiredEffects);
            leaves = new List<NodeGoap>();
            this.cost = cost;
        }
    }
    
    public class ActionPlan {
        public AgentGoal agentGoal { get; }
        public Stack<AgentAction> actions { get; }
        public float totalCost { get; set; }

        public ActionPlan(AgentGoal agentGoal, Stack<AgentAction> actions, float totalCost) {
            this.agentGoal = agentGoal;
            this.actions = actions;
            this.totalCost = totalCost;
        }
    }
    
    
    
}