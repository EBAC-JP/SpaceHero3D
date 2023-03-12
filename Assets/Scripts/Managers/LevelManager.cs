using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : Singleton<LevelManager> {

    [SerializeField] GameObject endScreen;
    [SerializeField] Player player;

    public void LoadEndLevel() {
        player.SetEndLevel();
        endScreen.SetActive(true);
    }

}
