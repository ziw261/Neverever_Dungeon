using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefPower : MonoBehaviour {

    public int itemDropRateToMultiply = 2;
    
    // Start is called before the first frame update
    void Start() {
        PlayerController.Instance.I_Am("Thief");
        PlayerController.Instance.itemDropRateToMultiply = itemDropRateToMultiply;

    }

    // Update is called once per frame
    void Update() {

    }
}
