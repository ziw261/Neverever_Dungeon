using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManPower : MonoBehaviour {

    public int healAmountForCaveMan = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        PlayerController.Instance.isCaveMan = true;
        PlayerController.Instance.healAmountForCaveMan = healAmountForCaveMan;
    }
}
