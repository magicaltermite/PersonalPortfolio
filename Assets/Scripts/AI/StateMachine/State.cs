using System.Collections.Generic;
using UnityEngine;

namespace AI.StateMachine {
[CreateAssetMenu(menuName = "FSM/State")]
public class State : BaseState {
    public List<FSMAction> actions = new List<FSMAction>();
    public List<Transition> transitions = new List<Transition>();

    public override void Execute(BaseStateMachine stateMachine) {
        foreach (var action in actions) {
            action.Execute(stateMachine);
        }

        foreach (var transition in transitions) {
            transition.Execute(stateMachine);
        }
    }
}
}