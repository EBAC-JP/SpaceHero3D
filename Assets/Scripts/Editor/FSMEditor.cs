using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FSM))]
public class FSMEditor : Editor {
    
    bool _showStateFoldout;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        FSM fsm = (FSM)target;

        #region StateMachine
        if (fsm.stateMachine == null) return;
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("State Machine");
        if (fsm.stateMachine.currentState != null)
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.currentState.ToString());
        _showStateFoldout = EditorGUILayout.Foldout(_showStateFoldout, "Avaliable States");
        if (_showStateFoldout) {
            if (fsm.stateMachine.stateDict != null) {
                var keys = fsm.stateMachine.stateDict.Keys.ToArray();
                var vals = fsm.stateMachine.stateDict.Values.ToArray();
                for (int i = 0; i < keys.Length; i++)
                   EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
            }
        }
        #endregion
    }

}
