using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    
    public static BossController Instance;

    public BossAction[] actions;
    private int _currentAction = 0;
    private float _actionCounter;

    private float _shotCounter;
    private Vector2 _moveDirection;
    public Rigidbody2D theRB;

    public int currentHealth;

    public GameObject deathEffect;
    public GameObject levelExit;
    public GameObject hitEffect;

    public BossSequence[] sequences;
    public int currentSequence;
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {

        actions = sequences[currentSequence].actions;
        
        _actionCounter = actions[_currentAction].actionLength;
        
        UIController.Instance.bossHealthBar.maxValue = currentHealth;
        UIController.Instance.bossHealthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update() {
        if (_actionCounter > 0) {
            _actionCounter -= Time.deltaTime;
            
            // handle movements
            _moveDirection = Vector2.zero;

            if (actions[_currentAction].shouldMove) {
                if (actions[_currentAction].shouldChasePlayer) {
                    _moveDirection = PlayerController.Instance.transform.position - transform.position;
                    _moveDirection.Normalize();
                }

                if (actions[_currentAction].moveToPoints && Vector3.Distance(transform.position, actions[_currentAction].pointToMoveTo.position) > 0.5f) {
                    _moveDirection = actions[_currentAction].pointToMoveTo.position - transform.position;
                    _moveDirection.Normalize();
                }
            }
            theRB.velocity = _moveDirection * actions[_currentAction].moveSpeed;
            
            
            // handle shooting
            if (actions[_currentAction].shouldShoot) {
                _shotCounter -= Time.deltaTime;
                if (_shotCounter <= 0) {
                    _shotCounter = actions[_currentAction].timeBetweenShots;

                    foreach (Transform t in actions[_currentAction].shotPoints) {
                        Instantiate(actions[_currentAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }
            
        } else {
            _currentAction++;
            if (_currentAction >= actions.Length) {
                _currentAction = 0;
            }

            _actionCounter = actions[_currentAction].actionLength;
        }
    }


    public void TakeDamage(int damageAmount) {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) {
            gameObject.SetActive(false);

            Instantiate(deathEffect, transform.position, transform.rotation);
            
            if (Vector3.Distance(PlayerController.Instance.transform.position, levelExit.transform.position) < 2.5f) {
                levelExit.transform.position += new Vector3(4f,4f,0f);
            }
            
            levelExit.SetActive(true);
            
            UIController.Instance.bossHealthBar.gameObject.SetActive(false);
            
        } else {
            if (currentHealth <= sequences[currentSequence].endSequenceHealth && currentSequence < sequences.Length - 1) {
                currentSequence++;
                actions = sequences[currentSequence].actions;
                _currentAction = 0;
                _actionCounter = actions[_currentAction].actionLength;
            }
        }
        
        UIController.Instance.bossHealthBar.value = currentHealth;
    }
}


[System.Serializable]
public class BossAction {
    [Header("Action")] 
    public float actionLength;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public bool moveToPoints;
    public float moveSpeed;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;
}


[System.Serializable]
public class BossSequence
{
    [Header("Sequence")] 
    public BossAction[] actions;

    public int endSequenceHealth;
}
