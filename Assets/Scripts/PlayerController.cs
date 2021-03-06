﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;
    
    public float moveSpeed = 5f;
    private Vector2 _moveInput;


    public Rigidbody2D theRb;
    public Transform gunDir;
    

    public Animator anim;
    
    public SpriteRenderer bodySpriteRenderer;

    private float _activeMoveSpeed;

    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;

    private float _dashCounter, _dashCoolCounter;
    
    private bool _canMove = true;
    
    public List<Gun> availableGuns = new List<Gun>();
    private int _currentGun;


    [HideInInspector] 
    public bool isDevil = false;
    public int damageExtraToAdd = 0;
    public float damageExtraToMultiply = 1;

    [HideInInspector] 
    public bool isCaveMan = false;
    public int healAmountForCaveMan = 1;

    [HideInInspector] 
    public bool isThief = false;
    public int itemDropRateToMultiply = 2;


    [HideInInspector]
    public bool isLucha = false;


    [HideInInspector] 
    public bool isNinja = false;

    
    [HideInInspector] 
    public bool isPirate = false;


    [HideInInspector] 
    public bool isSneaker = false;
    
    
    [HideInInspector] 
    public bool isSpaceMan = false;
    
    void Awake() {
        Instance = this;
        isSpaceMan = false;
        isDevil = false;
        isLucha = false;
        isNinja = false;
        isPirate = false;
        isSneaker = false;
        isThief = false;
        isCaveMan = false;
        
        DontDestroyOnLoad(gameObject);
    }    
    
    
    // Start is called before the first frame update
    void Start() {
        
        damageExtraToAdd = 0;
        damageExtraToMultiply = 1f;
        
        _activeMoveSpeed = moveSpeed;
        UIController.Instance.currentGun.sprite = availableGuns[_currentGun].gunUI;
        UIController.Instance.gunText.text = availableGuns[_currentGun].weaponName;
        
        
    }

    // Update is called once per frame
    void Update() {

        if (_canMove && !LevelManager.Instance.isPaused) {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");

            // Balance diagonal moving speed
            _moveInput.Normalize();

            //transform.position += new Vector3(_moveInput.x * Time.deltaTime * moveSpeed, _moveInput.y * Time.deltaTime * moveSpeed, 0f);

            theRb.velocity = _moveInput * _activeMoveSpeed;

            // Calculate direction
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.Instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunDir.localScale = new Vector3(-1f, -1f, 1f);
            } else {
                transform.localScale = new Vector3(1f, 1f, 1f);
                gunDir.localScale = new Vector3(1f, 1f, 1f);

            }

            
            // Rotate gun hand
            Vector2 offSet = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
            gunDir.rotation = Quaternion.Euler(0, 0, angle);

            /*
            if (Input.GetMouseButtonDown(0)) {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                _shotCounter = timeBetweenShots;
                AudioManager.Instance.PlaySFX(12);

            }

            if (Input.GetMouseButton(0)) {
                _shotCounter -= Time.deltaTime;

                if (_shotCounter <= 0) {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    AudioManager.Instance.PlaySFX(12);
                    _shotCounter = timeBetweenShots;
                }
            }
            */
            
            
            // Switch Gun
            if (Input.GetKeyDown(KeyCode.Tab)) {
                if (availableGuns.Count > 0) {
                    _currentGun++;
                    if (_currentGun >= availableGuns.Count) {
                        _currentGun = 0;
                    }
                    SwitchGun();
                } else {
                    Debug.LogError(" Player has no guns?");
                }
            }

            // Dash Logic
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (_dashCoolCounter <= 0 && _dashCounter <= 0) {
                    _activeMoveSpeed = dashSpeed;
                    _dashCounter = dashLength;

                    anim.SetTrigger("dash");

                    PlayerHealthController.Instance.MakeInvincible(dashInvincibility);

                    AudioManager.Instance.PlaySFX(8);

                }
            }

            if (_dashCounter > 0) {
                _dashCounter -= Time.deltaTime;
                if (_dashCounter <= 0) {
                    _activeMoveSpeed = moveSpeed;
                    _dashCoolCounter = dashCooldown;
                }
            }

            if (_dashCoolCounter > 0) {
                _dashCoolCounter -= Time.deltaTime;
            }


            // Animation
            if (_moveInput != Vector2.zero) {
                anim.SetBool("isWalking", true);
            } else {
                anim.SetBool("isWalking", false);
            }
        } else {
            theRb.velocity = Vector2.zero;
            anim.SetBool("isWalking", false);
        }
    }

    public void SwitchGun() {
        foreach (Gun theGun in availableGuns) {
            theGun.gameObject.SetActive(false);
        }
        
        availableGuns[_currentGun].gameObject.SetActive(true);
        UIController.Instance.currentGun.sprite = availableGuns[_currentGun].gunUI;
        UIController.Instance.gunText.text = availableGuns[_currentGun].weaponName;
    }
    

    public float GetDashCounter() {
        return _dashCounter;
    }

    public void StopMove() {
        _canMove = false;
    }

    public void StartMove() {
        _canMove = true;
    }

    public bool ReturnMoveStatus() {
        return _canMove;
    }

    public void ChangeToNewGun() {
        _currentGun = availableGuns.Count - 1;
        SwitchGun();
    }

    public int GetCurrentGun() {
        return _currentGun;
    }


    public void I_Am(String name) {
        switch (name) {
            case "SpaceMan":
                isSpaceMan = true; isDevil = false; isLucha = false; isNinja = false;
                isPirate = false; isSneaker = false; isThief = false; isCaveMan = false;
                break;
            case "Sneaker":
                isSpaceMan = false; isDevil = false; isLucha = false; isNinja = false;
                isPirate = false; isSneaker = true; isThief = false; isCaveMan = false;
                break;
            case "Devil":
                isSpaceMan = false; isDevil = true; isLucha = false; isNinja = false;
                isPirate = false; isSneaker = false; isThief = false; isCaveMan = false;
                break;
            case "Lucha":
                isSpaceMan = false; isDevil = false; isLucha = true; isNinja = false;
                isPirate = false; isSneaker = false; isThief = false; isCaveMan = false;
                break;
            case "Ninja":
                isSpaceMan = false; isDevil = false; isLucha = false; isNinja = true;
                isPirate = false; isSneaker = false; isThief = false; isCaveMan = false;
                break;
            case "Pirate":
                isSpaceMan = false; isDevil = false; isLucha = false; isNinja = false;
                isPirate = true; isSneaker = false; isThief = false; isCaveMan = false;
                break;
            case "Thief":
                isSpaceMan = false; isDevil = false; isLucha = false; isNinja = false;
                isPirate = false; isSneaker = false; isThief = true; isCaveMan = false;
                break;
            case "CaveMan":
                isSpaceMan = false; isDevil = false; isLucha = false; isNinja = false;
                isPirate = false; isSneaker = false; isThief = false; isCaveMan = true;
                break;
        }
    }
}
