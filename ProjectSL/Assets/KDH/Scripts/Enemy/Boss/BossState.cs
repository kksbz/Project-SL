/*

    보스 구현 
    기본 베이스 패턴 구성
    1. Idle 상태
    2. 플레이어 탐색 상태(순찰)
    3. 플레이어 추적 상태
    4. 플레이어 대치 상태(플레이어가 일정 범위 내에 있을 경우 - 추적이 필요하지 않은 경우)
        4-1. 
    5. 플레이어 공격 상태
        5-1. 하위 공격 패턴 구성

    보스 패턴 기획
    1. Idle 상태 (아무런 동작 없이 플레이어가 인식 범위 내로 들어올 때까지 대기)
    2. 

*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSL.Enemy;

/// <summary>
/// 플레이어가 보스 방에 진입 전 아무런 행동도 하지 않는 상태
/// </summary>
public class Boss_None_State : IState
{
    private BossBase _boss;
    public Boss_None_State(BossBase newBoss)
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
        if (_boss.IsPlayerJoined)
        {
            if (_boss.IsIntroPlay)
            {
                _boss.SetState(new Boss_Idle_State(_boss));
            }
            else
            {
                _boss.SetState(new Boss_Intro_State(_boss));
            }
        }
    }
}


/// <summary>
/// 인트로 애니메이션을 재생하기 위한 상태 최초 1회만 동작할 예정
/// </summary>
public class Boss_Intro_State : IState
{
    private BossBase _boss;
    public Boss_Intro_State(BossBase newBoss)
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
}

/// <summary>
/// Idle 상태 플레이어를 조우하기 전 상태
/// </summary>
public class Boss_Idle_State : IState
{
    private BossBase _boss;
    private IEnumerator _coroutine;
    public Boss_Idle_State(BossBase newBoss)
    {
        _boss = newBoss;
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_IDLE);

        _coroutine = _boss.FieldOfViewSearch(0.2f);
        _boss.StartCoroutine(_coroutine);
    }

    public void OnExit()
    {
        _boss.StopCoroutine(_coroutine);
    }

    public void Update()
    {
        if (_boss.IsFieldOfViewFind())
        {
            _boss.SetState(new Boss_Confrontation_State(_boss));
        }
    }
}

/// <summary>
/// 대치 상태 플레이어를 주기적으로 쫓아가거나 바라보다 플레이어가 공격 범위 내에 있다면 공격 상태로 전환
/// </summary>
public class Boss_Confrontation_State : IState
{
    private BossBase _boss;
    private Transform _target;
    private float _distance = float.MaxValue;
    public Boss_Confrontation_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGET_MOVE);

        if (1 < _boss.ChaseTargets.Count)
        {
            foreach (var element in _boss.ChaseTargets)
            {
                float distance_ = Vector3.Distance(_boss.transform.position, element.position);
                if (distance_ < _distance)
                {
                    _distance = distance_;
                    _target = element;
                }
            }
        }
        else
        {
            _target = _boss.ChaseTargets[0];
        }

        _boss.TargetFollow(_target);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        //  타겟을 따라감
        _boss.TargetFollow(_target);

        Debug.Log($"TARGET : {_target.name}");

        //  타겟이 공격 범위 내에 위치한다면 공격 상태로 전환
        if (_boss.IsArrive(_boss.Status.attackRange))
        {
            Debug.Log($"distance : {_boss.Status.attackRange}");
            _boss.SetState(new Boss_Attack_State(_boss));
        }
    }
}

/// <summary>
/// 공격 상태 일정 확률로 정해진 공격 패턴을 수행 시킬 예정
/// </summary>
public class Boss_Attack_State : IState
{
    private BossBase _boss;
    public Boss_Attack_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (1f <= _boss.CurrentStateInfo.normalizedTime && !_boss.CurrentStateInfo.loop)
        {
            _boss.SetState(new Boss_Idle_State(_boss));
        }
    }
}

/// <summary>
/// 그로기 상태 우선 상태 제작 후 구현은 미정
/// </summary>
public class Boss_Groggy_State : IState
{
    private BossBase _boss;
    public Boss_Groggy_State(BossBase newBoss)
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
}

/// <summary>
/// Die 상태 보스가 죽었을 때 전환될 예정
/// </summary>
public class Boss_Die_State : IState
{
    private BossBase _boss;
    public Boss_Die_State(BossBase newBoss)
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
}