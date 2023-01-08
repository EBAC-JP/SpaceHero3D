using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour {
    
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] TMP_Text checkpointText;
    [SerializeField] float durationText;
    [SerializeField] string checkpointKey = "CheckpointKey";
    [SerializeField] public int valueKey;

    bool _active = false;

    void OnTriggerEnter(Collider collider) {
        if (!_active && collider.transform.tag == "Player") Active();
    }

    void Active() {
        _active = true;
        PlayerPrefs.SetInt(checkpointKey, valueKey);
        meshRenderer.material.EnableKeyword("_EMISSION");
        if (checkpointText != null) StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        checkpointText.enabled = true;
        yield return new WaitForSeconds(durationText);
        checkpointText.enabled = false;
    }

}
