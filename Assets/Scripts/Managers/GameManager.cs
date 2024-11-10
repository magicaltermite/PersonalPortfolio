using System;
using UnityEngine;

namespace Managers {
public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    
    private bool hasTealKey = true;
    private bool hasOrangeKey  = true;
    
    
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
            
            default:
                Debug.Log("Key type does not exist");
                break;
            
        }
    }

    public bool GetHasKeys() {
        return hasOrangeKey && hasTealKey;
    }
    
    
    
}

public enum KeyType {
    TealKey,
    OrangeKey
}
}
