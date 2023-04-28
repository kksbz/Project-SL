using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSL.Enemy;

public class EnemyBase : CharacterBase, GData.IDamageable, GData.IGiveDamageable
{
    [Tooltip("Enemy의 Status")]
    [SerializeField]
    protected EnemyStatus status = default;
    [SerializeField]
    protected EnemyResearchStatus researchStatus = default;
    protected IStateMachine stateMachine = default;
    protected IEnemyMoveController moveController = default;
    protected IEnemyAnimator enemyAnimator = default;
    protected IEnemyTargetResearch targetResearch = default;

    [Tooltip("공격시 활성화 될 공격 범위 콜라이더")]
    [SerializeField]
    protected List<Collider> attackCollider = default;

    #region Property
    public EnemyStatus Status { get { return status; } protected set { status = value; } }
    public EnemyResearchStatus ResearchStatus { get { return researchStatus; } protected set { researchStatus = value; } }
    public IStateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
    public IEnemyMoveController MoveController { get { return moveController; } protected set { moveController = value; } }
    public IEnemyAnimator EnemyAnimator { get { return enemyAnimator; } protected set { enemyAnimator = value; } }
    public IEnemyTargetResearch TargetResearch { get { return targetResearch; } protected set { targetResearch = value; } }
    public List<Transform> PatrolTargets { get { return MoveController.Targets; } }
    public List<Transform> ChaseTargets { get { return TargetResearch.Targets; } }
    public List<Collider> AttackCollider { get { return attackCollider; } protected set { attackCollider = value; } }
    #endregion

    protected void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        StateMachine = new EnemyStateMachine();

        TryGetComponent<IEnemyMoveController>(out moveController);
        TryGetComponent<IEnemyTargetResearch>(out targetResearch);
        TryGetComponent<IEnemyAnimator>(out enemyAnimator);

        MoveController.Init();
        SetSpeed(Status.currentMoveSpeed);

        EnemyAnimator.Init();

        TargetResearch.Init(ResearchStatus, new FieldOfView(transform, ResearchStatus));

        //SetState(new Enemy_Idle_State(this));
    }

    protected void Update()
    {
        StateMachine.Update();
    }

    public void OnTriggerEnter(Collider other)
    {
        GData.IDamageable object_ = other.GetComponent<GData.IDamageable>();

        if (object_ == null || object_ == default)
        {
            /*  Do Nothing  */
        }
        else
        {
            GiveDamage(object_, Status.currentAttackDamage);
        }
    }

    public virtual void GiveDamage(GData.IDamageable damageable, float damage)
    {
        Debug.Log($"데미지를 입힘 / 대상 : {damageable.ToString()} / 데미지 : {damage}");
        damageable.TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        if (Status.currentHp - damage <= 0)
        {
            Status.currentHp = 0;
            SetState(new Enemy_Die_State(this));
        }
        else
        {
            Status.currentHp -= damage;
            SetState(new Enemy_Hit_State(this));
        }
    }

    public virtual IState Thought(Transform newTarget)
    {
        return null;
    }

    public virtual IState Thought()
    {
        return null;
    }

    #region AttackCollider
    public void SetAttackColliderEnabled(bool newEnabled)
    {
        foreach (var iterator in AttackCollider)
        {
            iterator.enabled = newEnabled;
        }
    }
    public void SetAttackColliderEnabled(int index, bool newEnabled)
    {
        AttackCollider[index].enabled = newEnabled;
    }
    public void NotAttackColliderEnabled()
    {
        foreach (var iterator in AttackCollider)
        {
            iterator.enabled = !iterator.enabled;
        }
    }
    public void NotAttackColliderEnabled(int index)
    {
        AttackCollider[index].enabled = !AttackCollider[index].enabled;
    }
    #endregion


    #region StateMachine
    public IState CurrentState { get { return StateMachine.CurrentState; } }
    public IState PreviousState { get { return StateMachine.PreviousState; } }
    public void SetState(IState newState)
    {
        StateMachine.SetState(newState);
    }
    public void OnAction()
    {
        StateMachine.OnAction();
    }
    #endregion

    #region IEnemyMoveController
    public Transform Target { get { return MoveController.Target; } }
    public UnityEngine.AI.NavMeshAgent NavMeshAgent { get { return MoveController.NavMeshAgent; } }
    public void SetSpeed(float newSpeed)
    {
        MoveController.SetSpeed(newSpeed);
    }
    public void SetStop(bool isStopped)
    {
        MoveController.SetStop(isStopped);
    }
    public void Patrol()
    {
        MoveController.Patrol();
    }
    public void TargetFollow(Transform newTarget)
    {
        MoveController.TargetFollow(newTarget);
    }
    public void TargetFollow(Transform newTarget, bool isFollow)
    {
        MoveController.TargetFollow(newTarget, isFollow);
    }
    public void Warp()
    {
        MoveController.Warp();
    }
    public void Warp(Vector3 newPos)
    {
        MoveController.Warp(newPos);
    }
    public bool IsArrive(float distance)
    {
        return MoveController.IsArrive(distance);
    }
    public bool IsMissed(float distance)
    {
        return MoveController.IsMissed(distance);
    }
    public bool IsNavMeshRangeChecked(float ranged)
    {
        return MoveController.IsNavMeshRangeChecked(ranged);
    }
    public bool IsRangedChecked(float ranged)
    {
        return MoveController.IsRangeChecked(ranged);
    }
    #endregion

    #region EnemyTargetResearch
    public IEnumerator FieldOfViewSearch(float delay)
    {
        return TargetResearch.FieldOfViewSearch(delay);
    }
    public bool IsFieldOfViewFind()
    {
        return TargetResearch.IsFieldOfViewFind;
    }
    #endregion

    #region IEnemyAnimator
    public AnimatorStateInfo CurrentStateInfo { get { return EnemyAnimator.CurrentStateInfo; } }
    public void SetTrigger(string parameter)
    {
        EnemyAnimator.SetTrigger(parameter);
    }
    public void SetBool(string parameter, bool value)
    {
        EnemyAnimator.SetBool(parameter, value);
    }
    public void SetFloat(string parameter, float value)
    {
        EnemyAnimator.SetFloat(parameter, value);
    }
    public void SetInt(string parameter, int value)
    {
        EnemyAnimator.SetInt(parameter, value);
    }
    public bool IsAnimationEnd()
    {
        return EnemyAnimator.IsAnimationEnd();
    }
    public bool IsAnimationEnd(string animationName)
    {
        return EnemyAnimator.IsAnimationEnd(animationName);
    }
    public bool IsAnimationPlaying(string animationName)
    {
        return enemyAnimator.IsAnimationPlaying(animationName);
    }
    #endregion
}

