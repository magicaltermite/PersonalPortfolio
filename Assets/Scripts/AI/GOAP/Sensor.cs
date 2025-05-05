using System;
using System.Timers;
using UnityEngine;

namespace AI.GOAP {
    [RequireComponent(typeof(SphereCollider))]
    public class Sensor : MonoBehaviour {
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private float timerInterval = 1f;
        
        public event Action OnTargetChanged = delegate { };
        public Vector3 TargetPosition => target ? target.transform.position : Vector3.zero;
        public bool IsTargetInRage => TargetPosition != Vector3.zero;
        
        private SphereCollider detectionRange;
        private GameObject target;
        private Vector3 lastKnownPosition;
        private CountdownTimer timer;

        private void Awake() {
            detectionRange = GetComponent<SphereCollider>();
            detectionRange.isTrigger = true;
            detectionRange.radius = detectionRadius;
        }

        private void Start() {
            timer = new CountdownTimer(timerInterval);
            timer.OnTimerStop += () => {
                UpdateTargetPosition(target.OrNull());
                timer.Start();
            };
            timer.Start();
        }

        private void Update() {
            timer.Tick(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            if(!other.CompareTag("Player")) return;
            UpdateTargetPosition(other.gameObject);
        }

        private void OnTriggerExit(Collider other) {
            if(!other.CompareTag("Player")) return;
            UpdateTargetPosition();
        }

        void UpdateTargetPosition(GameObject target = null) {
            this.target = target;

            if (IsTargetInRage && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.zero)) {
                lastKnownPosition = TargetPosition;
                OnTargetChanged.Invoke();
            }
        }
    }
}