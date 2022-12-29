using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBase {

    [Header("Shoot")]
    [SerializeField] bool lookPlayer;
    [SerializeField] GunBase enemyGun;
    [SerializeField] Transform shootPosition;

    float _time = 0;
    Player _player;

    void Start() {
        if (lookPlayer) _player = GameObject.FindObjectOfType<Player>();
        enemyGun.SetShootPosition(shootPosition);
    }

    void Update() {
        if (lookPlayer) transform.LookAt(_player.transform.position);
        if (_time >= enemyGun.GetCooldown()) {
            _time = 0;
            enemyGun.Shoot();
        }
        _time += Time.deltaTime;
    }
}