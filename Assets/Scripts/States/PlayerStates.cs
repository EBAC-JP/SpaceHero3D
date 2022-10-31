using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : StateBase {

    public override void OnStateEnter(GameObject obj = null) {
        Player.Instance.RunAnimation(false);
    }
}

public class PlayerStateWalk : StateBase {

    public override void OnStateEnter(GameObject obj = null) {
        Player.Instance.RunAnimation(true);
        Player.Instance.DefineVelocity();
    }

    public override void OnStateUpdate() {
        Player.Instance.Walk();
    }
}

public class PlayerStateJump : StateBase {

    public override void OnStateEnter(GameObject obj = null) {
        Player.Instance.RunAnimation(false);
        Player.Instance.Jump();
    }
}
