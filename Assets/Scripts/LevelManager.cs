using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    
    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;

    public int currentCoins;

    public Transform startPoint;
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {

        PlayerController.Instance.transform.position = startPoint.position;
        PlayerController.Instance.StartMove();
        
        currentCoins = CharacterTracker.Instance.currentCoins;
        
        Time.timeScale = 1f;
        UIController.Instance.coinText.text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update() {
        
        // Escape key to trigger pause
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!UIController.Instance.deathScreen.activeInHierarchy) {
                PauseControl();
            }
        }
    }

    public IEnumerator LevelEnd() {
        
        AudioManager.Instance.PlayLevelWin();
        
        UIController.Instance.StartFadeToBlack();

        PlayerController.Instance.StopMove();
        
        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.Instance.currentCoins = currentCoins;
        CharacterTracker.Instance.currentHealth = PlayerHealthController.Instance.currentHealth;
        CharacterTracker.Instance.maxHealth = PlayerHealthController.Instance.maxHealth;
        
        SceneManager.LoadScene(nextLevel);
    }


    public void PauseControl() {
        if (!isPaused) {
            UIController.Instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        } else {
            UIController.Instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount) {
        currentCoins += amount;
        UIController.Instance.coinText.text = currentCoins.ToString();
    }


    public void SpendCoins(int amount) {
        
        currentCoins -= amount;

        if (currentCoins < 0) {
            currentCoins = 0;
        }
        
        UIController.Instance.coinText.text = currentCoins.ToString();

    }
}
