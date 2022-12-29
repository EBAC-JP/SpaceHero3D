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

    public StateMachine(T startEnum, StateBase startState) {
        stateDict = new Dictionary<T, StateBase>();
        RegisterState(startEnum, startState);
        SwitchState(startEnum);
    }

    public void Update() {
        if (_currentState != null) _currentState.OnStateUpdate();
    }

    public void SwitchState(T state, params object[] objs) {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = stateDict[state];
        _currentState.OnStateEnter(objs);
    }

    public void RegisterState(T typeEnum, StateBase state) {
        stateDict.Add(typeEnum, state);
    }
}
