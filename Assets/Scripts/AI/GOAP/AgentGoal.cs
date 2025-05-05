using System.Collections.Generic;

namespace AI.GOAP {
    public class AgentGoal {
        public string name { get; }
        public float priority { get; private set; }
        public HashSet<AgentBelief> desiredEffects { get; } = new();

        public AgentGoal(string name) {
            this.name = name;
        }
        
        public class Builder {
            private readonly AgentGoal goal;

            public Builder(string name) {
                goal = new AgentGoal(name);
            }

            public Builder WithPriority(float priority) {
                goal.priority = priority;
                return this;
            }

            public Builder WithDesiredEffects(AgentBelief desiredEffects) {
                goal.desiredEffects.Add(desiredEffects);
                return this;
            }

            public AgentGoal Build() {
                return goal;
            }
        }
    }
}