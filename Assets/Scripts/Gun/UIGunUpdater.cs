using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGunUpdater : MonoBehaviour {

    [SerializeField] Image gunLoader;
    [Header("Animation")]
    [SerializeField] float duration;
    [SerializeField] Ease ease;

    Tween _currentTween;

    void OnValidate() {
        if (gunLoader == null) gunLoader = GetComponent<Image>();
    }

    public void UpdateValue(float current, float max) {
        if (_currentTween != null) _currentTween.Kill();
        _currentTween = gunLoader.DOFillAmount(1 - (current / max), duration).SetEase(ease);
    }

    public void UpdateValue(float value) {
        gunLoader.fillAmount = value;
    }

}
