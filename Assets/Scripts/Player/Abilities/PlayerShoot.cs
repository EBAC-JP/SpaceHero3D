using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : PlayerAbility {

    [SerializeField] InputAction shoot;
    [SerializeField] Transform gunLocation;
    [SerializeField] Transform shootPosition;
    [SerializeField] GunBase gun;

    Coroutine _currentCoroutine;
    GunBase _currentGun;

    protected override void Init() {
        CreateGun();
        shoot.Enable();
        shoot.performed += ctx => StartShoot();
        shoot.canceled += ctx => StopShoot();
    }

    void CreateGun() {
        _currentGun = Instantiate(gun, gunLocation);
        _currentGun.SetShootPosition(shootPosition);
    }

    void StartShoot() {
        _currentGun.StartShoot();
    }

    void StopShoot() {
        _currentGun.StopShoot(); 
    }
}
