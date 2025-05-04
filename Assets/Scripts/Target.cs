using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour, IDamagable {
    public float health = 100f;
    public bool isPlayer;
    
    public void Damage(float damage) {
        Debug.Log("damage taken:" + damage);
        health -= damage;

        switch (health) {
            case <= 0 when !isPlayer:
                Destroy(gameObject);
                break;
            case <= 0 when isPlayer:
                GameManager.Instance.LoadScene("EndScene");
                break;
        }
    }
}
