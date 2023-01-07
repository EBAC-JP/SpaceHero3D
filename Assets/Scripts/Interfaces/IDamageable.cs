using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void OnKill();
    void OnDamage(int damage);
    void OnDamage(int damage, Vector3 direction);
}
