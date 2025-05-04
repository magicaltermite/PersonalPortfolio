using System;
using UnityEngine;

namespace Weapons {
public class PlayerShoot : MonoBehaviour {

    public static Action shootInput;
    public static Action reloadInput;

    private InputManager inputManager;

    private void Start() {
        inputManager = InputManager.Instance;
    }

    private void Update() {
        if (inputManager.PlayerPressedShoot()) {
            shootInput?.Invoke();
        }

        if (inputManager.PlayerPressedReload()) {
            reloadInput?.Invoke();
        }
    }
}
}
