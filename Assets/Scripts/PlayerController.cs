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

    private Camera _theCam;

    public Animator anim;

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float _shotCounter;

    public SpriteRenderer bodySpriteRenderer;

    private float _activeMoveSpeed;

    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;

    private float _dashCounter, _dashCoolCounter;
    
    void Awake() {
        Instance = this;
    }    
    
    
    // Start is called before the first frame update
    void Start() {
        _theCam = Camera.main;
        _activeMoveSpeed = moveSpeed;

    }

    // Update is called once per frame
    void Update() {
        
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        
        // Balance diagonal moving speed
        _moveInput.Normalize();
        
        //transform.position += new Vector3(_moveInput.x * Time.deltaTime * moveSpeed, _moveInput.y * Time.deltaTime * moveSpeed, 0f);

        theRb.velocity = _moveInput * _activeMoveSpeed;

        // Calculate direction
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = _theCam.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x) {
            transform.localScale = new Vector3(-1f,1f,1f);
            gunDir.localScale = new Vector3(-1f,-1f,1f);
        } else {
            transform.localScale = new Vector3(1f,1f,1f);
            gunDir.localScale = new Vector3(1f,1f,1f);

        }
        
        Vector2 offSet = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
        gunDir.rotation = Quaternion.Euler(0,0, angle);

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            _shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0)) {
            _shotCounter -= Time.deltaTime;

            if (_shotCounter <= 0) {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                _shotCounter = timeBetweenShots;
            }
        }
        
        // Dash Logic
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_dashCoolCounter <= 0 && _dashCounter <= 0) {
                _activeMoveSpeed = dashSpeed;
                _dashCounter = dashLength;
                
                anim.SetTrigger("dash");
                
                PlayerHealthController.Instance.MakeInvincible(dashInvincibility);
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
            anim.SetBool("isWalking",true);
        } else {
            anim.SetBool("isWalking", false);
        }
    }

    public float GetDashCounter() {
        return _dashCounter;
    }
}
