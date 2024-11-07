using System;
using UnityEngine;

namespace Weapons {
public class PlayerShoot : MonoBehaviour {

    public static Action ShootInput;
    public static Action ReloadInput;

    private InputManager inputManager;

    private void Start() {
        inputManager = InputManager.Instance;
    }

    private void Update() {
        if (inputManager.PlayerPressedShoot()) {
            ShootInput?.Invoke();
        }

        if (inputManager.PlayerPressedReload()) {
            ReloadInput?.Invoke();
        }
    }
}
}
