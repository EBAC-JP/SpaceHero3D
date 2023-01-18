using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemBase : MonoBehaviour {

    [SerializeField] string targetTag;
    [SerializeField] ClothType clothType;
    [SerializeField] List<SkinnedMeshRenderer> meshes;
    [SerializeField] SkinnedMeshRenderer visor;
    [SerializeField] GameObject graphicItem;

    ClothSetup _setup;
    Collider _collider;

    void Start() {
        _collider = GetComponent<Collider>();
        _setup = ClothManager.Instance.GetSetupByType(clothType);
        meshes.ForEach(i => i.material.SetTexture("_EmissionMap", _setup.clothTexture));
        visor.material.SetColor("_EmissionColor", _setup.visorColor);
    }

    protected virtual void Collect() {
        if (_collider) _collider.enabled = false;
        if (graphicItem != null) graphicItem.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect() {
        Player.Instance.ChangeTexture(_setup);
        Player.Instance.SetDefaultClothSetup(_setup);
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag(targetTag)) {
            Collect();
        }
    }
}
