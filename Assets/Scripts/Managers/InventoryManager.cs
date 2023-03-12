using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager> {

    [SerializeField] List<ItemSetup> itemSetups;
    [SerializeField] UIItem prefabUI;
    [SerializeField] Transform inventory;

    void Start() {
        Reset();
        CreateUI();
    }

    void Reset() {
        itemSetups.Find(i => i.itemType == ItemType.COIN).itemValue = SaveManager.Instance.GetCoins();
        itemSetups.Find(i => i.itemType == ItemType.LIFE).itemValue = SaveManager.Instance.GetLifes();
    }

    void CreateUI() {
        foreach(var setup in itemSetups) {
            var itemUI = Instantiate(prefabUI, inventory);
            itemUI.Load(setup);
        }
    }

    public int GetItemValueByType(ItemType itemType) {
        return itemSetups.Find(i => i.itemType == itemType).itemValue;
    }
    
    public void AddByType(ItemType itemType, int amount = 1) {
        if (amount < 0) return;
        itemSetups.Find(i => i.itemType == itemType).itemValue += amount;
    }

    public void RemoveByType(ItemType itemType, int amount = 1) {
        if (amount < 0) return;
        var item = itemSetups.Find(i => i.itemType == itemType);
        item.itemValue -= amount;
        if(item.itemValue < 0) item.itemValue = 0;
    }
}

public enum ItemType {
    COIN,
    LIFE
}

[System.Serializable]
public class ItemSetup {

    public ItemType itemType;
    public int itemValue;
    public Sprite itemImage;
}