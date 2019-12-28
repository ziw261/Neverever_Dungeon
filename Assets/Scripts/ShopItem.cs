using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ShopItem : MonoBehaviour {

    public GameObject buyMessage;

    private bool inBuyZone;

    public bool isHealthRestore, isHealthUpgrade, isWeapon;

    public int itemCost;

    public int healthIncreaseAmount;

    public Gun[] potentialGuns;
    private Gun _theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;
    
    
    // Start is called before the first frame update
    void Start() {

        if (isWeapon) {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            _theGun = potentialGuns[selectedGun];

            gunSprite.sprite = _theGun.gunShopSprite;
            infoText.text = _theGun.weaponName + "\n - " + _theGun.itemCost + " Gold -";
            itemCost = _theGun.itemCost;
        }
        
    }

    // Update is called once per frame
    void Update() {

        if (inBuyZone) {
            if (Input.GetKeyDown(KeyCode.E)) {

                if (LevelManager.Instance.currentCoins >= itemCost) {
                    LevelManager.Instance.SpendCoins(itemCost);

                    if (isHealthRestore) {
                        PlayerHealthController.Instance.HealPlayer(PlayerHealthController.Instance.maxHealth);
                    } else if (isHealthUpgrade) {
                        PlayerHealthController.Instance.IncreaseMaxHealth(healthIncreaseAmount);
                    } else if (isWeapon) {
                        bool hasGun = false;
                        foreach (Gun gun in PlayerController.Instance.availableGuns) {
                            if (gun.weaponName.Equals(_theGun.weaponName)) {
                                hasGun = true;
                            }
                        }

                        if (!hasGun) {
                            Gun gunClone = Instantiate(_theGun);
                            gunClone.transform.parent = PlayerController.Instance.gunDir;
                            gunClone.transform.position = PlayerController.Instance.gunDir.position;
                            gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                            gunClone.transform.localScale = Vector3.one;
                
                            PlayerController.Instance.availableGuns.Add(gunClone);
                            PlayerController.Instance.ChangeToNewGun();
                        }
                    }
                    
                    gameObject.SetActive(false);
                    inBuyZone = false;
                    
                    AudioManager.Instance.PlaySFX(18);
                } else {
                    AudioManager.Instance.PlaySFX(19);
                }

            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
