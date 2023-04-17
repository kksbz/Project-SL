using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : EnemyBase
{
    protected override void Init()
    {
        base.Init();
        SetState(new Enemy_Idle_State(this));
    }
}
