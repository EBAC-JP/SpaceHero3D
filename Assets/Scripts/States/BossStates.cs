using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateBase : StateBase {

    protected BossBase _boss;

    public override void OnStateEnter(params object[] objs) {
        _boss = (BossBase)objs[0];
    }
}

public class BossStateInit : BossStateBase {

    public override void OnStateEnter(params object[] objs) {
        base.OnStateEnter(objs);
        _boss.BornAnimation();
    }
}

public class BossStateWalk : BossStateBase {

    public override void OnStateEnter(params object[] objs) {
        base.OnStateEnter(objs);
        _boss.WalkToPoint();
    }
}