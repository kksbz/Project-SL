using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_WALKLAYER, 1);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.SetMoveDirection();
        // �ӽ÷� �ȱ� �ӵ� 5 * ���߿� ���ȿ��� �����ü� ����
        Move nextMove = new Move(Ctx.CharacterController, Ctx.AppliedMovement, 2f);
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
    public override void ExitState()
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
