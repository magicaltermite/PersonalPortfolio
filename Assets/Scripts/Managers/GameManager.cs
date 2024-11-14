using System;
using UnityEngine;

namespace Managers {
public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    
    private bool hasTealKey = true;
    private bool hasOrangeKey  = true;
    private bool hasRedKey, hasGreenKey, hasBlueKey;
    
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
        
    }


    public void SetHasKey(KeyType keyType) {
        switch(keyType) {
            case KeyType.OrangeKey:
                hasOrangeKey = true;
                Debug.Log("Picked up orange key: " + hasOrangeKey);
                break;
            
            case KeyType.TealKey:
                hasTealKey = true;
                Debug.Log("Picked up teal key: " + hasTealKey);
                break;

            case KeyType.GreenKey:
                hasGreenKey = true;
                Debug.Log("Picked up green key: " + hasGreenKey);
                break;
            
            case KeyType.BlueKey:
                hasBlueKey = true;
                Debug.Log("Picked up blue key: " + hasBlueKey);
                break;
            
            case KeyType.RedKey:
                hasRedKey = true;
                Debug.Log("Picked up blue key: " + hasBlueKey);
                break;
            
            default:
                Debug.Log("Key type does not exist");
                break;
            
        }
    }

    public void OpenChurchDoor(GameObject churchDoor) {
        if (hasBlueKey && hasGreenKey && hasRedKey) {
            Destroy(churchDoor);
        }
        else {
            Debug.Log("You don't have all the keys!");
        }
    }

    public bool GetHasKeys() {
        return hasOrangeKey && hasTealKey;
    }
    
    
    
    
    
}

public enum KeyType {
    TealKey,
    OrangeKey,
    GreenKey,
    BlueKey,
    RedKey
}
}
