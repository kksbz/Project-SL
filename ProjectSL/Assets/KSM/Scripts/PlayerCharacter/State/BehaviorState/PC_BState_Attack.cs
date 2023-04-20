using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_BState_Attack : BehaviorStateBase
{
    public PC_BState_Attack(PlayerController controller) : base(controller)
    {

    }
    public override void OnEnterState()
    {
        Debug.Log("Enter Attack State");
        /* Do Nothing */
    }
    public override void OnExitState()
    {
        Debug.Log("Exit Attack State");
        /* Do Nothing */
    }
    public override void OnUpdateState()
    {

    }
    public override void OnFixedUpdateState()
    {

    }
}
