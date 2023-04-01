using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour {
    
    [SerializeField] GameObject continueButton;
    [Header("Audio")]
    [SerializeField] GameObject volume;
    [SerializeField] GameObject muted;
    [SerializeField] AudioMixer masterMixer;

    bool _isMuted;

    void Start() {
        continueButton.SetActive(SaveManager.Instance.VerifySaveExists());
        _isMuted = (PlayerPrefs.GetInt("Mute", 0) != 0);
        Init();
    }

    void Init() {
        if (!_isMuted) UnMute();
        else Mute();
    }

    public void UnMute() {
        volume.SetActive(true);
        muted.SetActive(false);
        masterMixer.SetFloat("masterVol", 0.0f);
        PlayerPrefs.SetInt("Mute", 0);
    }

    public void Mute() {
        volume.SetActive(false);
        muted.SetActive(true);
        masterMixer.SetFloat("masterVol", -80.0f);
        PlayerPrefs.SetInt("Mute", 1);
    }
}
