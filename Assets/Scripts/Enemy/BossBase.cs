using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public enum BossAction {
    INIT,
    WALK,
    ATTACK
}

public class BossBase : MonoBehaviour {

    [Header("Born Animation")]
    [SerializeField] float bornDuration;
    [SerializeField] Ease bornEase;
    [Header("Walk")]
    [SerializeField] float speed;
    [SerializeField] float minDistance;
    [Header("Attack")]
    [SerializeField] int attackAmount;
    [SerializeField] float timeBetweenAttacks;

    StateMachine<BossAction> _stateMachine;
    List<GameObject> _waypoints

    void Awake() {
        _waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("BossWaypoints"));
        _stateMachine = new StateMachine<BossAction>();
        _stateMachine.RegisterState(BossAction.INIT, new BossStateInit());
        _stateMachine.RegisterState(BossAction.WALK, new BossStateWalk());
        _stateMachine.RegisterState(BossAction.ATTACK, new BossStateAttack());
        SwitchState(BossAction.INIT);
    }

    public void BornAnimation(Action onArrive = null) {
        StartCoroutine(BornCoroutine(onArrive));
    }

    public void WalkToPoint(Action onArrive = null) {
        Transform point = _waypoints[UnityEngine.Random.Range(0, _waypoints.Count)].transform;
        StartCoroutine(WalkCoroutine(point, onArrive));
    }

    public void Attack(Action onArrive = null) {
        StartCoroutine(AttackCoroutine(onArrive));
    }

    public void SwitchState(BossAction state) {
        _stateMachine.SwitchState(state, this);
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