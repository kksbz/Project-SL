using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        // Dodge 애니메이션 실행
        Ctx.CharacterAnimator.applyRootMotion = true;
        if(Ctx.IsRollPressed)
        {
            Ctx.SetDirection(Ctx.CurrentMovement);
            Ctx.CombatController.Roll();
        }
        else if(Ctx.IsBackStepPressed)
        {
            Ctx.CombatController.BackStep();
        }
        Ctx.IsRollPressed = false;
        Ctx.IsBackStepPressed = false;
        // Ctx.CombatController.();
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
        Ctx.CharacterAnimator.applyRootMotion = false;
    }
    public override void CheckSwitchStates()
    {
        if(!Ctx.CombatController.IsDodging && Ctx.IsGuardPressed)
        {
            SwitchState(Factory.Guard());
        }
        else if(!Ctx.CombatController.IsDodging)
        {
            Debug.LogWarning("Switch State Roll to Grounded");
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {

    }
}
