using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase {

    public virtual void OnStateEnter(params object[] objs) {
        Debug.Log("State Enter not implemented!");
    }

    public virtual void OnStateUpdate() {
        Debug.Log("State Update not implemented!");
    }

    public virtual void OnStateExit() {
        Debug.Log("State Exit not implemented!");
    }
}
