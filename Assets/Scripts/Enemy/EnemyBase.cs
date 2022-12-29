using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable {
    
    [SerializeField] int startLife;
    [Header("Born Animation")]
    [SerializeField] protected float startDuration;
    [SerializeField] Ease startEase;
    [SerializeField] bool bornAnimation;
    [Header("Animation")]
    [SerializeField] float deathDuration;
    [Header("Damage Animation")]
    [SerializeField] ParticleSystem particles;

    protected AnimationBase _animation;
    protected bool _isDead = false;
    int _currentLife;
    Collider _collider;

    void Awake() {
        _animation = GetComponent<AnimationBase>();
        _collider = GetComponent<Collider>();
        if (bornAnimation) BornAnimation();
        ResetLife();
    }

    void ResetLife() {
        _currentLife = startLife;
    }

    void BornAnimation() {
        transform.DOScale(0, startDuration).SetEase(startEase).From();
    }

    protected virtual void OnKill() {
        _isDead = true;
        if (_collider != null) _collider.enabled = false;
        Destroy(gameObject, deathDuration);
        _animation.PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(int damage) {
        if (particles != null) particles.Play();
        _currentLife -= damage;
        if (_currentLife <= 0) OnKill();
    }
}
