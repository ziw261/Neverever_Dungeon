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
    
    /*
    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float _shotCounter;
    */

    public SpriteRenderer bodySpriteRenderer;

    private float _activeMoveSpeed;

    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;

    private float _dashCounter, _dashCoolCounter;
    
    private bool _canMove = true;
    
    public List<Gun> availableGuns = new List<Gun>();
    private int _currentGun;
    
    
    void Awake() {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }    
    
    
    // Start is called before the first frame update
    void Start() {

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
}
