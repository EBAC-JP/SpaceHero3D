using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    
    [SerializeField] GameObject continueButton;

    void Start() {
        continueButton.SetActive(SaveManager.Instance.VerifySaveExists());
    }
}
