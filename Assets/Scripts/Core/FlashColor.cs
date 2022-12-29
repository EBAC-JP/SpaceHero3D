using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour {
    
    [SerializeField] SkinnedMeshRenderer skinned;
    [SerializeField] MeshRenderer mesh;
    [Header("Setup")]
    [SerializeField] Color color;
    [SerializeField] float duration;

    Tween _currentTween;

    void OnValidate() {
        if (mesh == null) mesh = GetComponent<MeshRenderer>();
        if (skinned == null) skinned = GetComponent<SkinnedMeshRenderer>();
    }

    public void Flash() {
        if (mesh != null && !_currentTween.IsActive())
            _currentTween = mesh.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        if (skinned != null && !_currentTween.IsActive())
            _currentTween = skinned.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }

}