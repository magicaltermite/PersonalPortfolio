using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    private PlayerControls playerControls;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }

        playerControls = new PlayerControls();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement() {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetMouseDelta() {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame() {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerPressedShoot() {
        return playerControls.Player.Shoot.triggered;
    }
    
    public bool PlayerPressedReload() {
        return playerControls.Player.Reload.triggered;
    }

    public bool PlayerPressedPause() {
        return playerControls.Player.Pause.triggered;
    }

    public bool PlayerIsSprinting() {
        return playerControls.Player.Sprint.triggered;
    }
}
