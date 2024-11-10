using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.GetHasKeys()) {
            Debug.Log("Door should be destroyed");
            Destroy(gameObject);
        }
        else {
            Debug.Log("Missing keys");
        }
    }
}
