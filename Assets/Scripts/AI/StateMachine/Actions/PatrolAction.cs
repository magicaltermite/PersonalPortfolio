using UnityEngine;
using UnityEngine.AI;

namespace AI.StateMachine.Actions {
[CreateAssetMenu(menuName = "FSA/Actions/Patrol")]
public class PatrolAction : FSMAction {
    public override void Execute(BaseStateMachine stateMachine) {
        var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        var patrolPoints = stateMachine.GetComponent<PatrolPoints>();

        if (patrolPoints.HasReached(navMeshAgent)) {
            navMeshAgent.SetDestination(patrolPoints.GetNext().position);
        }
    }
}
}