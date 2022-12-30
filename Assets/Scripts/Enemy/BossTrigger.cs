using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform bossSpawn;

    void SpawnBoss() {
        var boss = Instantiate(bossPrefab);
        boss.transform.position = bossSpawn.position;

    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            SpawnBoss();
            Destroy(gameObject);
        }
    }
}
