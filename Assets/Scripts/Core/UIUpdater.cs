using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIUpdater : MonoBehaviour {

    [SerializeField] Image uiImage;
    [Header("Animation")]
    [SerializeField] float duration;
    [SerializeField] Ease ease;

    Tween _currentTween;

    void OnValidate() {
        if (uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float current, float max) {
        if (_currentTween != null) _currentTween.Kill();
        _currentTween = uiImage.DOFillAmount(1 - (current / max), duration).SetEase(ease);
    }

    public void UpdateValue(float value) {
        uiImage.fillAmount = value;
    }

}
