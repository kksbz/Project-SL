using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSL.Enemy;

public class Enemy_Idle_State : IState
{
    private EnemyBase enemy;
    public Enemy_Idle_State(EnemyBase newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
        enemy.SetTrigger(EnemyDefineData.TRIGGER_IDLE);
        enemy.StartCoroutine(StateChange(2f));
    }

    public void OnExit()
    {
    }

    public void Update()
    {

    }

    IEnumerator StateChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (1 <= enemy.PatrolTargets.Count)
        {
            enemy.SetState(new Enemy_Patrol_State(enemy));
        }
    }

    public void OnAction()
    {

    }
}

public class Enemy_Patrol_State : IState
{
    private EnemyBase enemy;
    private IEnumerator coroutine;
    public Enemy_Patrol_State(EnemyBase newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
        enemy.SetSpeed(enemy.Status.maxMoveSpeed * 0.5f);
        enemy.SetTrigger(EnemyDefineData.TRIGGER_PATROL);
        enemy.Patrol();
        coroutine = enemy.FieldOfViewSearch(0.2f);
        enemy.StartCoroutine(coroutine);
    }

    public void OnExit()
    {
        enemy.StopCoroutine(coroutine);
    }

    public void Update()
    {
        if (enemy.IsArrive(0.2f))
        {
            enemy.SetState(new Enemy_Idle_State(enemy));
        }
        if (enemy.IsFieldOfViewFind())
        {
            enemy.SetState(new Enemy_Chase_State(enemy));
            Debug.Log($"적 찾음");
        }
    }
    public void OnAction()
    {

    }
}

public class Enemy_Chase_State : IState
{
    EnemyBase enemy;
    private Transform _target;
    private float _distance = float.MaxValue;
    public Enemy_Chase_State(EnemyBase newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
        enemy.SetSpeed(enemy.Status.maxMoveSpeed);
        enemy.SetTrigger(EnemyDefineData.TRIGGER_CHASE);
        if (1 < enemy.ChaseTargets.Count)
        {
            foreach (var element in enemy.ChaseTargets)
            {
                float distance_ = Vector3.Distance(enemy.transform.position, element.position);
                if (distance_ < _distance)
                {
                    _distance = distance_;
                    _target = element;
                }
            }
        }
        else
        {
            _target = enemy.ChaseTargets[0];
        }
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        enemy.TargetFollow(_target);
        if (enemy.IsMissed(enemy.Status.detectionRange))
        {
            enemy.SetState(new Enemy_Patrol_State(enemy));
        }
        if (enemy.IsArrive(enemy.Status.attackRange))
        {
            enemy.SetState(new Enemy_Attack_State(enemy));
        }
    }
    public void OnAction()
    {

    }
}

public class Enemy_Attack_State : IState
{
    private EnemyBase enemy;
    public Enemy_Attack_State(EnemyBase newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
        enemy.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        Debug.Log($"Enemy Attack State Debug : currentStateLength : {enemy.CurrentStateInfo.normalizedTime} / loop : {enemy.CurrentStateInfo.loop}");
        // 공격 애니메이션이 끝난 후
        if (1f <= enemy.CurrentStateInfo.normalizedTime && !enemy.CurrentStateInfo.loop)
        {
            // 타겟이 범위를 벗어났다면 놓친것으로 간주 PatrolState로 전환 그렇지 않다면 ChaseState로 전환
            if (enemy.IsMissed(enemy.Status.detectionRange))
            {
                enemy.SetState(new Enemy_Patrol_State(enemy));
            }
            else
            {
                enemy.SetState(new Enemy_Chase_State(enemy));
            }
        }
    }
    public void OnAction()
    {

    }
}

public class Enemy_Hit_State : IState
{
    private EnemyBase enemy;
    public Enemy_Hit_State(EnemyBase newEnemy)
    {
        enemy = newEnemy;
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

    }
}

public class Enemy_Die_State : IState
{
    private EnemyBase enemy;
    public Enemy_Die_State(EnemyBase newEnemy)
    {
        enemy = newEnemy;
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

    }
}

