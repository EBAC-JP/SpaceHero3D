using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager> {

    SaveSetup _saveSetup;
    string path;

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
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

}

public class SaveSetup {

    public int currentLevel;
    public int currentCheckpoint;
    public int coins;
    public int lifeItem;
}