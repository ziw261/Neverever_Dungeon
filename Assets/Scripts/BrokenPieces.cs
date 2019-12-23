﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrokenPieces : MonoBehaviour {

    public float moveSpeed = 3f;
    private Vector3 _moveDirection;

    public float deceleration = 5f;

    public float lifeTime = 3f;

    public SpriteRenderer theSR;
    public float fadeSpeed = 2.5f;
    
    // Start is called before the first frame update
    void Start() {
        _moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        _moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update() {
        transform.position += _moveDirection * Time.deltaTime;
        
        // Move you value at a certain percentage rate toward to another
        _moveDirection = Vector3.Lerp(_moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0) {
            
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, Mathf.MoveTowards(theSR.color.a, 0f, fadeSpeed * Time.deltaTime));
            
            if (theSR.color.a == 0f) {
                Destroy(gameObject);    
            }
            
            
        }
    }
}
