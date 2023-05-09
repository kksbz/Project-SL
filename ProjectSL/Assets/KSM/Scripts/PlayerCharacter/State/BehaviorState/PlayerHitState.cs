using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState(PlayerBaseState prevState = null)
    {
        Debug.Log("Hit State Enter");
        Ctx.CharacterAnimator.applyRootMotion = true;
        Ctx.CombatController.Hit();

        // 아이템 사용중 맞은경우에 무기, 소비아이템 숨김 체크해서 다시 보이게 하거나 숨기기
        if(Ctx.EquipmentController.IsHideRightWeapon())
        {
            Ctx.EquipmentController.ShowRightWeapon();
        }
        if(Ctx.EquipmentController.IsHideLeftWeapon())
        {
            Ctx.EquipmentController.ShowLeftWeapon();
        }
        if(!Ctx.EquipmentController.IsHideRecoveryConsumption())
        {
            Ctx.EquipmentController.HideRecoveryConsumption();
        }
        
        Ctx.HitFlag = false;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        if(Ctx.HitFlag)
        {
            Ctx.CombatController.Hit();
            Ctx.HitFlag = false;
        }
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState(PlayerBaseState nextState = null)
    {
        Debug.Log("Hit State Exit");
        Ctx.CharacterAnimator.applyRootMotion = false;
    }
    public override void CheckSwitchStates()
    {
        if(Ctx.DeadFlag)
        {
            SwitchState(Factory.Dead());
        }
        else if(!Ctx.CombatController.IsHit)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {

    }
}
