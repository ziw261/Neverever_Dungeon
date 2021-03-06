﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public static UIController Instance;
    
    public Slider healthSlider;
    public Text heathText;
    public GameObject deathScreen;

    public Text coinText;
    
    // Scene switch fade/not fade
    public Image fadeScreen;
    public float fadeSpeed;
    private bool _fadeToBlack, _fadeOutBlack;
    
    // Game Over Button
    public string newGameScene, mainMenuScene;
    
    // Pause Menu
    public GameObject pauseMenu;
    
    // Mini Map
    public GameObject mapDisplay;

    public GameObject bigMapText;
    
    // Gun Switch
    public Image currentGun;
    public Text gunText;
    
    // Boss
    public Slider bossHealthBar;
    
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _fadeOutBlack = true;
        _fadeToBlack = false;

        currentGun.sprite = PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].gunUI;
        gunText.text = PlayerController.Instance.availableGuns[PlayerController.Instance.GetCurrentGun()].weaponName;
    }

    // Update is called once per frame
    void Update() {
        if (_fadeOutBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0) {
                _fadeOutBlack = false;
            }
        }

        if (_fadeToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1) {
                _fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack() {
        _fadeToBlack = true;
        _fadeOutBlack = false;
    }

    
    public void NewGame() {
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(newGameScene);
        
        Destroy(PlayerController.Instance.gameObject);

    }


    public void BackToMainMenu() {
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(mainMenuScene);
        
        Destroy(PlayerController.Instance.gameObject);
    }


    public void Resume() {
        LevelManager.Instance.PauseControl();
    }
}
