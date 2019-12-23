using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour {

    private SpriteRenderer theSR;
    
    
    // Start is called before the first frame update
    void Start() {
        theSR = GetComponent<SpriteRenderer>();

        theSR.sortingOrder = Mathf.RoundToInt(-10 * transform.position.y);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
