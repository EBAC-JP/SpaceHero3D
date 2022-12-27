using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

    [SerializeField] ProjectileBase projectilePrefab;
    [SerializeField] Transform positionShoot;
    [Header("Gun Limits")]
    [SerializeField] float cooldownShoot;
    [SerializeField] float rechargeTime;
    [SerializeField] int maxShoots;

    Coroutine _currentCoroutine;
    int _currentShoots;
    bool _recharging = false;

    public void StartShoot() {
        StopShoot();
        if (!_recharging) _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot() {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _recharging = false;
    }

    void Shoot() {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = positionShoot.position;
        projectile.transform.rotation = positionShoot.rotation;
    }

    IEnumerator ShootCoroutine() {
        while(true) {
            if (_currentShoots < maxShoots) {
                _currentShoots++;
                Shoot();
                yield return new WaitForSeconds(cooldownShoot);
            } else {
                _recharging = true;
                _currentShoots = 0;
                yield return new WaitForSeconds(rechargeTime);
                _recharging = false;
            }
        }
    }

}
