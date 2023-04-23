using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState()
    {
        // Attack 애니메이션 실행
        Ctx.CharacterAnimator.applyRootMotion = true;
        Ctx.CombatController.Attack();
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        if(Ctx.IsAttackPressed)
        {
            Ctx.CombatController.Attack();
        }
        Ctx.CombatController.NextAttackCheck();
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
        if(!Ctx.CombatController.IsAttacking)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {

    }
}
