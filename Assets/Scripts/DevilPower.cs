using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilPower : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        PlayerController.Instance.damageExtraToMultiply = 1f+(1f-(float)PlayerHealthController.Instance.currentHealth / (float)PlayerHealthController.Instance.maxHealth);
    }
}
