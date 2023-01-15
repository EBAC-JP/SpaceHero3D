using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour {

    [SerializeField] float dist;
    [SerializeField] float coinSpeed;

    void Update() {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > dist) {
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
            coinSpeed += .5f;
        }
    }

}