using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    public PlayerBlockState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState(PlayerBaseState prevState = null)
    {
        Debug.Log("Hit State Enter");
        Ctx.CharacterAnimator.applyRootMotion = true;
        Ctx.CombatController.Block();
        Ctx.BlockFlag = false;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        if (Ctx.BlockFlag)
        {
            Ctx.CombatController.Block();
            Ctx.BlockFlag = false;
        }
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState(PlayerBaseState nextState = null)
    {
        Debug.Log("Block State Exit");
        Ctx.CharacterAnimator.applyRootMotion = false;
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.CombatController.IsBlock && Ctx.IsGuardPressed)
        {
            SwitchState(Factory.Guard());
        }
        else if(!Ctx.CombatController.IsBlock && !Ctx.IsGuardPressed)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {

    }
}
