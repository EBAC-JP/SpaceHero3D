using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor {
    
    bool _showStateFoldout, _stateMachine;
    Player _target;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        _target = (Player)target;
        if (_target.stateMachine != null) StateMachineGUI();
    }

    void StateMachineGUI() {
        EditorGUILayout.Space(10);
        _stateMachine = EditorGUILayout.Foldout(_stateMachine, "StateMachine");
        if (_stateMachine) {
            if (_target.stateMachine.currentState != null)
                EditorGUILayout.LabelField(string.Format("Current State: {0}", _target.stateMachine.currentState.ToString()));
            _showStateFoldout = EditorGUILayout.Foldout(_showStateFoldout, "Avaliable States");
            if (_showStateFoldout) {
                if (_target.stateMachine.stateDict != null) {
                    var keys = _target.stateMachine.stateDict.Keys.ToArray();
                    var vals = _target.stateMachine.stateDict.Values.ToArray();
                    for (int i = 0; i < keys.Length; i++)
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }

}