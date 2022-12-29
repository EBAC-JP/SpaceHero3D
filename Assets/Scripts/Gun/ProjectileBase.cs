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

    void OnCollisionEnter(Collision collision) {
        var damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null) {
            Vector3 direction = -(collision.transform.position - transform.position).normalized;
            direction.y = 0;
            damageable.OnDamage(damage, direction);
        }
        Destroy(gameObject);
    }
}