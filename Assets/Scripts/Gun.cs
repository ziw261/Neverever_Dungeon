using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    
    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float _shotCounter;

    public string weaponName;
    public Sprite gunUI;

    public int itemCost;
    public Sprite gunShopSprite;
    
    
    // Special Power from Characters
    //[HideInInspector]
    public bool isNinjaPowerOn = false;
    
    // Start is called before the first frame update
    void Start() {
        isNinjaPowerOn = false;
    }

    // Update is called once per frame
    void Update() {

        if (isNinjaPowerOn) {
            if (PlayerController.Instance.ReturnMoveStatus() && !LevelManager.Instance.isPaused) {
                if (_shotCounter > 0) {
                    _shotCounter -= Time.deltaTime;
                } else {
                    
                    // Really Critical and Heavy Code, definitely need polishing.
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation).transform.Rotate(0,0,15);;
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation).transform.Rotate(0,0,-15);

                        _shotCounter = timeBetweenShots * 2;
                        AudioManager.Instance.PlaySFX(12);
                    }
                }
            }
            
        } else if (PlayerController.Instance.ReturnMoveStatus() && !LevelManager.Instance.isPaused) {

            NormalShoot();
        }
    }


    public void NormalShoot() {
        if (_shotCounter > 0) {
            _shotCounter -= Time.deltaTime;
        } else {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                _shotCounter = timeBetweenShots;
                AudioManager.Instance.PlaySFX(12);

            }
                
        }
    }
}
