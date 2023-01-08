using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera> {

    [SerializeField] List<CinemachineVirtualCamera> virtualCameras;
    [Header("Shake Attributes")]
    [SerializeField] float frequency;
    [SerializeField] float amplitude;
    [SerializeField] float duration;

    public void Shake() {
        virtualCameras.ForEach(vt => StartCoroutine(ShakeCoroutine(vt)));
    }

    IEnumerator ShakeCoroutine(CinemachineVirtualCamera vt) {
        CinemachineBasicMultiChannelPerlin multiChannel = vt.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannel.m_FrequencyGain = frequency;
        multiChannel.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(duration);
        multiChannel.m_FrequencyGain = 0f;
        multiChannel.m_AmplitudeGain = 0f;
    }

}
