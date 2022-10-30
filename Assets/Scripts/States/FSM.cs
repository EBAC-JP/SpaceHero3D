using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public enum States {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }
    public StateMachine<States> stateMachine;

    void Start() {
        stateMachine = new StateMachine<States>();
        stateMachine.RegisterState(States.STATE_ONE, new StateBase());
        stateMachine.RegisterState(States.STATE_TWO, new StateBase());
        stateMachine.RegisterState(States.STATE_THREE, new StateBase());
    }

}
