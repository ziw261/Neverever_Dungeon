using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breakables : MonoBehaviour {

    public GameObject[] brokenPieces;
    public int maxPieces = 5;
    public int takenBullets = 2;
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            if (PlayerController.Instance.GetDashCounter() > 0) {
                BreakLogic();
            }
        } else if (other.CompareTag("Player Bullets")) {
            if (takenBullets > 0) {
                takenBullets--;
            } else {
                BreakLogic();
                
            }
        }
    }


    private void BreakLogic() {
        Destroy(gameObject);
                
        // Show broken pieces
        int piecesToDrop = Random.Range(1, maxPieces);

        for (int i = 0; i < piecesToDrop; i++) {

            int random = Random.Range(0, brokenPieces.Length);

            Instantiate(brokenPieces[random], transform.position, transform.rotation);
        }
                
                
        // Item drop logic
        if (shouldDropItem) {
                    
            float dropChance = Random.Range(0f, 100f);
                    
            if (dropChance <= itemDropPercent) {
                int randomItem = Random.Range(0, itemsToDrop.Length);

                Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }
}
