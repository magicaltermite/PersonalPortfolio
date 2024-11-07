using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private InputManager inputManager;

    [Header("In Game UI")]
    [SerializeField] private GunData gunData;
    [SerializeField] private TMP_Text ammoCounter;

    [Header("Pause Menu")] 
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        SetAmmo();
        inputManager = InputManager.Instance;
        
        resumeButton.onClick.AddListener(() => OnPause(false));
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void Update() {
        if (inputManager.PlayerPressedPause()) {
            OnPause(true);
        }
    }

    public void SetAmmo() {
        ammoCounter.text = "Ammo:" + gunData.currentAmmo;
    }

    public void OnPause(bool isPaused) {
        pauseMenu.SetActive(isPaused);

        if (isPaused) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    
    public void OnQuitButtonClick() {
        Debug.Log("Application has been quit");
        Application.Quit();
    }
}
