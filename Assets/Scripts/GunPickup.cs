using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour {


    public Gun theGun;
    
    public float waitTime = .5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (waitTime > 0) {
            waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && waitTime <= 0) {

            bool hasGun = false;
            foreach (Gun gun in PlayerController.Instance.availableGuns) {
                if (gun.weaponName.Equals(theGun.weaponName)) {
                    hasGun = true;
                }
            }

            if (!hasGun) {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.Instance.gunDir;
                gunClone.transform.position = PlayerController.Instance.gunDir.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;
                
                PlayerController.Instance.availableGuns.Add(gunClone);
                PlayerController.Instance.ChangeToNewGun();
            }
            
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX(7);

        }
    }
}
