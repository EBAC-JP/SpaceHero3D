using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyBase : MonoBehaviour {
    
    [SerializeField] int startLife;
    [SerializeField] int damage;

    [SerializeField] int _currentLife;

    void Awake() {
        ResetLife();
    }

    void ResetLife() {
        _currentLife = startLife;
    }

    protected virtual void OnKill() {
        Destroy(gameObject);
    }

    public void OnDamage(int damage) {
        _currentLife -= damage;
        if (_currentLife <= 0) OnKill();
    }

    [Button]
    void Damage() {
        OnDamage(damage);
    }
}
