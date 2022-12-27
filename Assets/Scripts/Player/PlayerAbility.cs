using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour {

    protected Player player;

    void OnValidate() {
        if (player != null) player = GetComponent<Player>();
    }

    void Start() {
        Init();
        OnValidate();
        RegisterListeners();
    }

    void OnDestroy() {
        RemoveListeners();
    }

    protected virtual void Init() {}
    protected virtual void RegisterListeners() {}
    protected virtual void RemoveListeners() {}

}
