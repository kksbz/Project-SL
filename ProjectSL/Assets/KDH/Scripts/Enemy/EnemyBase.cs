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
    protected IEnemyHpBar enemyHpBar = default;

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
    public IEnemyHpBar EnemyHpBar { get { return enemyHpBar; } protected set { enemyHpBar = value; } }
    public List<Transform> PatrolPoints { get { return MoveController.PatrolPoints; } }
    public List<Transform> ChaseTargets { get { return TargetResearch.Targets; } }
    public List<Collider> AttackCollider { get { return attackCollider; } protected set { attackCollider = value; } }
    #endregion

    public string currentState;

    protected void Start()
    {
        Init();
        Invoke("DropReward", 1f);
    }

    protected virtual void Init()
    {
        StateMachine = new EnemyStateMachine();

        TryGetComponent<IEnemyMoveController>(out moveController);
        TryGetComponent<IEnemyTargetResearch>(out targetResearch);
        TryGetComponent<IEnemyAnimator>(out enemyAnimator);
        TryGetComponent<IEnemyHpBar>(out enemyHpBar);

        MoveController.Init();
        SetSpeed(Status.currentMoveSpeed);

        EnemyAnimator.Init();

        TargetResearch.Init(ResearchStatus, new FieldOfView(transform, ResearchStatus));

        if (EnemyHpBar != null && EnemyHpBar != default)
        {
            EnemyHpBar.Init();
            InitHpBar(Status.maxHp, Status.currentHp);
        }

        //SetState(new Enemy_Idle_State(this));
    }

    protected void Update()
    {
        StateMachine.Update();
        currentState = CurrentState.ToString();
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
        damageable.TakeDamage(gameObject, damage);
    }

    public virtual void TakeDamage(GameObject damageCauser, float damage)
    {
        if (Status.currentHp <= 0) return;
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
        UpdateHpBar(Status.currentHp);
    }

    public virtual void DropReward()
    {

    }

    public virtual IState Thought()
    {
        return null;
    }
    #region EnemyHpBar
    public void InitHpBar(float maxHp, float currentHp)
    {
        EnemyHpBar.InitHpBar(maxHp, currentHp);
    }
    public void UpdateHpBar(float newHp)
    {
        EnemyHpBar.UpdateHpBar(newHp);
    }
    #endregion

    #region AttackCollider
    public virtual void Attack(string CurrentAnimationName, int onActionIndex)
    {

    }
    public virtual int RandomAttack()
    {
        return 0;
    }
    public virtual void TargetLook()
    {
        Vector3 dir_ = Target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir_);
        transform.rotation = rotation;
    }
    public virtual void TargetLook(Transform newTarget)
    {
        Vector3 dir_ = newTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir_);
        transform.rotation = rotation;
    }
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
    public void SetUpdateRotation(bool isRotation)
    {
        MoveController.SetUpdateRotation(isRotation);
    }
    public void Patrol()
    {
        MoveController.Patrol();
    }
    public void PatrolStop()
    {
        MoveController.PatrolStop();
    }
    public void TargetFollow(Transform newTarget)
    {
        MoveController.TargetFollow(newTarget);
    }
    public void TargetFollow(Vector3 newPosition)
    {
        MoveController.TargetFollow(newPosition);
    }
    public void TargetFollow(Transform newTarget, bool isFollow)
    {
        MoveController.TargetFollow(newTarget, isFollow);
    }
    public void TargetFollow(Vector3 newPosition, bool isFollow)
    {
        MoveController.TargetFollow(newPosition, isFollow);
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
    public bool IsArrivePatrol(float distance)
    {
        return MoveController.IsArrivePatrol(distance);
    }
    public bool IsInRange(float distance)
    {
        return MoveController.IsInRange(distance);
    }
    public bool IsPositionReachable(Vector3 newPosition)
    {
        return MoveController.IsPositionValid(newPosition);
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
    public bool IsName(string animationName)
    {
        return EnemyAnimator.CurrentStateInfo.IsName(animationName);
    }
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

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}

