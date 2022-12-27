using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player> {

    [SerializeField] float rotateSpeed;
    [SerializeField] float gravity;
    [Header("Animations")]
    [SerializeField] Animator myAnim;
    [SerializeField] string runTrigger = "Run";
    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] KeyCode jumpKey;
    [Header("Run")]
    [SerializeField] float speed;
    [SerializeField] float runMultiplier;
    [SerializeField] KeyCode runKey;

    CharacterController _myChar;
    float _currentSpeed, _verticalSpeed;
    bool _isWalking;
    int _gunIndex = 0;

    void Start() {
        _myChar = GetComponent<CharacterController>();
        _verticalSpeed = 0f;
    }

    void Update() {
        _currentSpeed = Input.GetAxis("Vertical");
        _isWalking = _currentSpeed != 0;
        _verticalSpeed -= gravity * Time.deltaTime;
        HandleJump();
        HandleMoviments();
        HandleAnimation();
        HandleGuns();
    }

    void HandleGuns() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _gunIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) _gunIndex = 1;
    }

    void HandleJump() {
        if (_myChar.isGrounded && Input.GetKeyDown(jumpKey)) {
            _verticalSpeed = jumpSpeed;
        }
    }

    void HandleAnimation() {
        myAnim.SetBool(runTrigger, _isWalking);
    }

    void HandleMoviments() {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        transform.Rotate(rotation);
        Vector3 frontSpeed = transform.forward * _currentSpeed * speed;
        frontSpeed.y = _verticalSpeed;
        if (_isWalking && Input.GetKey(runKey)){
            frontSpeed *= runMultiplier;
            myAnim.speed = runMultiplier;
        } else {
            myAnim.speed = 1;
        }
        _myChar.Move(frontSpeed * Time.deltaTime);
    }

    public int GetGunIndex() {
        return _gunIndex;
    }

}
