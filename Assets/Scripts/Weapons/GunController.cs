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
        Debug.Log("Shoot gun called");
        if (gunData.currentAmmo <= 0) return;
        Debug.Log("gun has enough ammo");
        if (!CanShoot()) return;
        Debug.Log("Gun can shoot");

        Transform cameraTransform = camera.transform;
        
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 3);

        
        if (Physics.Raycast(camera.transform.position, cameraTransform.forward, out RaycastHit hitInfo,
                gunData.maxDistance, ~playerMask)) {
            Debug.Log("Hello what the fuck");
            Debug.Log(hitInfo.transform.name);
            
            IDamagable damageable = hitInfo.transform.GetComponent<IDamagable>();
            damageable?.Damage(gunData.damage);
        }
        
        gunData.currentAmmo--;
        timeSinceLastShot = 0;
        OnGunShot();
    }

    public void StartReloadGun() {
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
        }
        else {
            gunData.reloading = false;
        }
        
        
        
    }
    
    

    private void OnGunShot() {
        
    }
}
