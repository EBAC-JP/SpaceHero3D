using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootWalk : EnemyBase {

    [Header("Walk")]
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float minDistance;
    [SerializeField] float speed;
    [Header("Shoot")]
    [SerializeField] bool lookPlayer;
    [SerializeField] GunBase enemyGun;
    [SerializeField] Transform shootPosition;

    int _index = 0;
    float _time = 0;
    Player _player;

    void Start() {
        if (lookPlayer) _player = GameObject.FindObjectOfType<Player>();
        enemyGun.SetShootPosition(shootPosition);
    }

    void Update() {
        if (_isDead) return;
        if (lookPlayer) transform.LookAt(_player.transform.position);
        Shoot();
        Walk();
    }

    void Shoot() {
        if (_time >= enemyGun.GetCooldown()) {
            _time = 0;
            enemyGun.Shoot();
        }
        _time += Time.deltaTime;
    }

    void Walk() {
        _animation.PlayAnimationByTrigger(AnimationType.RUN);
        if (Vector3.Distance(transform.position, waypoints[_index].position) < minDistance) {
            _index++;
            if (_index >= waypoints.Count) _index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, Time.deltaTime * speed);
        if (!lookPlayer) transform.LookAt(waypoints[_index].position);
    }
}