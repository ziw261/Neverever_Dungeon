using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemanPower : MonoBehaviour {

    public int increaseHelathAmount = 5;
    
    
    public int powerLastTime = 5;
    public int coolDownTime = 10;
    private float _powerCoolDownCounter = 0f;
    private float _powerLastCounter = 0f;
    private bool _inPowerTime = false;
    private bool _inCoolDown = false;
    
    
    // Start is called before the first frame update
    void Start() {
        PlayerHealthController.Instance.maxHealth += increaseHelathAmount;
        PlayerHealthController.Instance.currentHealth = PlayerHealthController.Instance.maxHealth;
        UIController.Instance.healthSlider.maxValue = PlayerHealthController.Instance.maxHealth;
        UIController.Instance.healthSlider.value = PlayerHealthController.Instance.currentHealth;
        UIController.Instance.heathText.text = PlayerHealthController.Instance.currentHealth + " / " + PlayerHealthController.Instance.maxHealth;
        
        _powerLastCounter = powerLastTime;
        _powerCoolDownCounter = coolDownTime;
        _inPowerTime = false;
        _inCoolDown = false;
    }

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!_inPowerTime && !_inCoolDown) {
                PlayerHealthController.Instance.MakeInvincible(powerLastTime);
                _inPowerTime = true;
            }
        }

        if (_inPowerTime) {
            if (_powerLastCounter <= 0) {
                _inPowerTime = false;
                _powerLastCounter = powerLastTime;
                _inCoolDown = true;
            } else {
                _powerLastCounter -= Time.deltaTime;
                _inPowerTime = true;
            }
        }

        if (_inCoolDown) {
            if (_powerCoolDownCounter > 0) {
                _powerCoolDownCounter -= Time.deltaTime;
                _inCoolDown = true;
            } else {
                _powerCoolDownCounter = coolDownTime;
                _inCoolDown = false;
            }

        }

        

    }
}
