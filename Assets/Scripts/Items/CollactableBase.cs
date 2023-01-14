using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableBase : MonoBehaviour {

    [SerializeField] string targetTag;
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject graphicItem;

    protected virtual void Collect() {
        if (graphicItem != null) graphicItem.SetActive(false);
        OnCollect();
    }
    
    protected virtual void OnCollect() {
        if (particle != null) particle.Play();
        if (audioSource != null) audioSource.Play();
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag(targetTag)) {
            Collect();
        }
    }
}