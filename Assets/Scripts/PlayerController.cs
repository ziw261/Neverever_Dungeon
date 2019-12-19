using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
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
    
    // Start is called before the first frame update
    void Start() {
        _theCam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        
        // Balance diagonal moving speed
        _moveInput.Normalize();
        
        //transform.position += new Vector3(_moveInput.x * Time.deltaTime * moveSpeed, _moveInput.y * Time.deltaTime * moveSpeed, 0f);

        theRb.velocity = _moveInput * moveSpeed;

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
        
        
        // Animation
        if (_moveInput != Vector2.zero) {
            anim.SetBool("isWalking",true);
        } else {
            anim.SetBool("isWalking", false);
        }
    }
}
