using UnityEngine;

namespace AI.FSM.TriggerChecks {
    public class StrikingDistanceCheck : MonoBehaviour {
        private GameObject target { get; set; }
        private Enemy enemy;

        private void Awake() {
            target = GameObject.FindGameObjectWithTag("Player");
            enemy = GetComponentInParent<Enemy>();
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("striking distance check trigger enter");

            if (other.gameObject == target) {
                enemy.SetStrikingDistance(true);
            }
        }

        private void OnTriggerExit(Collider other) {
            Debug.Log("striking distance check trigger exit");

            enemy.SetStrikingDistance(false);
        }
    }
}
