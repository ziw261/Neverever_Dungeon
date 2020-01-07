using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject bulletEffect;
    public int damageToDeal = 50;
    
    
    
    
    // Start is called before the first frame update
    void Start() {
        damageToDeal = (int) ((damageToDeal + PlayerController.Instance.damageExtraToAdd) * PlayerController.Instance.damageExtraToMultiply);
    }

    // Update is called once per frame
    void Update() {
        theRB.velocity = transform.right * speed;
        
        //Debug.Log(damageToDeal);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        
        AudioManager.Instance.PlaySFX(4);


        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().DamageEnemy(damageToDeal);
        }

        if (other.CompareTag("Boss")) {
            BossController.Instance.TakeDamage(damageToDeal);
            Instantiate(BossController.Instance.hitEffect, transform.position, transform.rotation);
        }
    }


    private void OnBecameInvisible() {
        
        Destroy(gameObject);
    }
}
