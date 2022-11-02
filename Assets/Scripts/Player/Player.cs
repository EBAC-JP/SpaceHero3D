using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player> {

    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float gravity;
    [Header("Animations")]
    [SerializeField] Animator myAnim;
    [SerializeField] string runTrigger = "Run";

    CharacterController _myChar;
    float _currentSpeed;

    void Start() {
        _myChar = GetComponent<CharacterController>();
    }

    void Update() {
        _currentSpeed = Input.GetAxis("Vertical");
        HandleMoviments();
        HandleAnimation();
    }

    void HandleAnimation() {
        myAnim.SetBool(runTrigger, _currentSpeed != 0);
    }

    void HandleMoviments() {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        transform.Rotate(rotation);
        Vector3 frontSpeed = transform.forward * _currentSpeed * speed;
        frontSpeed.y = Time.deltaTime * (-gravity);
        _myChar.Move(frontSpeed * Time.deltaTime);
    }

}
