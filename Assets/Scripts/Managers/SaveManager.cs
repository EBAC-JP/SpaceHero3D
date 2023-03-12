using System.Net;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager> {

    [SerializeField] float playerStartLife;
    [Header("Prefs Keys")]
    [SerializeField] string clothKey = "ClothKey";
    [SerializeField] string checkpointKey = "CheckpointKey";

    SaveSetup _saveSetup;
    string path;
    

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
        _saveSetup.currentLevel = 2;
        _saveSetup.playerLife = playerStartLife;
        path = Application.persistentDataPath + "/save.txt";
    }

    void Save() {
        string setupJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupJson);
        SaveFile(setupJson);
    }

    void SaveFile(string json) {
        File.WriteAllText(path, json);
    }

    void SaveDefaults() {
        _saveSetup.coins = InventoryManager.Instance.GetItemValueByType(ItemType.COIN);
        _saveSetup.life = InventoryManager.Instance.GetItemValueByType(ItemType.LIFE);
        _saveSetup.clothValue = PlayerPrefs.GetInt(clothKey, 0);
        _saveSetup.playerLife = Player.Instance.GetCurrentLife();
    }

    public int GetCurrentLevel() {
        return _saveSetup.currentLevel;
    }

    public float GetCurrentPlayerLife() {
        return _saveSetup.playerLife;
    }

    public void SaveCheckpoint() {
        SaveDefaults();
        _saveSetup.currentCheckpoint = PlayerPrefs.GetInt(checkpointKey, 0);
        Save();
    }

    public void SaveEndLevel() {
        _saveSetup.currentLevel = _saveSetup.currentLevel + 1;
        SaveCheckpoint();
    }

    public bool VerifySaveExists() {
        return File.Exists(path);
    }

}

public class SaveSetup {

    public int currentLevel;
    public int currentCheckpoint;
    public int coins;
    public int life;
    public float playerLife;
    public int clothValue;
}