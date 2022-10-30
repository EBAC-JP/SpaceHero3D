using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {
    
    bool _showStateFoldout;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        GameManager gm = (GameManager)target;

        #region StateMachine
        if (gm.stateMachine == null) return;
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("State Machine");
        if (gm.stateMachine.currentState != null)
            EditorGUILayout.LabelField(string.Format("Current State: {0}", gm.stateMachine.currentState.ToString()));
        _showStateFoldout = EditorGUILayout.Foldout(_showStateFoldout, "Avaliable States");
        if (_showStateFoldout) {
            if (gm.stateMachine.stateDict != null) {
                var keys = gm.stateMachine.stateDict.Keys.ToArray();
                var vals = gm.stateMachine.stateDict.Values.ToArray();
                for (int i = 0; i < keys.Length; i++)
                   EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
            }
        }
        #endregion
    }

}
