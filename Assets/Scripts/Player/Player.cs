using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player> {

    [Header("Moviments")]
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] float friction;
    [SerializeField] KeyCode jumpKey;
    [SerializeField] KeyCode frontKey;
    [SerializeField] KeyCode backKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;

    public enum PlayerStates {
        IDLE,
        WALK,
        JUMP
    }
    public enum Directions {
        LEFT,
        RIGHT,
        FRONT,
        BACK
    }
    public StateMachine<PlayerStates> stateMachine;

    Directions _direction;
    Vector3 _velocity;
    Rigidbody _myRigid;

    void Start() {
        InitStateMachine();
        _myRigid = GetComponent<Rigidbody>();
    }

    void Update() {
        stateMachine.Update();
        if (_myRigid.velocity == Vector3.zero && stateMachine.currentState.ToString() != "PlayerStateIdle")
            stateMachine.SwitchState(PlayerStates.IDLE); 
        if (Input.GetKeyDown(jumpKey)) stateMachine.SwitchState(PlayerStates.JUMP);
        HandleMoviments();
    }

    void HandleMoviments() {
        _velocity = Vector3.zero;
        if (Input.GetKey(frontKey)) {
            _direction = Directions.FRONT;
            stateMachine.SwitchState(PlayerStates.WALK);
        } else if(Input.GetKey(backKey)) {
            _direction = Directions.BACK;
            stateMachine.SwitchState(PlayerStates.WALK);
        } else if(Input.GetKey(leftKey)) {
            _direction = Directions.LEFT;
            stateMachine.SwitchState(PlayerStates.WALK);
        } else if(Input.GetKey(rightKey)) {
            _direction = Directions.RIGHT;
            stateMachine.SwitchState(PlayerStates.WALK);
        }
    }

    void InitStateMachine() {
        stateMachine = new StateMachine<PlayerStates>(PlayerStates.IDLE, new PlayerStateIdle());
        stateMachine.RegisterState(PlayerStates.WALK, new PlayerStateWalk());
        stateMachine.RegisterState(PlayerStates.JUMP, new PlayerStateJump());
    }

    void HandleFriction() {
        if (_myRigid.velocity.z > 0) _myRigid.velocity += new Vector3(0, 0, friction);
        else if (_myRigid.velocity.z < 0) _myRigid.velocity += new Vector3(0, 0, -friction);
        else if (_myRigid.velocity.x < 0) _myRigid.velocity += new Vector3(-friction, 0, 0);
        else if (_myRigid.velocity.x > 0) _myRigid.velocity += new Vector3(friction, 0, 0);
    }

    void Flip() {
        if (_direction == Directions.FRONT) transform.DORotate(new Vector3(0, 0, 0), .1f); 
        else if (_direction == Directions.BACK) transform.DORotate(new Vector3(0, 180, 0), .1f);
        else if (_direction == Directions.LEFT) transform.DORotate(new Vector3(0, 270, 0), .1f);
        else if (_direction == Directions.RIGHT) transform.DORotate(new Vector3(0, 90, 0), .1f);
    }

    public void DefineVelocity() {
        if (_direction == Directions.FRONT) _velocity.z = speed;
        else if (_direction == Directions.BACK) _velocity.z = -speed;
        else if (_direction == Directions.LEFT) _velocity.x = -speed;
        else if (_direction == Directions.RIGHT) _velocity.x = speed;
    }

    public void Jump() {
        _myRigid.velocity = Vector3.up * jumpForce;
    }

    public void Walk() {
        _myRigid.velocity = _velocity;
        Flip();
        HandleFriction();
    }

}
