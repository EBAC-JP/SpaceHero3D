using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItem : MonoBehaviour {

    [SerializeField] Image icon;
    [SerializeField] TMP_Text value;

    ItemSetup _setup;

    public void Load(ItemSetup setup) {
        _setup = setup;
        icon.sprite = _setup.itemImage;
    }

    void Update() {
        value.text = _setup.itemValue.ToString();        
    }
}
