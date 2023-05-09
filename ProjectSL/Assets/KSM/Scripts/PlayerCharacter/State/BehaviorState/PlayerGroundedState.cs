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
    public override void EnterState(PlayerBaseState prevState = null)
    {
        //Debug.Log("Enter Grounded State");
        if(prevState != null && prevState is PlayerBlockState)
        {
            Ctx.CombatController.GuardOnOffState();
            Ctx.CombatController.GuardTransitionState();
        }
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState(PlayerBaseState nextState = null)
    {
        //Debug.Log("Exit Grounded State");
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsRollPressed || Ctx.IsBackStepPressed)
        {
            SwitchState(Factory.Roll());
        }
        else if (Ctx.IsGuardPressed && Ctx.EquipmentController.IsAviliableGuard())
        {
            SwitchState(Factory.Guard());
        }
        else if (Ctx.HitFlag)
        {
            SwitchState(Factory.Hit());
        }
        else if(Ctx.IsUseRecoveryItemPressed && Ctx.EquipmentController.IsUsableRecoveryConsumption())
        {
            SwitchState(Factory.UseItem());
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
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed && Ctx.PlayerCharacter.HealthSys.IsAvailableAction())
        {
            //Debug.Log("GroundedState SetSubState Run");
            SetSubState(Factory.Run());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsWalkPressed)
        {
            //Debug.Log("GroundedState SetSubState Walk");
            SetSubState(Factory.Walk());
        }
    }
}
