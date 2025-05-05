using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI.BehaviourTree {
    public class EnemyController_BT : MonoBehaviour
    {
        private BehaviourTree tree;
        private float attackRange = 1.5f;


        private void Awake() {
            tree = new BehaviourTree("Enemy");
        }
        

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Trigger enter" + other.name);
            tree.AddChild(new Leaf("Approach", new MoveStrategy(GetComponent<NavMeshAgent>(), other.transform, attackRange)));
        }

        private void OnTriggerStay(Collider other) {
            Debug.Log("Trigger Stay");

            tree.Process();
        }

        private void OnTriggerExit(Collider other) {
            Debug.Log("Trigger Exit");

            tree.Reset();
        }
    }
}
