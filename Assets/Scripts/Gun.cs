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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if (PlayerController.Instance.ReturnMoveStatus() && !LevelManager.Instance.isPaused) {

            if (_shotCounter > 0) {
                _shotCounter -= Time.deltaTime;
            } else {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    _shotCounter = timeBetweenShots;
                    AudioManager.Instance.PlaySFX(12);

                }
                
                /*
                if (Input.GetMouseButton(0)) {
                    _shotCounter -= Time.deltaTime;

                    if (_shotCounter <= 0) {
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        AudioManager.Instance.PlaySFX(12);
                        _shotCounter = timeBetweenShots;
                    }
                }
                */
            }
        }
    }
}
