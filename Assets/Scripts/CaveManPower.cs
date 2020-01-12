using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManPower : MonoBehaviour {

    public int healAmountForCaveMan = 1;
    
    // Start is called before the first frame update
    void Start() {
        PlayerController.Instance.I_Am("CaveMan");
        PlayerController.Instance.healAmountForCaveMan = healAmountForCaveMan;
    }

    // Update is called once per frame
    void Update() {

    }
}
