using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : Singleton<EffectsManager> {

    [SerializeField] PostProcessVolume processVolume;
    [SerializeField] float vignetteDuration;

    Vignette _vignette;

    void Start() {
        processVolume.profile.TryGetSettings<Vignette>(out _vignette);
    }

    public void DisplayVignette() {
        StartCoroutine(FlashVignette());
    }

    IEnumerator FlashVignette() {
        _vignette.active = true;
        yield return new WaitForSeconds(vignetteDuration);
        _vignette.active = false;
    }

}
