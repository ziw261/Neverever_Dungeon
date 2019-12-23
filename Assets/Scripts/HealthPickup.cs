using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public int healAmount = 1;

    public float waitTime = .5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (waitTime > 0) {
            waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && waitTime <= 0) {
            PlayerHealthController.Instance.HealPlayer(healAmount);
            
            Destroy(gameObject);
        }
    }
}
