using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooks : EnemyBase {

    [Header("Look At")]
    [SerializeField] Transform lookAt;

    void Update() {
        transform.LookAt(lookAt.position);
    }
}