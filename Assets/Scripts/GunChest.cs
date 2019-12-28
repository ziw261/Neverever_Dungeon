using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour {

    public GunPickup[] potentialGuns;

    public SpriteRenderer theSR;
    public Sprite chestOpen;

    public GameObject notification;

    private bool _canOpen, _isOpen;

    public Transform spawnPoint;

    // Used when open chest, chest grows big, and slowly grows back
    public float scaleSpeed = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if (_canOpen && !_isOpen) {
            if (Input.GetKeyDown(KeyCode.E)) {
                int gunSelect = Random.Range(0, potentialGuns.Length);

                Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);

                theSR.sprite = chestOpen;

                _isOpen = true;
                
                transform.localScale = new Vector3(1.2f,1.2f,0f);
            }
        }

        if (_isOpen) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            notification.SetActive(true);
            _canOpen = true;
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            notification.SetActive(false);
            _canOpen = false;
        }
    }
}
