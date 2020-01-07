using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefPower : MonoBehaviour {

    public int itemDropRateToMultiply = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        PlayerController.Instance.isTheif = true;
        PlayerController.Instance.itemDropRateToMultiply = itemDropRateToMultiply;
    }
}
