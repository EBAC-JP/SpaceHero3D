using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemSpeed : ClothItemBase {
    
    [Header("Speed")]
    [SerializeField] float newSpeed;
    [SerializeField] float duration;

    protected override void OnCollect() {
        Player.Instance.ChangeTexture(_setup);
        Player.Instance.ChangeSpeed(newSpeed, duration);
    }

}
