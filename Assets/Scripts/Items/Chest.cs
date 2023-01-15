using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour {

    [SerializeField] string openTrigger = "Open";
    [SerializeField] ChestItem chestItem;
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

    void Update() {
        if(Input.GetKeyDown(KeyCode.F) && key.activeSelf) OpenChest();
    }

    void OpenChest() {
        if (_isOpened) return;
        _animator.SetTrigger(openTrigger);
        HideNotification();
        _isOpened = true;
        Invoke(nameof(ShowItem), 1f);
    }

    void ShowItem() {
        chestItem.ShowItem();
        Invoke(nameof(Collect), 1f);
    }

    void Collect() {
        chestItem.Collect();
    }

    void ShowNotification() {
        key.SetActive(true);
        key.transform.localScale = Vector3.zero;
        key.transform.DOScale(_startKeyScale, duration).SetEase(ease);
    }

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
