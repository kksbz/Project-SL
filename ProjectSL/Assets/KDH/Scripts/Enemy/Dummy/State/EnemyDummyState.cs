using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Dummy_Idle_State : IState
{
    EnemyDummy enemy;
    public Enemy_Dummy_Idle_State(EnemyDummy newEnemy)
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
        if (1 <= enemy.PatrolTargets.Count)
        {
            enemy.SetState(new Enemy_Dummy_Patrol_State(enemy));
        }
    }
}

public class Enemy_Dummy_Patrol_State : IState
{
    EnemyDummy enemy;
    IEnumerator coroutine;
    public Enemy_Dummy_Patrol_State(EnemyDummy newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
        enemy.Patrol();
        coroutine = enemy.FieldOfViewSearch(0.2f);
        enemy.StartCoroutine(coroutine);
    }

    public void OnExit()
    {
        enemy.StopCoroutine(coroutine);
        Debug.Log($"코루틴 스탑");
    }

    public void Update()
    {
        if (enemy.IsArrive(0.2f))
        {
            enemy.Patrol();
        }
        if (enemy.IsFieldOfViewFind())
        {
            enemy.SetState(new Enemy_Dummy_Chase_State(enemy));
            Debug.Log($"적 찾음");
        }
    }
}

public class Enemy_Dummy_Chase_State : IState
{
    EnemyDummy enemy;
    private Transform _target;
    private float _distance = float.MaxValue;
    public Enemy_Dummy_Chase_State(EnemyDummy newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
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
            enemy.SetState(new Enemy_Dummy_Patrol_State(enemy));
        }
        if (enemy.IsArrive(enemy.Status.attackRange))
        {
            enemy.SetState(new Enemy_Dummy_Attack_State(enemy));
        }
    }
}

public class Enemy_Dummy_Attack_State : IState
{
    EnemyDummy enemy;
    public Enemy_Dummy_Attack_State(EnemyDummy newEnemy)
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
}