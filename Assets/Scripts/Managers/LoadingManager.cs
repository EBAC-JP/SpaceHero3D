using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

    [SerializeField] Image loadBar;

    void Start() {
        int level = SaveManager.Instance.GetCurrentLevel();
        StartCoroutine(LoadSceneAsync(level));
    }

    IEnumerator LoadSceneAsync(int level) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);
            loadBar.fillAmount = progressValue;
            yield return null;
        }
    }

}
