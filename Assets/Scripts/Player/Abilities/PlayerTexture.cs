using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTexture : MonoBehaviour {

    [SerializeField] List<SkinnedMeshRenderer> meshes;
    [SerializeField] SkinnedMeshRenderer visor;

    public void ChangeTextureBySetup(ClothSetup setup) {
        meshes.ForEach(i => i.material.SetTexture("_EmissionMap", setup.clothTexture));
        visor.material.SetColor("_EmissionColor", setup.visorColor);
    }
}