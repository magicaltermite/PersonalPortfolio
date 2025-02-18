using UnityEngine;

namespace AI.StateMachine {
public abstract class FSMAction : ScriptableObject {
    public abstract void Execute(BaseStateMachine stateMachine);
}
}