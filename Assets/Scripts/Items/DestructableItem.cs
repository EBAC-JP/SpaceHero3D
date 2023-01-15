using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItem : MonoBehaviour, IDamageable {
    
    [SerializeField] int lifeAmount;
    [SerializeField] float shakeDuration;
    [SerializeField] int shakeForce;
    [Header("Coins")]
    [SerializeField] Vector2 amountRange;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform dropPosition;
    [SerializeField] float coinDelay;
    [Header("Death")]
    [SerializeField] float deathDuration;
    [SerializeField] GameObject viewObject;

    int _currentLife, _coinsAmount;
    Collider _collider;

    void Start() {
        _currentLife = lifeAmount;
        _coinsAmount = (int)Random.Range(amountRange.x, amountRange.y);
        _collider = GetComponent<Collider>();
    }

    void Damage() {
        OnDamage(1, Vector3.zero);
    }

    void DropCoin() {
        var coin = Instantiate(coinPrefab);
        coin.transform.position = dropPosition.position;
        coin.transform.DOScale(0, .5f).From().SetEase(Ease.Linear);
    }

    IEnumerator DropCoins() {
        for (int i = 0; i < _coinsAmount; i++) {
            DropCoin();
            yield return new WaitForSeconds(coinDelay);
        }
    }

    public void OnDamage(int damage) {}

    public void OnDamage(int damage, Vector3 direction) {
        _currentLife -= damage;
        transform.DOShakeScale(shakeDuration, Vector3.forward / 2, shakeForce);
        if (_currentLife <= 0) OnKill();
    }

    public void OnKill() {
        if (_collider != null) _collider.enabled = false;
        if (viewObject != null) viewObject.SetActive(false); 
        Destroy(gameObject, deathDuration);
        StartCoroutine(DropCoins());
    }
}
