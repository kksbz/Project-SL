using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{
    PlayerStateMachine _context;
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }
    
    public PlayerBaseState Jog()
    {
        return new PlayerJogState(_context, this);
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }
    
    public PlayerBaseState Attack()
    {
        return new PlayerAttackState(_context, this);
    }
    public PlayerBaseState Guard()
    {
        return new PlayerGuardState(_context, this);
    }
    public PlayerBaseState Roll()
    {
        return new PlayerRollState(_context, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
}
