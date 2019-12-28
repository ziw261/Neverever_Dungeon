using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool _bigMapActive;
    
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
        // Expensive, may improve the logic later on.
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.M)) {
            if (!_bigMapActive) {
                ActivateBigMap();
            } else {
                DeactivateBigMap();
            }
        }

    }

    public void ChangeTarget(Transform newTarget) {
        target = newTarget;
    }

    public void ActivateBigMap() {

        if (!LevelManager.Instance.isPaused) {
            _bigMapActive = true;
            bigMapCamera.enabled = true;
            mainCamera.enabled = false;
            UIController.Instance.bigMapText.SetActive(true);
            
            // Close mini map
            UIController.Instance.mapDisplay.SetActive(false);

            // Stop the player moving when opening map
            PlayerController.Instance.StopMove();
            Time.timeScale = 0f;
        }
    }

    public void DeactivateBigMap() {

        if (!LevelManager.Instance.isPaused) {
            _bigMapActive = false;
            bigMapCamera.enabled = false;
            mainCamera.enabled = true;
            UIController.Instance.bigMapText.SetActive(false);

            // Open mini map
            UIController.Instance.mapDisplay.SetActive(true);


            // Make the player can move again
            PlayerController.Instance.StartMove();
            Time.timeScale = 1f;
        }

    }
}
