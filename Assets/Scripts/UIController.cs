using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;
    
    public Slider healthSlider;
    public Text heathText;
    public GameObject deathScreen;
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
