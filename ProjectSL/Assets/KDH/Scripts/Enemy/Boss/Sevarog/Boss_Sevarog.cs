using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Sevarog : BossBase
{

    protected override void Init()
    {
        base.Init();
        //SetState(new Boss_None_State(this));
        SetState(new Boss_Idle_State(this));
    }

    public override IState Thought(Transform newTarget)
    {

        return new Boss_Sevarog_Example_Attack_State(this);
    }

    public override IState Thought()
    {
        // 플레이어가 근접공격 범위 안에 있을 때
        if (IsRangedChecked(Status.attackRange))
        {
            Debug.Log($"플레이어가 근접공격 범위 내에 있음 Swing1Attack State로 전환");
            return new Boss_Sevarog_Swing1Attack_State(this);
        }
        else    //  플레이어가 근접공격 범위 밖에 있을 때
        {
            //  조건에 따라 원거리 공격 혹은 플레이어 추적
            Debug.Log($"플레이어가 근접공격 범위 밖에 있음 Chase State로 전환");
            return new Boss_Chase_State(this);
        }
    }
}
