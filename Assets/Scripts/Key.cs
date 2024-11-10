using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Key : MonoBehaviour {
    
    [SerializeField]
    private KeyType keyType;
    
    //adjust this to change speed
    [SerializeField]
    float speed = 5f;   
    
    //adjust this to change how high it goes
    [SerializeField]
    float height = 0.5f;

    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;
        
        transform.Rotate(0,50f * Time.deltaTime,0);
        
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        //set the object’s Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z) ;
    }

    private void OnTriggerEnter(Collider other) {
        GameManager.Instance.SetHasKey(keyType);
        Destroy(gameObject);
    }
}
