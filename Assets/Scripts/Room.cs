using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public bool closeWhenEntered; // openWhenEnemiesCleared;
    
    
    public GameObject[] doors;

    //public List<GameObject> enemies = new List<GameObject>();

    private bool _roomActive;

    public GameObject mapHider;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
        /*
        
        // The logic for room that door is gonna open when all enemies die
        if (enemies.Count > 0 && _roomActive && openWhenEnemiesCleared) {
            
            for (int i = 0; i < enemies.Count; i++) {
                if (!enemies[i]) {
                    enemies.RemoveAt(i);
                    i--;
                }    
            }

            if (enemies.Count == 0) {
                
            }
        }
        */
    }

    public void OpenDoors() {
        foreach (GameObject door in doors) {
            door.SetActive(false);
            closeWhenEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            
            // Change Camera position
            CameraController.Instance.ChangeTarget(transform);
            
            // Close door logic
            if (closeWhenEntered) {
                foreach (GameObject door in doors) {
                    door.SetActive(true);
                }
            }

            _roomActive = true;
            mapHider.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _roomActive = false;
        }
    }


    public bool IsRoomActive() {
        return _roomActive;
    }
}
