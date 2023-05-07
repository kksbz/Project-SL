using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItemState : PlayerBaseState
{
    public PlayerUseItemState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Ctx.PlayerController.UseRecoveryItem();
        
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        if (Ctx.PlayerController.ItemAction_Recovery._isWalkable)
        {
            Ctx.SetMoveDirection();
            Move nextMove = new Move(Ctx.CharacterController, Ctx.AppliedMovement, 2f);
            Ctx.NextBehavior = nextMove;
        }
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState()
    {
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_WALKLAYER, 0);
        Ctx.AnimationController.SetWeight(AnimationController.LAYERINDEX_UPPERLAYER, 0);
        Debug.Log("Exit UseItem State");
    }
    public override void CheckSwitchStates()
    {
        if(!Ctx.UseItemFlag)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {

    }
}
