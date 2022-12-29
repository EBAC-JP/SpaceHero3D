using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    [SerializeField] float timeDestroy;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] List<string> tagsToHit;

    void Awake() {
        Destroy(gameObject, timeDestroy);
    }

    void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void ApplyDamage(Collision collision) {
        var damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null) {
            Vector3 direction = -(collision.transform.position - transform.position).normalized;
            direction.y = 0;
            damageable.OnDamage(damage, direction);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        foreach (var tag in tagsToHit) {
            if (collision.transform.tag == tag) {
                ApplyDamage(collision);
                break;
            }
        }
    }
}