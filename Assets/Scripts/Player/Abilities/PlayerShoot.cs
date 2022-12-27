using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : PlayerAbility {

    [SerializeField] InputAction shoot;
    [SerializeField] ProjectileBase projectilePrefab;
    [SerializeField] Transform positionShoot;
    [SerializeField] float cooldownShoot;

    Coroutine _currentCoroutine;

    protected override void Init() {
        shoot.Enable();
        shoot.performed += ctx => StartShoot();
        shoot.canceled += ctx => StopShoot();
    }

    void StartShoot() {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    void StopShoot() {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

    void Shoot() {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = positionShoot.position;
        projectile.transform.rotation = positionShoot.rotation;
    }

    IEnumerator ShootCoroutine() {
        while(true) {
            Shoot();
            yield return new WaitForSeconds(cooldownShoot);
        }
    }

}
