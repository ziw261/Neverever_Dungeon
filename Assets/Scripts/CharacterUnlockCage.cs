using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterUnlockCage : MonoBehaviour {

    private bool _canUnlock;
    public GameObject message;

    public List<CharacterSelector> charSelects = new List<CharacterSelector>();
    private CharacterSelector _playerToUnlock;

    public SpriteRenderer cagedSprite;

    // Start is called before the first frame update
    void Start() {
        _playerToUnlock = charSelects[Random.Range(0, charSelects.Count)];

        cagedSprite.sprite = _playerToUnlock.playerToSpawn.bodySpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update() {

        if (_canUnlock) {
            if (Input.GetKeyDown(KeyCode.E)) {
                
                // 0 for lock, 1 for unlock
                PlayerPrefs.SetInt(_playerToUnlock.playerToSpawn.name, 1);
                
                Instantiate(_playerToUnlock, transform.position, transform.rotation);

                charSelects.Remove(_playerToUnlock);
                
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _canUnlock = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _canUnlock = false;
            message.SetActive(false);
        }
    }
}
