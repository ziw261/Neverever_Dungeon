using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuchaPower : MonoBehaviour {

    public int coolDownTime = 10;
    public float powerLastTime = 0.3f;
    private float _powerCoolDownCounter = 0f;
    private float _powerLastCounter = 0f;
    private bool _inCoolDown = false;
    private bool _isFirstPress = true;
    
    
    // Start is called before the first frame update
    void Start() {
        _isFirstPress = true;
        _inCoolDown = false;
        _powerCoolDownCounter = coolDownTime;
        _powerLastCounter = powerLastTime;
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.F)) {
            if (_isFirstPress) {
                if (!_inCoolDown) {
                    Time.timeScale = 0.05f;
                    PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].isLuchaPowerOn =
                        true;
                    _isFirstPress = false;
                    
                }

            } else {
                Time.timeScale = 1f;
                PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].isLuchaPowerOn = false;
                _isFirstPress = true;
                _inCoolDown = true;
            }
        }

        if (!_isFirstPress) {
            
            if (_powerLastCounter > 0) {
                _powerLastCounter -= Time.deltaTime;
            } else {
                _powerLastCounter = powerLastTime;
                Time.timeScale = 1f;
                PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].isLuchaPowerOn = false;
                _isFirstPress = true;
                _inCoolDown = true;
            }
            
        }

        if (_inCoolDown) {
            if (_powerCoolDownCounter > 0) {
                _powerCoolDownCounter -= Time.deltaTime;
            } else {
                _powerCoolDownCounter = coolDownTime;
                _inCoolDown = false;
            }
        } 
        
    }
    
}
