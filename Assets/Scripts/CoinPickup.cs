using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    public int coinValue = 1;

    public float waitTime;
    
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (waitTime > 0) {
            waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && waitTime <= 0) {
            
            LevelManager.Instance.GetCoins(coinValue);
            
            Destroy(gameObject);
            
            AudioManager.Instance.PlaySFX(5);

        }
    }
}
