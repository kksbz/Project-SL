using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Debug.Log("Attack State Enter");
        // Attack �ִϸ��̼� ����
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
        Debug.Log("Attack State Exit");
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
