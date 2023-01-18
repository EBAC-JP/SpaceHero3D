using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothManager : Singleton<ClothManager> {
  
    [SerializeField] List<ClothSetup> clothSetups;

    public ClothSetup GetSetupByType(ClothType type) {
        return clothSetups.Find(i => i.clothType == type);
    }

}

public enum ClothType {
    BASIC,
    BASIC_V2,
    SPEED,
    DEFENSE
}

[System.Serializable]
public class ClothSetup {

    public ClothType clothType;
    public Texture2D clothTexture;
    public Color visorColor;
}
