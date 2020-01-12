using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilPower : MonoBehaviour {

    public static DevilPower Instance;


    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        PlayerController.Instance.I_Am("Devil");
        AbilityManager.Instance.shouldTurnOnDevilPassive = true;
        AbilityManager.Instance.needResetDevil = true;
    }

    // Update is called once per frame
    void Update() {
    }

    public void Devil_Passive1() {
        PlayerController.Instance.damageExtraToMultiply = 1f + (1f - (float) PlayerHealthController.Instance.currentHealth /
                                                                (float) PlayerHealthController.Instance.maxHealth);
    }
}
