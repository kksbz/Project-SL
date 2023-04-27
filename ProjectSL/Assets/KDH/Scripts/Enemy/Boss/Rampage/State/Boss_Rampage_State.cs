using UnityEngine;
using ProjectSL.Enemy;
using System.Collections;

public class Boss_Rampage_RockRaise_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_RockRaise_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Attack");
        _boss.SetTrigger("RockRaise");

        _boss.RockRaise();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Rock_Raise"))
        {
            _boss.SetState(new Boss_Rampage_RockThrow_Staet(_boss));
        }
    }
}

public class Boss_Rampage_RockThrow_Staet : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_RockThrow_Staet(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("RockThrow");

        _boss.RockThrow();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Rock_Throw"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}

public class Boss_Ramapage_BodyTackle_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Ramapage_BodyTackle_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("RockThrow");

        _boss.RockThrow();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Rock_Throw"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}

public class Boss_Rampage_JumpStart_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_JumpStart_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("RockThrow");

        _boss.RockThrow();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Rock_Throw"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}

public class Boss_Rampage_Attack_A_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Attack_A_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Attack_A");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Attack_A"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}
public class Boss_Rampage_Attack_B_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Attack_B_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Attack_B");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Attack_B"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}
public class Boss_Rampage_Attack_C_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Attack_C_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {


    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Attack_C");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Attack_C"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}

public class Boss_Rampage_Dodge_Start_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_Start_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_Start");

        _boss.StartCoroutine(DodgeDelay());
    }

    public void OnExit()
    {

    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Dodge_Start"))
        {
            _boss.SetState(new Boss_Rampage_Dodge_Mid_State(_boss));
        }
    }

    IEnumerator DodgeDelay()
    {
        yield return new WaitForSeconds(0.2f);
        _boss.Dodge();
    }
}

public class Boss_Rampage_Dodge_Mid_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_Mid_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_Mid");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Dodge_Mid"))
        {
            _boss.SetState(new Boss_Rampage_Dodge_Loop_State(_boss));
        }
    }
}

public class Boss_Rampage_Dodge_Loop_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_Loop_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_Loop");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.DodgeCompleteCheck())
        {
            _boss.SetState(new Boss_Rampage_Dodge_End_State(_boss));
        }
    }
}

public class Boss_Rampage_Dodge_End_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_End_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_End");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Dodge_End"))
        {
            _boss.DodgeComplete();
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}

#region Lagacy Code
// public class Boss_Rampage_Dodge_Left_State : IState
// {
//     private Boss_Rampage _boss;
//     public Boss_Rampage_Dodge_Left_State(Boss_Rampage boss)
//     {
//         _boss = boss;
//     }
//     public void OnAction()
//     {
//     }

//     public void OnEnter()
//     {
//     }

//     public void OnExit()
//     {
//     }

//     public void Update()
//     {
//     }
// }

// public class Boss_Rampage_Dodge_Right_State : IState
// {
//     private Boss_Rampage _boss;
//     public Boss_Rampage_Dodge_Right_State(Boss_Rampage boss)
//     {
//         _boss = boss;
//     }
//     public void OnAction()
//     {
//     }

//     public void OnEnter()
//     {
//     }

//     public void OnExit()
//     {
//     }

//     public void Update()
//     {
//     }
// }

// public class Boss_Rampage_Dodge_Back_State : IState
// {
//     private Boss_Rampage _boss;
//     public Boss_Rampage_Dodge_Back_State(Boss_Rampage boss)
//     {
//         _boss = boss;
//     }
//     public void OnAction()
//     {
//     }

//     public void OnEnter()
//     {
//         _boss.Dodge();
//         _boss.SetTrigger("Dodge");
//     }

//     public void OnExit()
//     {
//     }

//     public void Update()
//     {
//         // if (_boss.IsAnimationEnd("Dodge") && _boss.IsNavMeshRangeChecked(1f))
//         // {
//         //     _boss.DodgeComplete();
//         //     _boss.SetState(new Boss_Thought_State(_boss));
//         // }
//         if (_boss.IsNavMeshRangeChecked(0.1f))
//         {
//             _boss.DodgeComplete();
//             _boss.SetState(new Boss_Thought_State(_boss));
//         }
//     }
// }
#endregion