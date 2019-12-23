using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour  {

    public Rigidbody2D theRb;
    public float moveSpeed = 3f;

    public float rangeToChasePlayer = 7f;
    private Vector3 _moveDirection;

    public Animator anim;

    public int health = 150;

    public GameObject[] deathSplatters;
    public GameObject hitEffect;

    public bool shouldShoot;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float _fireCounter;



    public float shootRange;
    
    public SpriteRenderer theBody;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (theBody.isVisible && PlayerController.Instance.gameObject.activeInHierarchy) {
            
            // Moving logic
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <
                rangeToChasePlayer) {
                _moveDirection = PlayerController.Instance.transform.position - transform.position;
            } else {
                _moveDirection = Vector3.zero;
            }

            _moveDirection.Normalize();
            
            // Fixed facing issue for enemies (default facing left)
            if (transform.position.x > PlayerController.Instance.transform.position.x) {
                transform.localScale = new Vector3(1f,1f,1f);
                firePoint.localScale = new Vector3(1f,1f,1f);
            } else {
                transform.localScale = new Vector3(-1f,1f,1f);
                firePoint.localScale = new Vector3(-1f,1f,1f);

            }

            theRb.velocity = _moveDirection * moveSpeed;

            // Shooting logic
            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.Instance.transform.position)<=shootRange) {
                _fireCounter -= Time.deltaTime;

                if (_fireCounter <= 0) {
                    _fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
            }
        } else {
            theRb.velocity = Vector2.zero;
        }


        // Moving animation logic
        if (_moveDirection != Vector3.zero) {
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }        
    }


    public void DamageEnemy(int damage) {
        health -= damage;

        Instantiate(hitEffect, transform.position, transform.rotation);
        
        if (health <= 0) {
            
            Destroy(gameObject);

            int selectedSplatter = Random.Range(0,deathSplatters.Length);

            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f,0f,rotation*90f));
        }
    }
}
