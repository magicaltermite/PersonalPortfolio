using UnityEngine;

namespace AI.StateMachine {
[CreateAssetMenu(menuName = "FSM/Transition")]
public class Transition : ScriptableObject {
    public Decision decision;
    public BaseState trueState;
    public BaseState falseState;

    public void Execute(BaseStateMachine stateMachine) {
        if (decision.Decide(stateMachine) && trueState is not RemainInState) {
            stateMachine.CurrentState = trueState;
        } else if (falseState is not RemainInState) {
            stateMachine.CurrentState = falseState;
        }
    }

}
}