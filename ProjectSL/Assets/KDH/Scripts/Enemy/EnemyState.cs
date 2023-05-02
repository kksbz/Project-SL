using UnityEngine;
using System.Collections;
using ProjectSL.Enemy;

public class Enemy_Idle_State : IState
{
    private EnemyBase _enemy;
    public Enemy_Idle_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetTrigger(EnemyDefineData.TRIGGER_IDLE);

        _enemy.SetState(new Enemy_Thought_State(_enemy));
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
/// 판단 상태 
/// </summary>
public class Enemy_Thought_State : IState
{
    private EnemyBase _enemy;
    public Enemy_Thought_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetTrigger(EnemyDefineData.TRIGGER_THOUGHT);

        _enemy.StartCoroutine(Thought(0.1f));
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        //_enemy.transform.LookAt(_enemy.Target);
    }
    public void OnAction()
    {

    }

    IEnumerator Thought(float delay)
    {
        yield return new WaitForSeconds(delay);

        IState newState_ = _enemy.Thought();

        if (newState_ == null || newState_ == default)
        {
            _enemy.SetState(new Enemy_Thought_State(_enemy));
            yield break;
        }

        _enemy.SetState(newState_);
    }
}

/// <summary>
/// 순찰 상태 해당 상태에서 인식 범위 내 플레이어가 존재하는지 탐색하는 로직이 실행 됨
/// </summary>
public class Enemy_Patrol_State : IState
{
    private EnemyBase _enemy;
    private IEnumerator coroutine;
    public Enemy_Patrol_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetStop(false);

        _enemy.SetSpeed(_enemy.Status.maxMoveSpeed * 0.5f);
        _enemy.SetFloat(EnemyDefineData.FLOAT_MOVESPEED, 0f);

        _enemy.Patrol();
        _enemy.StartCoroutine(MoveDelay());

        coroutine = _enemy.FieldOfViewSearch(0.2f);

        _enemy.StartCoroutine(coroutine);
    }

    public void OnExit()
    {
        _enemy.StopCoroutine(coroutine);
        //_enemy.PatrolStop();
    }

    public void Update()
    {
        //  순찰 지점에 도달했거나, 플레이어를 발견했다면
        if (_enemy.IsArrive(0.2f) || _enemy.IsFieldOfViewFind())
        {
            _enemy.SetState(new Enemy_Thought_State(_enemy));
        }
        // if (_enemy.IsFieldOfViewFind())
        // {
        //     _enemy.SetState(new Enemy_Thought_State(_enemy));
        //     Debug.Log($"적 찾음");
        // }
    }
    public void OnAction()
    {

    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1f);
        _enemy.SetTrigger(EnemyDefineData.TRIGGER_MOVE);
        _enemy.SetStop(false);
    }
}

public class Enemy_Chase_State : IState
{
    private EnemyBase _enemy;
    private Transform _target;
    private float _distance = float.MaxValue;
    public Enemy_Chase_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetSpeed(_enemy.Status.maxMoveSpeed);

        _enemy.SetTrigger(EnemyDefineData.TRIGGER_MOVE);
        _enemy.SetFloat(EnemyDefineData.FLOAT_MOVESPEED, 1f);

        if (1 < _enemy.ChaseTargets.Count)
        {
            foreach (var element in _enemy.ChaseTargets)
            {
                float distance_ = Vector3.Distance(_enemy.transform.position, element.position);
                if (distance_ < _distance)
                {
                    _distance = distance_;
                    _target = element;
                }
            }
        }
        else
        {
            _target = _enemy.ChaseTargets[0];
        }
        _enemy.SetStop(false);
        _enemy.TargetFollow(_target);
    }

    public void OnExit()
    {

    }

    public void Update()
    {
        _enemy.TargetFollow(_target);
        //  플레이어를 놓쳤거나, 플레이어가 공격 범위 내로 들어온다면
        if (_enemy.IsMissed(_enemy.Status.detectionRange) || _enemy.IsArrive(_enemy.Status.attackRange))
        {
            _enemy.SetState(new Enemy_Thought_State(_enemy));
        }
        // if (_enemy.IsArrive(_enemy.Status.attackRange))
        // {
        //     _enemy.SetState(new Enemy_Thought_State(_enemy));
        // }
    }
    public void OnAction()
    {

    }
}

public class Enemy_Attack_State : IState
{
    private EnemyBase _enemy;
    private string _currentAnimationName;
    private int _animationIndex;
    private int _onActionIndex;

    public Enemy_Attack_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetStop(true);

        float randNum_ = Random.value;

        _animationIndex = _enemy.RandomAttack();

