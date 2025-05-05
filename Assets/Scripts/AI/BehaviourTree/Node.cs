using System.Collections.Generic;

namespace AI.BehaviourTree {
    public class BehaviourTree : Node {
        public BehaviourTree(string name) : base(name) { }

        public override Status Process() {
            while (CurrentChild < Children.Count) {
                var status = Children[CurrentChild].Process();

                if (status != Status.Success) {
                    return status;
                }

                CurrentChild++;
            }

            return Status.Success;
        }
    }
    
    public class Leaf : Node {
        public IStrategy strategy { get; set; }

        public Leaf(string name, IStrategy strategy) : base(name) {
            this.strategy = strategy;
        }

        public override Status Process() => strategy.Process();
        
        public override void Reset() => strategy.Reset();
        
    }
    
    public class Node {
        public enum Status { Success, Failure, Running }

        public readonly string Name;

        public List<Node> Children = new();
        protected int CurrentChild;

        public Node(string name = "Node") {
            this.Name = name;
        }

        public void AddChild(Node child) => Children.Add(child);
        
        public virtual Status Process() => Children[CurrentChild].Process();
        
        public virtual void Reset() {
            CurrentChild = 0;
        }
        
        
        
        
    }
}
