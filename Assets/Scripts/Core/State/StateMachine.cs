using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : System.Enum {
    
    public StateBase currentState { get { return _currentState; }}
    public Dictionary<T, StateBase> stateDict;
    StateBase _currentState;

    public StateMachine() {
        stateDict = new Dictionary<T, StateBase>();
    }

    public void Update() {
        if (_currentState != null) _currentState.OnStateUpdate();
    }

    public void SwitchState(T state, GameObject obj = null) {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = stateDict[state];
        _currentState.OnStateEnter(obj);
    }

    public void RegisterState(T typeEnum, StateBase state) {
        stateDict.Add(typeEnum, state);
    }
}
