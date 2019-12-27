using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _fadeOutBlack = true;
        _fadeToBlack = false;
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
    }


    public void BackToMainMenu() {
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(mainMenuScene);
    }


    public void Resume() {
        LevelManager.Instance.PauseControl();
    }
}
