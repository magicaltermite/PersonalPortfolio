using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GunData gunData;
    [SerializeField] private TMP_Text ammoCounter;
    
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
    }

    public void SetAmmo() {
        Debug.Log("Ammo has been set");
        ammoCounter.text = "Ammo:" + gunData.currentAmmo.ToString();
    }
}