        switch (_animationIndex)
        {
            case 1:
                _currentAnimationName = EnemyDefineData.ANIMATION_ATTACK_01;
                break;
            case 2:
                _currentAnimationName = EnemyDefineData.ANIMATION_ATTACK_02;
                break;
            case 3:
                _currentAnimationName = EnemyDefineData.ANIMATION_ATTACK_03;
                break;
            case 4:
                _currentAnimationName = EnemyDefineData.ANIMATION_ATTACK_04;
                break;
            case 5:
                _currentAnimationName = EnemyDefineData.ANIMATION_ATTACK_05;
                break;
            default:
                _currentAnimationName = EnemyDefineData.ANIMATION_ATTACK_01;
                _animationIndex = 1;
                break;
        }

        _enemy.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _enemy.SetInt(EnemyDefineData.INT_ATTACK_INDEX, _animationIndex);
    }

    public void OnExit()
    {
        _enemy.SetInt(EnemyDefineData.INT_ATTACK_INDEX, 0);
    }

    public void Update()
    {
        // 공격 애니메이션이 끝난 후
        if (_enemy.IsAnimationEnd(_currentAnimationName))
        {
            _enemy.SetState(new Enemy_Thought_State(_enemy));
            // // 타겟이 범위를 벗어났다면 놓친것으로 간주 PatrolState로 전환 그렇지 않다면 ChaseState로 전환
            // if (enemy.IsMissed(enemy.Status.detectionRange))
            // {
            //     enemy.SetState(new Enemy_Patrol_State(enemy));
            // }
            // else
            // {
            //     enemy.SetState(new Enemy_Chase_State(enemy));
            // }
        }
    }
    public void OnAction()
    {
        _enemy.Attack(_currentAnimationName, _onActionIndex);
        _onActionIndex++;
    }
}

public class Enemy_Dodge_State : IState
{
    private EnemyBase _enemy;
    private Transform _transform;
    public Enemy_Dodge_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
        _transform = _enemy.transform;
    }

    public void OnEnter()
    {
        _enemy.SetTrigger(EnemyDefineData.TRIGGER_DODGE);
        _enemy.SetUpdateRotation(false);
        _enemy.SetStop(false);

        Vector3 currentPosition = _transform.position;
        Quaternion currentRotation = _transform.rotation;

        Vector3 backwardDirection = -_transform.forward;
        Vector3 randomOffset = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 0f));
        Vector3 targetPosition = currentPosition + backwardDirection + randomOffset;

        Vector3 targetDirection = (targetPosition - currentPosition).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(-targetDirection, Vector3.up);
        _enemy.transform.rotation = targetRotation;

        Debug.Log($"targetposition : {targetPosition}");
        _enemy.TargetFollow(targetPosition);
    }

    public void OnExit()
    {
        _enemy.SetUpdateRotation(true);
        _enemy.TargetFollow(_enemy.Target, true);
    }

    public void Update()
    {
        if (_enemy.IsArrive(0.2f))
        {
            _enemy.SetState(new Enemy_Thought_State(_enemy));
        }
    }
    public void OnAction()
    {

    }
}

public class Enemy_Hit_State : IState
{
    private EnemyBase _enemy;
    public Enemy_Hit_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetStop(true);

        Vector3 direction_ = (_enemy.transform.position - _enemy.Target.position).normalized;
        float posX_ = direction_.x;
        float posY_ = direction_.z;

        _enemy.SetFloat(EnemyDefineData.FLOAT_HIT_X, posX_);
        _enemy.SetFloat(EnemyDefineData.FLOAT_HIT_Y, posY_);

        _enemy.SetTrigger(EnemyDefineData.TRIGGER_HIT);

    }

    public void OnExit()
    {

    }

    public void Update()
    {
        if (_enemy.IsAnimationEnd())
        {
            _enemy.SetState(new Enemy_Thought_State(_enemy));
        }
    }
    public void OnAction()
    {

    }
}

public class Enemy_Die_State : IState
{
    private EnemyBase _enemy;
    public Enemy_Die_State(EnemyBase newEnemy)
    {
        _enemy = newEnemy;
    }

    public void OnEnter()
    {
        _enemy.SetStop(true);

        Vector3 direction_ = (_enemy.transform.position - _enemy.Target.position).normalized;
        float posX_ = direction_.x;
        float posY_ = direction_.z;

        _enemy.SetFloat(EnemyDefineData.FLOAT_HIT_X, posX_);
        _enemy.SetFloat(EnemyDefineData.FLOAT_HIT_Y, posY_);

        _enemy.SetTrigger(EnemyDefineData.TRIGGER_DIE);
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

