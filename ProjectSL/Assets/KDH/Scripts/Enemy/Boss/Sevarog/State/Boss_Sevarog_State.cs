using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ProjectSL.Enemy;

public class Boss_Sevarog_Example_Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Example_Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
    public void OnAction()
    {
        _boss.NotAttackCOlliderEnabled();
    }
}

public class Boss_Sevarog_Swing1Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing1Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SWING1);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd())
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
        _boss.NotAttackCOlliderEnabled();
    }
}