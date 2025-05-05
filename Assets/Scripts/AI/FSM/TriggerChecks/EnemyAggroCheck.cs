using UnityEngine;

namespace AI.FSM.TriggerChecks {
    public class EnemyAggro : MonoBehaviour
    {
        private GameObject target { get; set; }
        private Enemy enemy;

        private void Awake() {
            target = GameObject.FindGameObjectWithTag("Player");
            enemy = GetComponentInParent<Enemy>();
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Aggro check trigger enter");
            if (other.gameObject == target) {
                enemy.SetAggroStatus(true);
            }
        }

        private void OnTriggerExit(Collider other) {
            Debug.Log("Aggro check trigger exit");

            enemy.SetAggroStatus(false);
        }
    }
}
