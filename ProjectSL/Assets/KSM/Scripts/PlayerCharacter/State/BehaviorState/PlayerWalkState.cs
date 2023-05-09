using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState(PlayerBaseState prevState = null)
    {
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_WALKLAYER, 1);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.SetMoveDirection();
        // 임시로 걷기 속도 5 * 나중에 스탯에서 가져올수 있음
        Move nextMove = new Move(Ctx.Rigidbody, Ctx.AppliedMovement, 2f);
        Ctx.NextBehavior = nextMove;
    }
    public override void FixedUpdateState()
    {
        Debug.Log("JogState FixedUpdateState()");
        if (Ctx.NextBehavior != null)
        {
            Debug.Log("JogState FixedUpdateState context nextBehavior.Execute()");
            Ctx.NextBehavior.Execute();
            Ctx.NextBehavior = null;
        }
    }
    public override void ExitState(PlayerBaseState nextState = null)
    {
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_WALKLAYER, 0);
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Jog());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
    }
    public override void InitializeSubState()
    {

    }
}
