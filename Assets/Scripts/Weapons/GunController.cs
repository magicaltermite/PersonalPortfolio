using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons {
public class GunController : MonoBehaviour {

    [SerializeField] private GunData gunData;
    
    [SerializeField] private GameObject bulletHole;

    
    private new Camera camera;
    private float timeSinceLastShot;
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    
    private void Start() {
        camera = Camera.main;
        gunData.currentAmmo = 6;

        PlayerShoot.ShootInput += ShootGun;
        PlayerShoot.ReloadInput += StartReloadGun;
        
        UIManager.Instance.SetAmmo(gunData);
    }
    


    private void Update() {
        timeSinceLastShot += Time.deltaTime;
    }

    private void ShootGun() {
        camera = Camera.main;
        if (gunData.currentAmmo <= 0) return;
        if (!CanShoot()) return;

        Transform cameraTransform = camera.transform;

        int counter = 0;

        while (counter < gunData.bulletsPerTap) {
            
            // This is for calculating the spread of the gun
            float x = Random.Range(-gunData.spread, gunData.spread);
            float y = Random.Range(-gunData.spread, gunData.spread);
            Vector3 direction = cameraTransform.forward + new Vector3(x, y, 0);

            if (Physics.Raycast(camera.transform.position, direction, out RaycastHit hitInfo,
                    gunData.maxDistance)) {
                IDamagable damageable = hitInfo.transform.GetComponent<IDamagable>();
                damageable?.Damage(gunData.damage);
                GameObject obj = Instantiate(bulletHole, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
            
            counter++;
        }

        gunData.currentAmmo--;
        UIManager.Instance.SetAmmo(gunData);
        timeSinceLastShot = 0;
        OnGunShot();
        
        
    }

    private void OnDisable() => gunData.reloading = false;
    
    private void StartReloadGun() {
        if (!gunData.reloading && gameObject.activeSelf) {
            StartCoroutine(ReloadGun());
        }
    }
    
    private IEnumerator ReloadGun() {
        gunData.reloading = true;
        
        yield return new WaitForSeconds(gunData.reloadTime);
        
        if (gunData.currentAmmo < gunData.magSize) {
            gunData.currentAmmo++;    
            gunData.reloading = false;
            UIManager.Instance.SetAmmo(gunData);
        }
        else {
            gunData.reloading = false;
        }
    }

    private void OnGunShot() {
        print("gun shot");
    }


    public GunData GetGunData() {
        return gunData;
    }

    
}
}
