using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PC_BState_Move : BehaviorStateBase
{
    private Move nextMove;
    public PC_BState_Move(PlayerController controller) : base(controller)
    {
        this.nextMove = default;
    }
    public override void OnEnterState()
    {
        Debug.Log("Enter Move State");
        /* Do Nothing */
    }
    public override void OnExitState()
    {
        Debug.Log("Exit Move State");
        /* Do Nothing */
    }
    public override void OnUpdateState()
    {
        SetMove();
    }
    public override void OnFixedUpdateState()
    {
        MoveExecute();
    }
    void SetMove()
    {
        nextMove = playerController.nextMove;
    }   // SetMove()
    void MoveExecute()
    {
        if(nextMove != null)
            nextMove.Execute();
    } // MoveExecute()
}
