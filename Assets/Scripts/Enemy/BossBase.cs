using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;

public enum BossAction {
    INIT,
    WALK,
    ATTACK,
    DEATH
}

public class BossBase : MonoBehaviour, IDamageable {

    [SerializeField] int lifeAmount;
    [Header("Damage")]
    [SerializeField] FlashColor flash;
    [SerializeField] ParticleSystem particles;
    [SerializeField] float deathDuration;
    [Header("Born Animation")]
    [SerializeField] float bornDuration;
    [SerializeField] Ease bornEase;
    [Header("Walk")]
    [SerializeField] float speed;
    [SerializeField] float minDistance;
    [Header("Attack")]
    [SerializeField] int attackAmount;
    [SerializeField] int secondPhaseAttack;
    [SerializeField] float timeBetweenAttacks;
    [Header("Boss Gun")]
    [SerializeField] int amountPerShoot;
    [SerializeField] float angle;
    [SerializeField] Transform shootPosition;
    [SerializeField] ProjectileBase projectilePrefab;
    [Header("Events")]
    [SerializeField] UnityEvent killEvent;

    StateMachine<BossAction> _stateMachine;
    List<GameObject> _waypoints;
    Collider _collider;
    AnimationBase _animation;
    int _currentLife;
    Player _player;
    bool _onSecondPhase = false;

    void Awake() {
        Init();
        _stateMachine.RegisterState(BossAction.INIT, new BossStateInit());
        _stateMachine.RegisterState(BossAction.WALK, new BossStateWalk());
        _stateMachine.RegisterState(BossAction.ATTACK, new BossStateAttack());
        _stateMachine.RegisterState(BossAction.DEATH, new BossStateDeath());
        SwitchState(BossAction.INIT);
    }

    public void BornAnimation(Action onArrive = null) {
        StartCoroutine(BornCoroutine(onArrive));
    }

    public void WalkToPoint(Action onArrive = null) {
        Transform point = _waypoints[UnityEngine.Random.Range(0, _waypoints.Count)].transform;
        _animation.PlayAnimationByTrigger(AnimationType.RUN);
        StartCoroutine(WalkCoroutine(point, onArrive));
    }

    public void Attack(Action onArrive = null) {
        _animation.PlayAnimationByTrigger(AnimationType.IDLE);
        StartCoroutine(AttackCoroutine(onArrive));
    }

    public void Death() {
        StopAllCoroutines();
        if (_collider != null) _collider.enabled = false;
        Destroy(gameObject, deathDuration);
        _animation.PlayAnimationByTrigger(AnimationType.DEATH);
        LevelManager.Instance.LoadEndLevel();
    }

    public void SwitchState(BossAction state) {
        _stateMachine.SwitchState(state, this);
    }

    public void OnDamage(int damage) {}

    public void OnDamage(int damage, Vector3 direction) {
        if (particles != null) particles.Play();
        if (flash != null) flash.Flash();
        _currentLife -= damage;
        if (_currentLife <= lifeAmount / 2 && !_onSecondPhase) SecondPhase();
        if (_currentLife <= 0) OnKill();
    }

    void Update() {
        if (_player != null) transform.LookAt(_player.transform.position);
    }

    void Init() {
        _animation = GetComponent<AnimationBase>();
        _collider = GetComponent<Collider>();
        _currentLife = lifeAmount;
        _waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("BossWaypoints"));
        _stateMachine = new StateMachine<BossAction>();
        _player = GameObject.FindObjectOfType<Player>();
    }

    void Shoot() {
        int mult = 0;
        for (int i = 0; i < amountPerShoot; i++) {
            if (i % 2 == 0) mult++;
            var projectile = Instantiate(projectilePrefab, shootPosition);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle) * mult;
            projectile.transform.parent = null;
        }
    }

    public void OnKill() {
        SwitchState(BossAction.DEATH);
    }

    void SecondPhase() {
        speed *= 1.5f;
        attackAmount = secondPhaseAttack;
        timeBetweenAttacks *= .5f;
        amountPerShoot *= 2;
        _onSecondPhase = true;
    }

    IEnumerator WalkCoroutine(Transform point, Action onArrive = null) {
        while(Vector3.Distance(transform.position, point.position) > minDistance) {
            transform.position = Vector3.MoveTowards(transform.position, point.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        onArrive?.Invoke();
    }

    IEnumerator AttackCoroutine(Action onArrive = null) {
        int attack = 0;
        while(attack < attackAmount) {
            attack++;
            transform.DOScale(2.5f, .3f);
            Shoot();
            yield return new WaitForSeconds(.3f);
            transform.DOScale(2f, .1f);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        onArrive?.Invoke();
    }

    IEnumerator BornCoroutine(Action onArrive = null) {
        transform.DOScale(0, bornDuration).SetEase(bornEase).From();
        yield return new WaitForSeconds(bornDuration);
        onArrive?.Invoke();
    }
}