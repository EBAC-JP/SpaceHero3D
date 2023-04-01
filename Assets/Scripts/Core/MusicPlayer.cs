using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] MusicType musicType;

    void Start() {
        PlayMusic();
    }

    void PlayMusic() {
        audioSource.clip = AudioManager.Instance.GetMusicClipByType(musicType);
        audioSource.Play();
    }

}