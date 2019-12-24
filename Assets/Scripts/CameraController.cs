using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    public float moveSpeed;

    public Transform target;


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

    }

    public void ChangeTarget(Transform newTarget) {
        target = newTarget;
    }
}
