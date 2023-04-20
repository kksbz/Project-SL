using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_BState_Idle : BehaviorStateBase
{
    private Move nextMove;
    private Transform cameraArm;
    private Transform characterBody;
    public PC_BState_Idle(PlayerController playerController) : base(playerController)
    {
        nextMove = default;
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
        nextMove = playerController.nextMove;
    }   // SetMove()
    void IdleExecute()
    {
        if(nextMove != null)
        {
            nextMove.Execute();
        }
    }   // IdleExecute()
}
