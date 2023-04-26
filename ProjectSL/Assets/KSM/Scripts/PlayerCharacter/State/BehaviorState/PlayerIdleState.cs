using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        //
        Ctx.SetMoveDirection();
        // 임시로 조깅 속도 5 * 나중에 스탯에서 가져올수 있음
        //Move nextMove = new Move(Ctx.CharacterController, Ctx.AppliedMovement, 5f);
        //Ctx.NextBehavior = nextMove;
        //
    }
    public override void FixedUpdateState()
    {
        
    }
    public override void ExitState()
    {

    }
    public override void CheckSwitchStates()
    {
        if(Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsWalkPressed)
        {
            //Debug.Log("Switch Jog State");
            SwitchState(Factory.Jog());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState()
    {

    }
}
