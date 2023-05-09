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

    public void OnAction()
    {

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
    public void OnAction()
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
        _boss.ActiveHpBar();

        AudioClip introSfx_ = _boss.FindAudioClip("Intro");
        _boss.SFX_Play(introSfx_, true);
    }

    public void Update()
    {
        if (_boss.IsFieldOfViewFind())
        {
            Transform target_ = default;
            float distance_ = float.MaxValue;

            if (1 < _boss.ChaseTargets.Count)
            {
                foreach (var element in _boss.ChaseTargets)
                {
                    float newDistance_ = Vector3.Distance(_boss.transform.position, element.position);
                    if (newDistance_ < distance_)
                    {
                        distance_ = newDistance_;
                        target_ = element;
                    }
                }
            }
            else
            {
                target_ = _boss.ChaseTargets[0];
            }

            Debug.Log($"target : {target_.name}");
            _boss.TargetFollow(target_, false);

            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
    public void OnAction()
    {

    }
}

/// <summary>
/// 생각 상태 상황에 따른 동작을 판단할 로직을 작성
/// </summary>
public class Boss_Thought_State : IState
{
    private BossBase _boss;
    private IEnumerator _coroutine;
    public Boss_Thought_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_THOUGHT);

        _coroutine = StateChangedDelay(1f);
        _boss.StartCoroutine(_coroutine);

        _boss.TargetFollow(_boss.Target, true);
    }

    public void OnExit()
    {
        _boss.StopCoroutine(_coroutine);
    }

    public void Update()
    {
        //  플레이어를 바라보는 동작을 수행할 예정 후에 Lerp를 사용해서 회전을 구현할 예정
        //_boss.transform.LookAt(_boss.Target);
        Vector3 dir = _boss.Target.transform.position - _boss.transform.position;

        //_boss.transform.rotation = Quaternion.Lerp(_boss.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
        _boss.TargetLook();
    }
    public void OnAction()
    {
    }

    IEnumerator StateChangedDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IState newState_ = _boss.Thought();

        if (newState_ == null || newState_ == default)
        {
            //_boss.SetState(new Boss_Thought_State(_boss));
            yield break;
        }

        _boss.SetState(newState_);
    }
}

/// <summary>
/// 추적 상태 플레이어를 추적함
/// </summary>
public class Boss_Chase_State : IState
{
    private BossBase _boss;
    private float _distance = float.MaxValue;
    private Transform _target = default;
    private IEnumerator _thoughtDelay;
    public Boss_Chase_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_MOVE);

        AudioClip runSFX_ = _boss.FindAudioClip("Run");
        _boss.SFX_Play_Loop(runSFX_);

        _boss.SetStop(false);

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

        _thoughtDelay = ThoutghtDelay();
        _boss.StartCoroutine(_thoughtDelay);
    }

    public void OnExit()
    {
        _boss.StopCoroutine(_thoughtDelay);
        _boss.SFX_Stop();
    }

    public void Update()
    {
        _boss.TargetFollow(_target);

        //  플레이어가 공격 범위 내에 들어왔다면 상태 전환
        if (_boss.IsInRange(_boss.Status.attackRange))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
            _boss.SetStop(true);
        }
    }

    public void OnAction()
    {
    }

    //  지정한 딜레이 시간 마다 상태 전환 조건을 체크, 다른 상태로 전환이 가능하다면 해다 상태로 전환
    IEnumerator ThoutghtDelay()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            IState thoughtState = _boss.Thought();
            Debug.Log($"판단 상태 : {thoughtState.ToString()} / 현재 상태 : {this.ToString()}");

            if (!(thoughtState is Boss_Chase_State))
            {
                _boss.SetStop(true);
                _boss.SetTrigger(EnemyDefineData.TRIGGER_THOUGHT);
                _boss.SetState(thoughtState);
            }

            yield return new WaitForSeconds(1f);
        }
    }

}

/// <summary>
/// 그로기 상태 구현은 미정
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
    public void OnAction()
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
        _boss.SetTrigger("Die");

        AudioClip deathSFX_ = _boss.FindAudioClip("Die");
        _boss.SFX_Play(deathSFX_, true);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        //  사망 애니메이션이 종료된 이후 감지
        // if (_boss.IsAnimationEnd(EnemyDefineData.ANIMATION_DIE))
        // {
        //     _boss.OnDie();
        // }
    }
    public void OnAction()
    {
        _boss.OnDie();
    }
}