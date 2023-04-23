using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState()
    {

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

    }
    public override void CheckSwitchStates()
    {
        /*if(Ctx.IsAttackPressed) // * 스테미너 조건 추가 작업 예상
        {
            SwitchState(Factory.Attack());
        }
        else */if(Ctx.IsMovementPressed && Ctx.IsRollPressed) // * 스테미너 조건 추가 작업 예상
        {
            SwitchState(Factory.Roll());
        }
        else if(Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsWalkPressed)
        {
            Debug.Log("Switch Jog State");
            SwitchState(Factory.Jog());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState()
    {

    }
}
