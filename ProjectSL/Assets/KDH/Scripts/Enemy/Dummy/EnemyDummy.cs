using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : EnemyBase
{
    protected override void Init()
    {
        base.Init();
        SetState(new Enemy_Dummy_Idle_State(this));
    }
}
