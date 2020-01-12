using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnemyController : MonoBehaviour  {

    [Header("Chase Player Variable")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer = 7f;
    private Vector3 _moveDirection;

    
    [Header("Run Away Variable")]
    public bool shouldRunAway;
    public float runawayRange;


    [Header("Wander Variable")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float _wanderCounter, _pauseCounter;
    private Vector3 _wanderDirection;


    [Header("Patrol Variable")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int _currentPatrolPoint;
    
    
    [Header("Shooting Variable")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float _fireCounter;
    public float shootRange;
    
    [Header("Item Drop Variable")]
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;
    
    
    [Header("Basic Variables")]
    public Animator anim;
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public SpriteRenderer theBody;
    public Rigidbody2D theRb;
    public float moveSpeed = 3f;
    
    
    
    // Start is called before the first frame update
    void Start() {
        if (shouldWander) {
            _pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
        }

    }

    // Update is called once per frame
    void Update() {

        if (theBody.isVisible && PlayerController.Instance.gameObject.activeInHierarchy) {
            
            // Moving logic - chase player
            _moveDirection = Vector3.zero;
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <
                rangeToChasePlayer && shouldChasePlayer) {
                _moveDirection = PlayerController.Instance.transform.position - transform.position;
            } else {
                if (shouldWander) {
                    if (_wanderCounter > 0) {
                        _wanderCounter -= Time.deltaTime;
                        
                        //move the enemy
                        _moveDirection = _wanderDirection;
                        
                        if (_wanderCounter <= 0) {
                            _pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
                        }
                    }

                    if (_pauseCounter > 0) {
                        _pauseCounter -= Time.deltaTime;

                        if (_pauseCounter < -0) {
                            _wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength * 1.25f);
                            _wanderDirection = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f), 0f);
                        }
                    }
                }

                if (shouldPatrol) {
                    
                    // Move towards to the next patrol points?
                    _moveDirection = patrolPoints[_currentPatrolPoint].position - transform.position;

                    if (Vector3.Distance(transform.position, patrolPoints[_currentPatrolPoint].position) < 0.2f) {
                        _currentPatrolPoint++;
                        if (_currentPatrolPoint >= patrolPoints.Length) {
                            _currentPatrolPoint = 0;
                        }
                    }
                }
            }

            
            // Moving logic - runaway from player
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <
                runawayRange && shouldRunAway) {
                _moveDirection = transform.position - PlayerController.Instance.transform.position;
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
                    AudioManager.Instance.PlaySFX(13);

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
        
        AudioManager.Instance.PlaySFX(2);

        
        Instantiate(hitEffect, transform.position, transform.rotation);
        
        // Enemy Died
        if (health <= 0) {
            
            Destroy(gameObject);

            if (PlayerController.Instance.isCaveMan) {
                PlayerHealthController.Instance.HealPlayer(PlayerController.Instance.healAmountForCaveMan);
            }
            
            AudioManager.Instance.PlaySFX(1);
            
            int selectedSplatter = Random.Range(0,deathSplatters.Length);

            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f,0f,rotation*90f));
            
            // Item drop logic
            if (shouldDropItem) {
                    
                float dropChance = Random.Range(0f, 100f);
                    
                if (PlayerController.Instance.isThief) {
                    itemDropPercent = itemDropPercent * PlayerController.Instance.itemDropRateToMultiply;
                }
                
                if (dropChance <= itemDropPercent) {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
