using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnetic : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Coin")) {
            var coin = collider.gameObject.GetComponent<CollactableBase>();
            coin.gameObject.AddComponent<Magnetic>();
        }
    }

}
