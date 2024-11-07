using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CinemachinePOVExtenstion : CinemachineExtension {
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float horizontalSensitivity = 10f; 
    [SerializeField] private float verticalSensitivity = 10f;
    
    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake() {
        inputManager = InputManager.Instance;
        startingRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if (!vcam.Follow) return;
        
        if (stage == CinemachineCore.Stage.Aim) {
            Vector2 deltaInput = inputManager.GetMouseDelta();
            
            startingRotation.x += deltaInput.x * verticalSensitivity * Time.deltaTime;
            startingRotation.y += deltaInput.y * horizontalSensitivity * Time.deltaTime;
            startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
            
            state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
        }
    }
}
