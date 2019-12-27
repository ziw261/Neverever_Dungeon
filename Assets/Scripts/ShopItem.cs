using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {

    public GameObject buyMessage;

    private bool inBuyZone;

    public bool isHealthRestore, isHealthUpgrade, isWeapon;

    public int itemCost;

    public int healthIncreaseAmount;
    // Start is called before the first frame update
    void Start()
    {
        
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
