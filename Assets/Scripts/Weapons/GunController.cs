using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;


public class GunController : MonoBehaviour {

    [SerializeField] private GunData gunData;
    [SerializeField] private GameObject camera;
    [SerializeField] private LayerMask playerMask;

    private float timeSinceLastShot;
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    
    private void Start() {
        gunData.currentAmmo = 6;

        PlayerShoot.ShootInput += ShootGun;
        PlayerShoot.ReloadInput += StartReloadGun;
    }
    

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
    }

    public void ShootGun() {
        if (gunData.currentAmmo <= 0) return;
        if (!CanShoot()) return;

        Transform cameraTransform = camera.transform;
        
        if (Physics.Raycast(camera.transform.position, cameraTransform.forward, out RaycastHit hitInfo,
                gunData.maxDistance)) {
            Debug.Log("Gun has been shot");
            IDamagable damageable = hitInfo.transform.GetComponent<IDamagable>();
            damageable?.Damage(gunData.damage);
        }
        
        gunData.currentAmmo--;
        UIManager.Instance.SetAmmo();
        timeSinceLastShot = 0;
        OnGunShot();
    }

    private void StartReloadGun() {
        if (!gunData.reloading) {
            StartCoroutine(ReloadGun());
        }
    }
    
    private IEnumerator ReloadGun() {
        gunData.reloading = true;
        
        yield return new WaitForSeconds(gunData.reloadTime);
        
        if (gunData.currentAmmo < gunData.magSize) {
            gunData.currentAmmo++;    
            gunData.reloading = false;
            UIManager.Instance.SetAmmo();
        }
        else {
            gunData.reloading = false;
        }
        
        
        
    }
    
    

    private void OnGunShot() {
        
    }
}
