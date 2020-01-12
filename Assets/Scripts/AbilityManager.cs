using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    public static AbilityManager Instance;


    [Header("SpaceMan")]
    public bool needResetSpaceMan = false;
    public int increaseHealthAmount = 5;
    public bool shouldTurnOnSpacemanPassive = false;



    [Header("Devil")] 
    public bool needResetDevil = false;
    public bool shouldTurnOnDevilPassive = false;
    
    
    // Currently, Caveman does not need to reset, since the actual implementation is in EnemyController and decided by
    // PlayerController.Instance.isCaveman.
    
    void Awake() {
        Instance = this;
        shouldTurnOnSpacemanPassive = false;
        shouldTurnOnDevilPassive = false;
        needResetSpaceMan = false;
    }


    // Start is called before the first frame update
    void Start() {
        

    }

    // Update is called once per frame
    void Update() {
        
        ResetAllAbilities();

        PassiveMonitor();
        
        
    }


    public void ResetAllAbilities() {
        if (!PlayerController.Instance.isSpaceMan && needResetSpaceMan) {
            PlayerHealthController.Instance.IncreaseMaxHealth(-increaseHealthAmount);
            needResetSpaceMan = false;
        } else if (!PlayerController.Instance.isDevil && needResetDevil) {
            PlayerController.Instance.damageExtraToMultiply = 1f;
            needResetDevil = false;
        } 
        
        
    }


    public void PassiveMonitor() {
        if (shouldTurnOnSpacemanPassive) {
            SpacemanPower.Instance.SpaceMan_Passive1(increaseHealthAmount);
            shouldTurnOnSpacemanPassive = false;
        } else if (shouldTurnOnDevilPassive) {
            DevilPower.Instance.Devil_Passive1();
        }
    }



}
