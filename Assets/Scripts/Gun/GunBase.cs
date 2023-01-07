using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

    [SerializeField] ProjectileBase projectilePrefab;
    [Header("Gun Limits")]
    [SerializeField] float cooldownShoot;
    [SerializeField] float rechargeTime;
    [SerializeField] int maxShoots;

    Transform _shootPosition;
    UIUpdater _gunUpdater;
    Coroutine _currentCoroutine;
    int _currentShoots;
    bool _recharging = false;

    public void StartShoot() {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot() {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

    public void SetShootPosition(Transform shootPosition) {
        _shootPosition = shootPosition;
    }

    public void SetGunUpdater(UIUpdater gunUpdater) {
        _gunUpdater = gunUpdater;
    }

    public float GetCooldown() {
        return cooldownShoot;
    }
    
    public void Shoot() {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = _shootPosition.position;
        projectile.transform.rotation = _shootPosition.rotation;
    }

    void CheckRecharge() {
        if (_currentShoots >= maxShoots) {
            StopShoot();
            StartRecharge();
        }
    }

    void StartRecharge() {
        _recharging = true;
        StartCoroutine(Recharge());
    }

    IEnumerator Recharge() {
        float time = 0;
        while(time < rechargeTime) {
            time += Time.deltaTime;
            _gunUpdater.UpdateValue(time / rechargeTime);
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    IEnumerator ShootCoroutine() {
        if (_recharging) yield break;
        while(true) {
            if (_currentShoots < maxShoots) {
                _currentShoots++;
                CheckRecharge();
                _gunUpdater.UpdateValue(_currentShoots, maxShoots);
                Shoot();
                yield return new WaitForSeconds(cooldownShoot);
            }
        }
    }

}
