using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Ctx.CharacterAnimator.applyRootMotion = true;
        Ctx.PlayerController.Die();
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
        
    }
    public override void InitializeSubState()
    {
        
    }
}
