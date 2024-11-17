using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameManager.Instance.LoadScene("EndScene");
    }
}
