using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

    [SerializeField] string levelKey = "LevelKey";
    [SerializeField] int firstLevelValue = 2;
    [SerializeField] UIUpdater loadBar;

    void Start() {
        int level = PlayerPrefs.GetInt(levelKey, firstLevelValue);
        StartCoroutine(LoadSceneAsync(level));
    }

    IEnumerator LoadSceneAsync(int level) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);
            loadBar.UpdateValue(progressValue);
            yield return null;
        }
    }

}
