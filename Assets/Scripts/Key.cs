using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Key : MonoBehaviour {
    
    [SerializeField]
    private KeyType keyType;
    

    // Update is called once per frame
    private void Update() {
        transform.Rotate(0, 50f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other) {
        GameManager.Instance.SetHasKey(keyType);
        Destroy(gameObject);
    }
}
