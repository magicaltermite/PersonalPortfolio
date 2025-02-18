using UnityEngine.AI;

namespace AI.StateMachine.Actions {
public class ChaseAction : FSMAction {
    public override void Execute(BaseStateMachine stateMachine) {
        var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();

        navMeshAgent.SetDestination(enemySightSensor.Player.setPosition);
    }
}
}