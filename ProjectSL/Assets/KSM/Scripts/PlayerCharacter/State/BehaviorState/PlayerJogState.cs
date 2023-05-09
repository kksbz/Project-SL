using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJogState : PlayerBaseState
{
    public PlayerJogState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState(PlayerBaseState prevState = null)
    {
        
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.SetMoveDirection();
        // 임시로 조깅 속도 5 * 나중에 스탯에서 가져올수 있음
        Move nextMove = new Move(Ctx.Rigidbody, Ctx.AppliedMovement, 5f);
        Ctx.NextBehavior = nextMove;
    }
    public override void FixedUpdateState()
    {
        //Debug.Log("JogState FixedUpdateState()");
        if(Ctx.NextBehavior != null)
        {
            //Debug.Log("JogState FixedUpdateState context nextBehavior.Execute()");
            Ctx.NextBehavior.Execute();
            Ctx.NextBehavior = null;
        }
    }
    public override void ExitState(PlayerBaseState nextState = null)
    {

    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed && Ctx.PlayerCharacter.HealthSys.IsAvailableAction())
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState()
    {

    }
}
