using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour {

    [SerializeField] string openTrigger = "Open";
    [Header("Show Key")]
    [SerializeField] GameObject key;
    [SerializeField] Ease ease;
    [SerializeField] float duration;

    Animator _animator;
    bool _isOpened = false;
    float _startKeyScale;

    void Start() {
        _animator = GetComponent<Animator>();
        _startKeyScale = key.transform.localScale.x;
    }

    [NaughtyAttributes.Button]
    void OpenChest() {
        _animator.SetTrigger(openTrigger);
        HideNotification();
        _isOpened = true;
    }

    [NaughtyAttributes.Button]
    void ShowNotification() {
        key.SetActive(true);
        key.transform.localScale = Vector3.zero;
        key.transform.DOScale(_startKeyScale, duration).SetEase(ease);
    }

    [NaughtyAttributes.Button]
    void HideNotification() {
        key.SetActive(false);
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Player") && !_isOpened) ShowNotification();
    }

    void OnTriggerExit(Collider collider) {
        if(collider.CompareTag("Player") && !_isOpened) HideNotification();
    }

}
