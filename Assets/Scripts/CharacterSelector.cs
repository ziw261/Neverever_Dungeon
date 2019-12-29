using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {
    private bool _canSelect;
    
    public GameObject message;

    public PlayerController playerToSpawn;

    public bool shouldUnlock;
    
    // Start is called before the first frame update
    void Start() {

        if (!shouldUnlock) {
            if (PlayerPrefs.HasKey(playerToSpawn.name)) {
                if (PlayerPrefs.GetInt(playerToSpawn.name) == 1) {
                    gameObject.SetActive(true);
                } else {
                    gameObject.SetActive(false);
                }
            } else {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update() {

        if (_canSelect) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Vector3 playerPos = PlayerController.Instance.transform.position;
                
                Destroy(PlayerController.Instance.gameObject);

                PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
                PlayerController.Instance = newPlayer;
                
                gameObject.SetActive(false);

                CameraController.Instance.target = newPlayer.transform;

                CharacterSelectManager.Instance.activePlayer = newPlayer;
                CharacterSelectManager.Instance.activeCharSelect.gameObject.SetActive(true);
                CharacterSelectManager.Instance.activeCharSelect = this;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _canSelect = true;
            message.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _canSelect = false;
            message.SetActive(false);
        }
    }
}
