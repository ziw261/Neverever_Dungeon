using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject bulletEffect;
    public int damgeToDeal = 50;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().DamageEnemy(damgeToDeal);
        }
    }


    private void OnBecameInvisible() {
        
        Destroy(gameObject);
    }
}
