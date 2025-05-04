using UnityEngine;

namespace Weapons {
    public class WeaponSwitching : MonoBehaviour {
        [SerializeField] private Transform[] weapons;
        [SerializeField] private KeyCode[] keys;
        [SerializeField] private float switchTime;
    
        private int selectedWeapon;
        private float timeSinceLastSwitch;

        private void Start() {
            SetWeapons();
            Select(selectedWeapon);

            timeSinceLastSwitch = 0f;
        }

        private void Update() {
            var previousSelectedWeapon = selectedWeapon;

            for (var i = 0; i < keys.Length; i++) {
                if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime) {
                    selectedWeapon = i;
                }
            }
        
            if(previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

            timeSinceLastSwitch += Time.deltaTime;
        }

        private void SetWeapons() {
            weapons = new Transform[transform.childCount];

            for (var i = 0; i < transform.childCount; i++) {
                weapons[i] = transform.GetChild(i);
            }

            keys ??= new KeyCode[weapons.Length];
        }

        private void Select(int weaponIndex) {
            for (var i = 0; i < weapons.Length; i++) {
                weapons[i].gameObject.SetActive(i == weaponIndex);
            }

            timeSinceLastSwitch = 0f;

            OnWeaponSelected(weapons[weaponIndex]);
        }

        private void OnWeaponSelected(Transform gunControllerTransform) {
            var gunController = gunControllerTransform.gameObject.GetComponent<GunController>();
        
            UIManager.Instance.SetAmmo(gunController.GetGunData());
        }
    }
}
