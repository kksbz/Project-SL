using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("Enter Grounded State");
    }
    public override void UpdateState()
    {
        
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState()
    {
        Debug.Log("Exit Grounded State");
    }
    public override void CheckSwitchStates()
    {

    }
    public override void InitializeSubState()
    {
        if(!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            Debug.Log("GroundedState SetSubState Idle");
            SetSubState(Factory.Idle());
        }
        else if(Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsWalkPressed)
        {
            Debug.Log("GroundedState SetSubState Jog");
            SetSubState(Factory.Jog());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            Debug.Log("GroundedState SetSubState Run");
            SetSubState(Factory.Run());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsWalkPressed)
        {
            Debug.Log("GroundedState SetSubState Walk");
            SetSubState(Factory.Walk());
        }
    }
}
