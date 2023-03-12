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
        _saveSetup = new SaveSetup(1, 1);
        path = Application.persistentDataPath + "/save.txt";
    }

    [NaughtyAttributes.Button]
    void Save() {
        string setupJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupJson);
        SaveFile(setupJson);
    }

    void SaveFile(string json) {
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

}

public class SaveSetup {

    public int currentLevel;
    public int currentCheckpoint;

    public SaveSetup(int level, int checkpoint) {
        currentLevel = level;
        currentCheckpoint = checkpoint;
    }

}