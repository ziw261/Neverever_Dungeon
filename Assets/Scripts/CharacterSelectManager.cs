using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

    public static CharacterSelectManager Instance;
    public PlayerController activePlayer;
    public CharacterSelector activeCharSelect;

    private void Awake() {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
