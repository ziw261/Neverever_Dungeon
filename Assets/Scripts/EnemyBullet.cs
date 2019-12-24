using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed;
    private Vector3 _direction;
    
    
    // Start is called before the first frame update
    void Start() {
        _direction = PlayerController.Instance.transform.position - transform.position;
        _direction.Normalize();
    }

    // Update is called once per frame
    void Update() {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerHealthController.Instance.DamagePlayer();
        }
        
        Destroy(gameObject);
        AudioManager.Instance.PlaySFX(4);

    }
    
    private void OnBecameInvisible() {
        
        Destroy(gameObject);
    }
}
