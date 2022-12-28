using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class EnemyBase : MonoBehaviour {
    
    [SerializeField] int startLife; 
    [Header("Born Animation")]
    [SerializeField] float startDuration;
    [SerializeField] Ease startEase;
    [SerializeField] bool bornAnimation;
    [Header("Animation")]
    [SerializeField] float deathDuration;

    int _currentLife;
    AnimationBase _animation;

    void Awake() {
        _animation = GetComponent<AnimationBase>();
        if (bornAnimation) BornAnimation();
        ResetLife();
    }

    void ResetLife() {
        _currentLife = startLife;
    }

    void BornAnimation() {
        transform.DOScale(0, startDuration).SetEase(startEase).From();
    }

    [Button]
    void Damage() {
        OnDamage(3);
    }

    protected virtual void OnKill() {
        Destroy(gameObject, deathDuration);
        _animation.PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(int damage) {
        _currentLife -= damage;
        if (_currentLife <= 0) OnKill();
    }
}
