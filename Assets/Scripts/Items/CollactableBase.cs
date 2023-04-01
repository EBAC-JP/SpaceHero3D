using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableBase : MonoBehaviour {

    [SerializeField] string targetTag;
    [SerializeField] GameObject graphicItem;
    [SerializeField] ItemType itemType;
    [Header("Collected")]
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float deathDuration;
    [SerializeField] SFXType sfxType;
    
    Collider _collider;

    void Start() {
        _collider = GetComponent<Collider>();
    }

    void PlaySFX() {
        audioSource.clip = AudioManager.Instance.GetSFXClipByType(sfxType);
        audioSource.Play();
    }

    protected virtual void Collect() {
        if (graphicItem != null) graphicItem.SetActive(false);
        if (_collider != null) _collider.enabled = false;
        OnCollect();
        Destroy(gameObject, deathDuration);
    }

    protected virtual void OnCollect() {
        if (particle != null) particle.Play();
        if (audioSource != null) PlaySFX();
        InventoryManager.Instance.AddByType(itemType);
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag(targetTag)) {
            Collect();
        }
    }
}