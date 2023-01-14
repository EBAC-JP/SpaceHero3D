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
    
    Collider _collider;

    void Start() {
        _collider = GetComponent<Collider>();
    }

    protected virtual void Collect() {
        if (graphicItem != null) graphicItem.SetActive(false);
        if (_collider) _collider.enabled = false;
        OnCollect();
        Destroy(gameObject, deathDuration);
    }

    protected virtual void OnCollect() {
        if (particle != null) particle.Play();
        if (audioSource != null) audioSource.Play();
        InventoryManager.Instance.AddByType(itemType);
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag(targetTag)) {
            Collect();
        }
    }
}