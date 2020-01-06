using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaPower : MonoBehaviour {

    public int powerLastTime = 5;
    public int coolDownTime = 10;
    private float _powerCoolDownCounter = 0f;
    private float _powerLastCounter = 0f;
    private bool _inPowerTime = false;
    private bool _inCoolDown = false;
    
    // Start is called before the first frame update
    void Start() {
        _powerLastCounter = powerLastTime;
        _powerCoolDownCounter = coolDownTime;
        _inPowerTime = false;
        _inCoolDown = false;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.F)) {
            if (!_inCoolDown) {
                _inPowerTime = true;
            } 
        }

        if (_inPowerTime) {
            PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].isNinjaPowerOn = true;
            if (_powerLastCounter > 0) {
                _powerLastCounter -= Time.deltaTime;
                
            } else {
                _inPowerTime = false;
                _powerLastCounter = powerLastTime;
                _inCoolDown = true;
            }
        } else if (!_inPowerTime) {
            PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].isNinjaPowerOn = false;
            
        }

        if (_inCoolDown) {
            // Start to count cooldown time
            if (_powerCoolDownCounter > 0) {
                _powerCoolDownCounter -= Time.deltaTime;
                _inCoolDown = true;
            } else {
                _inCoolDown = false;
                _powerCoolDownCounter = coolDownTime;
            }
        }

    }
    
    
    
    
    
}
