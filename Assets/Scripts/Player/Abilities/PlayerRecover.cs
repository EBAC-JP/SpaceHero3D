using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRecover : PlayerAbility {
    
    [SerializeField] InputAction health;
    [SerializeField] int recoverAmount;
    [SerializeField] ParticleSystem particle;

    protected override void Init() {
        health.Enable();
        health.performed += ctx => Recover();
    }

    void Recover() {
        if (InventoryManager.Instance.GetItemValueByType(ItemType.LIFE) > 0) {
            player.Recover(recoverAmount);
            InventoryManager.Instance.RemoveByType(ItemType.LIFE);
            particle?.Play();
        }
    }

}