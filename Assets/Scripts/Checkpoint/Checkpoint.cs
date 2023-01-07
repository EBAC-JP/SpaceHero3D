using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] string checkpointKey = "CheckpointKey";
    [SerializeField] int valueKey;

    bool _active = false;

    void OnTriggerEnter(Collider collider) {
        if (!_active && collider.transform.tag == "Player") Active();
    }

    void Active() {
        PlayerPrefs.SetInt(checkpointKey, valueKey);
        meshRenderer.material.EnableKeyword("_EMISSION");
    }

}
