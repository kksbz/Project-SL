using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_BState_Idle : BehaviorStateBase
{
    private Behavior wait;
    private Transform cameraArm;
    private Transform characterBody;
    public PC_BState_Idle(PlayerController playerController) : base(playerController)
    {
        wait = default;
    }
    public override void OnEnterState()
    {
        Debug.Log("Enter Idle State");
        /* Do Nothing */ 
    }
    public override void OnExitState()
    {
        Debug.Log("Exit Idle State");
        /* Do Nothing */
    }
    public override void OnUpdateState()
    {
        SetMove();
        /* Do Nothing */
    }
    public override void OnFixedUpdateState()
    {
        IdleExecute();
    }

    void SetMove()
    {
        wait = playerController.wait;
    }   // SetMove()
    void IdleExecute()
    {
        if(wait != null)
        {
            wait.Execute();
        }
    }   // IdleExecute()
}
