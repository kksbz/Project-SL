using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState(PlayerBaseState prevState = null)
    {
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_RUNLAYER, 1);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();

        Ctx.SetMoveDirection();

        // 임시로 달리는 속도 8 * 나중에 스탯에서 가져올수 있음
        Move nextMove = new Move(Ctx.Rigidbody, Ctx.AppliedMovement, 8f);
        Ctx.NextBehavior = nextMove;
    }
    public override void FixedUpdateState()
    {
        Debug.Log("JogState FixedUpdateState()");
        if (Ctx.NextBehavior != null)
        {
            Ctx.PlayerCharacter.HealthSys.Decrease_SP(Ctx.PlayerController._sprintActionCost * Time.deltaTime);
            Debug.Log("JogState FixedUpdateState context nextBehavior.Execute()");
            Ctx.NextBehavior.Execute();
            Ctx.NextBehavior = null;
        }
    }
    public override void ExitState(PlayerBaseState nextState = null)
    {
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_RUNLAYER, 0);
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if ( (Ctx.IsMovementPressed && !Ctx.IsRunPressed) || !Ctx.PlayerCharacter.HealthSys.IsAvailableAction())
        {
            SwitchState(Factory.Jog());
        }
    }
    public override void InitializeSubState()
    {

    }
}
