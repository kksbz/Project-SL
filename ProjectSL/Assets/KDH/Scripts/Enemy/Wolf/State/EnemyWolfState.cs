using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSL.Enemy;

public class Enemy_Wolf_Idle_State : IState
{
    private EnemyWolf enemy;
    public Enemy_Wolf_Idle_State(EnemyWolf newEnemy)
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
            enemy.SetState(new Enemy_Wolf_Patrol_State(enemy));
        }
    }
}

public class Enemy_Wolf_Patrol_State : IState
{
    private EnemyWolf enemy;
    private IEnumerator coroutine;
    public Enemy_Wolf_Patrol_State(EnemyWolf newEnemy)
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
            enemy.SetState(new Enemy_Wolf_Idle_State(enemy));
        }
        if (enemy.IsFieldOfViewFind())
        {
            enemy.SetState(new Enemy_Wolf_Chase_State(enemy));
            Debug.Log($"적 찾음");
        }
    }
}

public class Enemy_Wolf_Chase_State : IState
{
    EnemyWolf enemy;
    private Transform _target;
    private float _distance = float.MaxValue;
    public Enemy_Wolf_Chase_State(EnemyWolf newEnemy)
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
            enemy.SetState(new Enemy_Wolf_Patrol_State(enemy));
        }
        if (enemy.IsArrive(enemy.Status.attackRange))
        {
            enemy.SetState(new Enemy_Wolf_Attack_State(enemy));
        }
    }
}

public class Enemy_Wolf_Attack_State : IState
{
    private EnemyWolf enemy;
    public Enemy_Wolf_Attack_State(EnemyWolf newEnemy)
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
    }
}

