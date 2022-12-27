using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GunBase : MonoBehaviour {
    
    [SerializeField] ProjectileBase prefabProjectile;
    [SerializeField] Transform positionShoot;
    [SerializeField] float cooldownShoot;

    [Button]
    void Shoot() {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionShoot.position;
        projectile.transform.rotation = positionShoot.rotation;
    }

}