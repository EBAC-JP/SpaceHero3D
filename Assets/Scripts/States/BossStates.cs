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
        _boss.BornAnimation(OnArrive);
    }

    void OnArrive() {
        _boss.SwitchState(BossAction.WALK);
    }
}

public class BossStateWalk : BossStateBase {

    public override void OnStateEnter(params object[] objs) {
        base.OnStateEnter(objs);
        _boss.WalkToPoint(OnArrive);
    }

    void OnArrive() {
        _boss.SwitchState(BossAction.ATTACK);
    }
}

public class BossStateAttack : BossStateBase {

    public override void OnStateEnter(params object[] objs) {
        base.OnStateEnter(objs);
        _boss.Attack(OnArrive);
    }

    void OnArrive() {
        _boss.SwitchState(BossAction.WALK);
    }
}