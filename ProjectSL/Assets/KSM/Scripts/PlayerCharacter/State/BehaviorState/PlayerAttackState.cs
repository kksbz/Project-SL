using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState(PlayerBaseState prevState = null)
    {
        //Debug.Log("Attack State Enter");
        // Attack 애니메이션 실행
        Ctx.CharacterAnimator.applyRootMotion = true;
        Ctx.CombatController.Attack();
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.SetCurrentMovement();
        if(Ctx.IsAttackPressed)
        {
            Ctx.CombatController.Attack();
        }
        Ctx.CombatController.NextAttackCheck();
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState(PlayerBaseState nextState = null)
    {
        //Debug.Log("Attack State Exit");
        // Ctx.CombatController.ResetAnimatorController();
        Ctx.CharacterAnimator.applyRootMotion = false;
    }
    public override void CheckSwitchStates()
    {
        if(!Ctx.CombatController.IsAttacking)
        {
            SwitchState(Factory.Grounded());
        }
        else if(Ctx.IsRollPressed) 
        {
            SwitchState(Factory.Roll());
        }
        else if(Ctx.HitFlag)
        {
            SwitchState(Factory.Hit());
        }
    }
    public override void InitializeSubState()
    {

    }
}
