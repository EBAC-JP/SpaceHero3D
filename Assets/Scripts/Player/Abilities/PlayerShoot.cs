using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : PlayerAbility {

    [SerializeField] InputAction shoot;
    [SerializeField] Transform gunLocation;
    [SerializeField] Transform shootPosition;
    [SerializeField] GunBase submachine;
    [SerializeField] GunBase pistol;
    [SerializeField] UIUpdater UIGunUpdater;

    Coroutine _currentCoroutine;
    GunBase _submachine, _pistol;

    protected override void Init() {
        CreateGuns();
        shoot.Enable();
        shoot.performed += ctx => StartShoot();
        shoot.canceled += ctx => StopShoot();
    }

    public void DisableShoot() {
        shoot.Disable();
    }

    void CreateGuns() {
        CreateSubMachine();
        CreatePistol();
    }

    void CreateSubMachine() {
        _submachine = Instantiate(submachine, gunLocation);
        _submachine.SetShootPosition(shootPosition);
        _submachine.SetGunUpdater(UIGunUpdater);
    }

    void CreatePistol() {
        _pistol = Instantiate(pistol, gunLocation);
        _pistol.SetShootPosition(shootPosition);
        _pistol.SetGunUpdater(UIGunUpdater);
    }


    void StartShoot() {
        if (player.GetGunIndex() == 0) _submachine.StartShoot();
        else if (player.GetGunIndex() == 1) _pistol.StartShoot();
    }

    void StopShoot() {
        if (player.GetGunIndex() == 0) _submachine.StopShoot();
        else if (player.GetGunIndex() == 1) _pistol.StopShoot();
    }
}
