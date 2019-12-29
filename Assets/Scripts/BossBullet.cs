using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
    
    
    public float speed;
    private Vector3 _direction;
    
    
    // Start is called before the first frame update
    void Start() {
        _direction = transform.right;
    }

    // Update is called once per frame
    void Update() {
        transform.position += _direction * speed * Time.deltaTime;

        if (!BossController.Instance.gameObject.activeInHierarchy) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerHealthController.Instance.DamagePlayer();
        }
        
        Destroy(gameObject);
        AudioManager.Instance.PlaySFX(4);

    }
    
    private void OnBecameInvisible() {
        
        Destroy(gameObject);
    }
}
