using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemDefense : ClothItemBase {
    
    [Header("Defense")]
    [SerializeField] float newDefense;
    [SerializeField] float duration;

    protected override void OnCollect() {
        Player.Instance.ChangeTexture(_setup);
        Player.Instance.ChangeDefense(newDefense, duration);
    }

}
