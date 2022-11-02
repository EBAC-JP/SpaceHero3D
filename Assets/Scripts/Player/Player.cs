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
    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] KeyCode jumpKey;

    CharacterController _myChar;
    float _currentSpeed, _verticalSpeed;

    void Start() {
        _myChar = GetComponent<CharacterController>();
        _verticalSpeed = 0f;
    }

    void Update() {
        _currentSpeed = Input.GetAxis("Vertical");
        _verticalSpeed -= gravity * Time.deltaTime;
        HandleJump();
        HandleMoviments();
        HandleAnimation();
    }

    void HandleJump() {
        if (_myChar.isGrounded && Input.GetKeyDown(jumpKey)) {
            _verticalSpeed = jumpSpeed;
        }
    }

    void HandleAnimation() {
        myAnim.SetBool(runTrigger, _currentSpeed != 0);
    }

    void HandleMoviments() {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        transform.Rotate(rotation);
        Vector3 frontSpeed = transform.forward * _currentSpeed * speed;
        frontSpeed.y = _verticalSpeed;
        _myChar.Move(frontSpeed * Time.deltaTime);
    }

}
