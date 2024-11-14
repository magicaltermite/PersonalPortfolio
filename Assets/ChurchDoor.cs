using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ChurchDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameManager.Instance.OpenChurchDoor(gameObject);
    }
}
