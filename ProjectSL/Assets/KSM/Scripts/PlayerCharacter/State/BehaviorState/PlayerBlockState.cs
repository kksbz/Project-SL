using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    public PlayerBlockState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Debug.Log("Hit State Enter");
        Ctx.CharacterAnimator.applyRootMotion = true;
        Ctx.CombatController.Block();
        Ctx.BlockFlag = false;
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
        Debug.Log("Block State Exit");
        Ctx.CharacterAnimator.applyRootMotion = false;
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.CombatController.IsBlock)
        {
            SwitchState(Factory.Guard());
        }
    }
    public override void InitializeSubState()
    {

    }
}
