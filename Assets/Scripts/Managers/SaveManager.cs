using System.Net;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager> {

    [Header("Default Values")]
    [SerializeField] float playerStartLife;
    [SerializeField] int firstLevel;
    [SerializeField] int defaultCloth;
    [SerializeField] int defaultCheckpoint;
    [Header("Prefs Keys")]
    [SerializeField] string clothKey = "ClothKey";
    [SerializeField] string checkpointKey = "CheckpointKey";

    SaveSetup _saveSetup;
    string _path;
    

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
        _path = Application.dataPath + "/save.txt";
    }

    void ResetSetup() {
        _saveSetup.currentLevel = firstLevel;
        _saveSetup.playerLife = playerStartLife;
        _saveSetup.currentCheckpoint = defaultCheckpoint;
        _saveSetup.clothValue = defaultCloth;
        _saveSetup.coins = 0;
        _saveSetup.lifes = 0;
        PlayerPrefs.SetInt(clothKey, _saveSetup.clothValue);
        PlayerPrefs.SetInt(checkpointKey, _saveSetup.currentCheckpoint);
    }

    void Save() {
        string setupJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupJson);
        SaveFile(setupJson);
    }

    void SaveFile(string json) {
        File.WriteAllText(_path, json);
    }

    void LoadFile() {
        string fileLoaded = "";
        if (VerifySaveExists()) fileLoaded = File.ReadAllText(_path);
        _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
    }

    void SaveDefaults() {
        _saveSetup.coins = InventoryManager.Instance.GetItemValueByType(ItemType.COIN);
        _saveSetup.lifes = InventoryManager.Instance.GetItemValueByType(ItemType.LIFE);
        _saveSetup.clothValue = PlayerPrefs.GetInt(clothKey, 0);
        _saveSetup.playerLife = Player.Instance.GetCurrentLife();
    }

    public void NewGame() {
        ResetSetup();
        if (VerifySaveExists()) File.Delete(_path);
    }

    public void Load() {
        LoadFile();
        PlayerPrefs.SetInt(clothKey, _saveSetup.clothValue);
        PlayerPrefs.SetInt(checkpointKey, _saveSetup.currentCheckpoint);
    }

    public int GetCurrentLevel() {
        return _saveSetup.currentLevel;
    }

    public int GetCoins() {
        return _saveSetup.coins;
    }

    public int GetLifes() {
        return _saveSetup.lifes;
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
        return File.Exists(_path);
    }

}

public class SaveSetup {

    public int currentLevel;
    public int currentCheckpoint;
    public int coins;
    public int lifes;
    public float playerLife;
    public int clothValue;
}