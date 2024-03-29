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
    [SerializeField] AudioSource audioSource;
    [SerializeField] SFXType sfxType;

    bool _active = false;

    void OnTriggerEnter(Collider collider) {
        if (!_active && collider.CompareTag("Player")) Active();
    }

    void PlaySFX() {
        audioSource.clip = AudioManager.Instance.GetSFXClipByType(sfxType);
        audioSource.Play();
    }

    void Active() {
        if (audioSource != null) PlaySFX();
        _active = true;
        PlayerPrefs.SetInt(checkpointKey, valueKey);
        meshRenderer.material.EnableKeyword("_EMISSION");
        if (checkpointText != null) StartCoroutine(ShowText());
        SaveManager.Instance.SaveCheckpoint();
    }

    IEnumerator ShowText() {
        checkpointText.enabled = true;
        yield return new WaitForSeconds(durationText);
        checkpointText.enabled = false;
    }

}
