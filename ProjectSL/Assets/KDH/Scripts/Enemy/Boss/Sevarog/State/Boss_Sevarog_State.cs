using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ProjectSL.Enemy;

public class Boss_Sevarog_Swing1Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing1Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetStop(true);

        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SWING1);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Swing1"))
        {
            _boss.SetState(new Boss_Sevarog_Swing1_Return_State(_boss));
        }
    }
    public void OnAction()
    {
        _boss.NotAttackCOlliderEnabled();
    }
}

public class Boss_Sevarog_Swing1_Return_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing1_Return_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SWING1_RETURN);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Swing1_Return"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
    }
}

public class Boss_Sevarog_Teleport_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Teleport_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_TELEPORT);
        _boss.BossStatus.hitCount = 0;
        _boss.Warp();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd(EnemyDefineData.ANIMATION_TELEPORT))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
    }
}

public class Boss_Sevarog_Subjugation_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Subjugation_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SUBJUGATION);

        //  원거리 공격 수행 예정
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd(EnemyDefineData.ANIMATION_SUBJUGATION))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
    }
}