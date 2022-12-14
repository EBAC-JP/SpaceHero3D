using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player>, IDamageable {

    [SerializeField] float rotateSpeed;
    [SerializeField] float gravity;
    [Header("Animations")]
    [SerializeField] Animator myAnim;
    [SerializeField] string runTrigger = "Run";
    [SerializeField] string deathTrigger = "Death";
    [SerializeField] string reviveTrigger = "Revive";
    [SerializeField] float deathDuration;
    [SerializeField] List<FlashColor> flashes;
    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] KeyCode jumpKey;
    [Header("Run")]
    [SerializeField] float speed;
    [SerializeField] float runMultiplier;
    [SerializeField] KeyCode runKey;
    [Header("Health")]
    [SerializeField] UIUpdater healthBar;
    [SerializeField] float startLife;

    CharacterController _myChar;
    Collider _collider;
    float _currentSpeed, _verticalSpeed, _currentLife;
    bool _isWalking, _isDead;
    int _gunIndex = 0;

    void Start() {
        _myChar = GetComponent<CharacterController>();
        _collider = GetComponent<Collider>();
        Init();
    }

    void Update() {
        if (_isDead) return;
        _currentSpeed = Input.GetAxis("Vertical");
        _isWalking = _currentSpeed != 0;
        _verticalSpeed -= gravity * Time.deltaTime;
        HandleJump();
        HandleMoviments();
        HandleAnimation();
        HandleGuns();
    }

    void Init() {
        healthBar.UpdateValue(1);
        _currentLife = startLife;
        _isDead = false;
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

    void Damage(int damage) {
        flashes.ForEach(i => i.Flash());
        _currentLife -= damage;
        healthBar.UpdateValue(_currentLife / startLife);
        EffectsManager.Instance.DisplayVignette();
        ShakeCamera.Instance.Shake();
        if (_currentLife <= 0) OnKill();
    }

    void Revive() {
        Init();
        myAnim.SetTrigger(reviveTrigger);
        Respawn();
        Invoke(nameof(TurnOn), .1f);
    }

    void Respawn() {
        transform.position = CheckpointManager.Instance.GetLastPosition();
    }

    void TurnOn() {
        if (_collider != null) _collider.enabled = true;
        if (_myChar != null) _myChar.enabled = true;
    }

    public int GetGunIndex() {
        return _gunIndex;
    }

    public void OnDamage(int damage) {
        Damage(damage);
    }

    public void OnDamage(int damage, Vector3 direction) {
        transform.DOMove(transform.position - direction, .1f);
        Damage(damage);
    }

    public void OnKill() {
        _isDead = true;
        if (_collider != null) _collider.enabled = false;
        if (_myChar != null) _myChar.enabled = false;
        myAnim.SetTrigger(deathTrigger);
        Invoke(nameof(Revive), deathDuration);
    }
}
