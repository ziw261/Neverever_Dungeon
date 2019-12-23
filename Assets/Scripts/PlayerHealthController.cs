using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {

    public static PlayerHealthController Instance;

    public int currentHealth;
    public int maxHealth = 5;

    public float damageInvincLength = 1f;
    private float _invincCount;
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        UIController.Instance.healthSlider.maxValue = maxHealth;
        UIController.Instance.healthSlider.value = currentHealth;
        UIController.Instance.heathText.text = currentHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update() {
        if (_invincCount > 0) {
            _invincCount -= Time.deltaTime;

            if (_invincCount <= 0) {
                Color temp = PlayerController.Instance.bodySpriteRenderer.color;
                PlayerController.Instance.bodySpriteRenderer.color = new Color(temp.r, temp.g, temp.b, 1f);
            }
        }
    }

    public void DamagePlayer() {

        if (_invincCount <= 0) {

            currentHealth--;

            _invincCount = damageInvincLength;
            
            // visual effect for invincible
            Color temp = PlayerController.Instance.bodySpriteRenderer.color;
            PlayerController.Instance.bodySpriteRenderer.color = new Color(temp.r, temp.g, temp.b, 0.5f);
            
            if (currentHealth <= 0) {
                PlayerController.Instance.gameObject.SetActive(false);
                UIController.Instance.deathScreen.SetActive(true);
            }

            // UI for health bar
            UIController.Instance.healthSlider.value = currentHealth;
            UIController.Instance.heathText.text = currentHealth + " / " + maxHealth;
        }
    }

    public void MakeInvincible(float length) {
        _invincCount = length;
        
        // visual effect for invincible
        Color temp = PlayerController.Instance.bodySpriteRenderer.color;
        PlayerController.Instance.bodySpriteRenderer.color = new Color(temp.r, temp.g, temp.b, 0.5f);
    }

    public void HealPlayer(int healthAmount) {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        
        // UI for health bar
        UIController.Instance.healthSlider.value = currentHealth;
        UIController.Instance.heathText.text = currentHealth + " / " + maxHealth;
    }

}
