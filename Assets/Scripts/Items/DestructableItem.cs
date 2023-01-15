using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItem : MonoBehaviour, IDamageable {
    
    [SerializeField] int lifeAmount;
    [SerializeField] float shakeDuration;
    [SerializeField] int shakeForce;

    int _currentLife;

    void Start() {
        _currentLife = lifeAmount;
    }

    [NaughtyAttributes.Button]
    void Damage() {
        OnDamage(1, Vector3.zero);
    }

    public void OnDamage(int damage) {}

    public void OnDamage(int damage, Vector3 direction) {
        _currentLife -= damage;
        transform.DOShakeScale(shakeDuration, Vector3.forward / 2, shakeForce);
    }

    public void OnKill() {}
}
