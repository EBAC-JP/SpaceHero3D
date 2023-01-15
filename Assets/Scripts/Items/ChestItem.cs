using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestItem : MonoBehaviour {
    [Header("Item")]
    [SerializeField] ItemType itemType;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] int itemAmount;
    [Header("Show Animation")]
    [SerializeField] Vector2 range;
    [SerializeField] float duration;

    List<GameObject> _itens = new List<GameObject>();

    public void ShowItem() {
        for (int i = 0; i < itemAmount; i++) {
            var item = Instantiate(itemPrefab);
            item.transform.position = transform.position + Vector3.forward * Random.Range(range.x, range.y) + Vector3.right * Random.Range(range.x, range.y);
            item.transform.DOScale(0, .2f).From().SetEase(Ease.Linear);
            _itens.Add(item);
        }
    }

    public void Collect() {
        foreach (var item in _itens) {
            item.transform.DOMoveY(2f, duration).SetRelative();
            item.transform.DOScale(0f, duration / 2f).SetDelay(duration / 2f);
            InventoryManager.Instance.AddByType(itemType);
            Invoke(nameof(ClearItens), 1.5f);
        }
    }

    void ClearItens() {
        foreach (var item in _itens) {
            _itens.Remove(item);
            Destroy(item);
        }
    }
}
