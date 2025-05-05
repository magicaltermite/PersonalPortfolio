using System.Collections.Generic;

namespace AI.GOAP {
    public class AgentAction {
        public string name { get; }
        public float cost { get; private set; }

        public HashSet<AgentBelief> preconditions { get; } = new();
        public HashSet<AgentBelief> effects { get; } = new();

        private IActionStrategy strategy;
        
        public bool Complete => strategy.Complete;
        
        public void Start() => strategy.Start();


        public void Update(float deltatime) {
            // check if action can be performed and update the strategy
            if (strategy.CanPerform) {
                strategy.Update(deltatime);
            }
            
            // Bail out if the strategy is still executing
            if (!strategy.Complete) return;
            
            // Apply effects
            foreach (var effect in effects) {
                effect.Evaluate();
            }
        }
        
        public void Stop() => strategy.Stop();

        public class Builder {
            private readonly AgentAction action;

            public Builder(string name) {
                action = new AgentAction {
                    cost = 1
                };
            }

            public Builder WithCost(float cost) {
                action.cost = cost;
                return this;
            }

            public Builder WithStrategy(IActionStrategy strategy) {
                action.strategy = strategy;
                return this;
            }

            public Builder AddPrecondition(AgentBelief precondition) {
                action.preconditions.Add(precondition);
                return this;
            }

            public Builder AddEffect(AgentBelief effect) {
                action.effects.Add(effect);
                return this;
            }

            public AgentAction Build() {
                return action;
            }
            
        }
    }

    
}