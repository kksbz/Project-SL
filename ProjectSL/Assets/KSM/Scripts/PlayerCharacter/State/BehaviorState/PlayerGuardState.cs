using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardState : PlayerBaseState
{
    public PlayerGuardState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("Enter Guard State");
        Ctx.CombatController.OnGuard();
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
        Ctx.CombatController.OffGuard();
    }
    public override void CheckSwitchStates()
    {
        if(!Ctx.IsGuardPressed)
        {
            SwitchState(Factory.Grounded());
        }
        else if(Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsRollPressed || Ctx.IsBackStepPressed)
        {
            SwitchState(Factory.Roll());
        }
    }
    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            //Debug.Log("GroundedState SetSubState Idle");
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsWalkPressed)
        {
            //Debug.Log("GroundedState SetSubState Jog");
            SetSubState(Factory.Jog());
        }
    }
}
