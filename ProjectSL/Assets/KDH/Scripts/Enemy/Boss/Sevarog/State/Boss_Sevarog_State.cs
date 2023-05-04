using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ProjectSL.Enemy;

public class Boss_Sevarog_Swing1_Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing1_Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        //_boss.SetStop(true);

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
        _boss.NotAttackColliderEnabled();
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

public class Boss_Sevarog_Swing2_Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing2_Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SWING2);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Swing2"))
        {
            _boss.SetState(new Boss_Sevarog_Swing2_1_Attack_State(_boss));
        }
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled();
    }
}

public class Boss_Sevarog_Swing2_1_Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing2_1_Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SWING2_1);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Swing2_1"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled();
    }
}
public class Boss_Sevarog_Swing3_Attack_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Swing3_Attack_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SWING3);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Swing3"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled();
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
        _boss.Teleport();
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
        _boss.SubjugationPattern();
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

public class Boss_Sevarog_Enrage_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Enrage_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.StartCoroutine(AnimationDelay());

        //  원거리 공격 수행 예정
        _boss.EnragePattern();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd(EnemyDefineData.ANIMATION_ENRAGE))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {
    }


    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(0.6f);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ENRAGE);
    }
}

public class Boss_Sevarog_EnemySpawn_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_EnemySpawn_State(Boss_Sevarog newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger(EnemyDefineData.TRIGGER_SUBJUGATION);

        _boss.StartCoroutine(SpawnDelay());
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

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.2f);
        _boss.EnemySpawn();
    }
}