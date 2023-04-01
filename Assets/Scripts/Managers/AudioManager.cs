using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField] List<MusicSetup> musicSetups;
    [SerializeField] List<SFXSetup> sfxSetups;

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip GetMusicClipByType(MusicType musicType) {
        return musicSetups.Find(i => i.musicType == musicType).musicClip;
    }

    public AudioClip GetSFXClipByType(SFXType sfxType) {
        return sfxSetups.Find(i => i.sfxType == sfxType).sfxClip;
    }

}

public enum MusicType {
    MENU,
    LEVEL_01,
    LEVEL_02
}

[System.Serializable]
public class MusicSetup {

    public MusicType musicType;
    public AudioClip musicClip;
}

public enum SFXType {
    SHOOT,
    CRY,
    JUMP,
    COIN,
    HEALTH,
    WIN,
    CHECKPOINT
}

[System.Serializable]
public class SFXSetup {

    public SFXType sfxType;
    public AudioClip sfxClip;
}