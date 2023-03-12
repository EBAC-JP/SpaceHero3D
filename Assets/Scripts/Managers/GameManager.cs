using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    [SerializeField] GameObject endScreen;
    [SerializeField] Player player;
    [SerializeField] string checkpointKey = "CheckpointKey";

    public void LoadEndLevel() {
        player.SetEndLevel();
        endScreen.SetActive(true);
    }

    public void NextLevel() {
        PlayerPrefs.SetInt(checkpointKey, 0);
        SaveManager.Instance.SaveEndLevel();
    }

    public void LoadGame() {
        SaveManager.Instance.Load();
        LoadScene(1);
    }

    public void NewGame() {
        SaveManager.Instance.NewGame();
        LoadScene(1);
    }

    public void LoadScene(int value) {
        SceneManager.LoadScene(value);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
