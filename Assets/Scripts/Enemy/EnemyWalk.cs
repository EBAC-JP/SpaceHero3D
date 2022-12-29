using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : EnemyBase {

    [Header("Waypoints")]
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float minDistance;
    [SerializeField] float speed;

    int _index = 0;
    float _time = 0;

    void Update() {
        if (_isDead) return;
        if (_time < startDuration) {
            _time += Time.deltaTime;
            return;
        }
        _animation.PlayAnimationByTrigger(AnimationType.RUN);
        if (Vector3.Distance(transform.position, waypoints[_index].position) < minDistance) {
            _index++;
            if (_index >= waypoints.Count) _index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, Time.deltaTime * speed);
        transform.LookAt(waypoints[_index].position);
    }
}