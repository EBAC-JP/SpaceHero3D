using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    [SerializeField] float timeDestroy;
    [SerializeField] float speed;
    [SerializeField] int damage;

    void Awake() {
        Destroy(gameObject, timeDestroy);
    }

    void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}