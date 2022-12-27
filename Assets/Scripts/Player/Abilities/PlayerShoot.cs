using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : PlayerAbility {

    [SerializeField] InputAction shoot;
    [SerializeField] GunBase gun;

    Coroutine _currentCoroutine;

    protected override void Init() {
        shoot.Enable();
        shoot.performed += ctx => StartShoot();
        shoot.canceled += ctx => StopShoot();
    }

    void StartShoot() {
        gun.StartShoot();
    }

    void StopShoot() {
        gun.StopShoot(); 
    }
}
