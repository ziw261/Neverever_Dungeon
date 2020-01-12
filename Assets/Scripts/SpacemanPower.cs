using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemanPower : MonoBehaviour {

    public static SpacemanPower Instance;
    
    
    public int increaseHealthAmount = 5;
    
    
    public int powerLastTime = 5;
    public int coolDownTime = 10;
    private float _powerCoolDownCounter = 0f;
    private float _powerLastCounter = 0f;
    private bool _inPowerTime = false;
    private bool _inCoolDown = false;


    private void Awake() {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start() {

        PlayerController.Instance.I_Am("SpaceMan");
        AbilityManager.Instance.shouldTurnOnSpacemanPassive = true;
        AbilityManager.Instance.needResetSpaceMan = true;
        AbilityManager.Instance.increaseHealthAmount = increaseHealthAmount;

        
        _powerLastCounter = powerLastTime;
        _powerCoolDownCounter = coolDownTime;
        _inPowerTime = false;
        _inCoolDown = false;
    }

    void Update() {
        SpaceManActive1();
    }

    public void SpaceManActive1() {
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
    
    public void SpaceMan_Passive1(int amount) {
        PlayerHealthController.Instance.IncreaseMaxHealth(amount);
    }
}
