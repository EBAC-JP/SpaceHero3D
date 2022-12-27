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

    void Shoot() {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = _shootPosition.position;
        projectile.transform.rotation = _shootPosition.rotation;
    }

    IEnumerator ShootCoroutine() {
        while(true) {
            if (_currentShoots < maxShoots) {
                _currentShoots++;
                Shoot();
                yield return new WaitForSeconds(cooldownShoot);
            } else {
                Debug.Log("Recarregando!");
                yield return new WaitForSeconds(rechargeTime);
                Debug.Log("Pronto para Atirar!");
                _currentShoots = 0;
            }
        }
    }

}
