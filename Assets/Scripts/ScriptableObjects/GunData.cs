using UnityEngine;

namespace ScriptableObjects {
[CreateAssetMenu(fileName="Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject {
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public int bulletsPerTap; // The amount of bullets the gun fires per trigger pull
    public float spread;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    
    [HideInInspector]
    public bool reloading;
}
}
