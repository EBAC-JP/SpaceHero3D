using System.Collections;
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
    [SerializeField] List<Transform> waypoints;

    StateMachine<BossAction> _stateMachine;

    void Awake() {
        _stateMachine = new StateMachine<BossAction>();
        _stateMachine.RegisterState(BossAction.INIT, new BossStateInit());
        _stateMachine.RegisterState(BossAction.WALK, new BossStateWalk());
        _stateMachine.RegisterState(BossAction.ATTACK, new BossStateInit());
        SwitchState(BossAction.INIT);
    }

    public void BornAnimation() {
        transform.DOScale(0, bornDuration).SetEase(bornEase).From();
    }

    public void WalkToPoint() {
        Transform point = waypoints[Random.Range(0, waypoints.Count)];
        StartCoroutine(WalkCoroutine(point));
    }

    public void SwitchState(BossAction state) {
        _stateMachine.SwitchState(state, this);
    }

    IEnumerator WalkCoroutine(Transform point) {
        while(Vector3.Distance(transform.position, point.position) > minDistance) {
            transform.position = Vector3.MoveTowards(transform.position, point.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }

    #region DEBUG
    [Button]
    void SwitchWalk() {
        SwitchState(BossAction.WALK);
    }
    [Button]
    void SwitchAttack() {
        SwitchState(BossAction.ATTACK);
    }
    #endregion

}