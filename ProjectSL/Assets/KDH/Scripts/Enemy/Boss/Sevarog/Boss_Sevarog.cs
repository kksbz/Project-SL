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
}
