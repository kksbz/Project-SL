using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSL.Enemy;

public class EnemyBase : CharacterBase, GData.IDamageable
{
    [Tooltip("Enemy의 Status")]
    [SerializeField]
    protected EnemyStatus status = default;
    [SerializeField]
    protected EnemyResearchStatus researchStatus = default;
    protected IStateMachine stateMachine = default;
    protected IEnemyMoveController moveController = default;
    protected IEnemyAnimator animator = default;
    protected IEnemyTargetResearch targetResearch = default;

    #region Property
    public EnemyStatus Status { get { return status; } protected set { status = value; } }
    public EnemyResearchStatus ResearchStatus { get { return researchStatus; } protected set { researchStatus = value; } }
    public IStateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
    public IEnemyMoveController MoveController { get { return moveController; } protected set { moveController = value; } }
    public IEnemyAnimator Animator { get { return animator; } protected set { animator = value; } }
    public IEnemyTargetResearch TargetResearch { get { return targetResearch; } protected set { targetResearch = value; } }
    public Queue<Transform> PatrolTargets { get { return MoveController.Targets; } }
    public List<Transform> ChaseTargets { get { return TargetResearch.Targets; } }
    #endregion

    protected void Start()
    {
        Init();
        StartCoroutines();
    }

    protected virtual void Init()
    {
        StateMachine = new EnemyStateMachine();

        TryGetComponent<IEnemyMoveController>(out moveController);
        TryGetComponent<IEnemyTargetResearch>(out targetResearch);
        TryGetComponent<IEnemyAnimator>(out animator);

        MoveController.Init();
        SetSpeed(Status.currentMoveSpeed);

        animator.Init();

        TargetResearch.Init(ResearchStatus, new FieldOfView(transform, ResearchStatus));

        //SetState(new Enemy_Idle_State(this));
    }

    protected void StartCoroutines()
    {
    }

    protected void Update()
    {
        StateMachine.Update();
    }

    public void TakeDamage(float damage)
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


    #region StateMachine
    public void SetState(IState newState)
    {
        StateMachine.SetState(newState);
    }
    #endregion

    #region IEnemyMoveController
    public void SetSpeed(float newSpeed)
    {
        moveController.SetSpeed(newSpeed);
    }
    public void Patrol()
    {
        moveController.Patrol();
    }
    public void TargetFollow(Transform newTarget)
    {
        moveController.TargetFollow(newTarget);
    }
    public bool IsArrive(float distance)
    {
        return moveController.IsArrive(distance);
    }
    public bool IsMissed(float distance)
    {
        return moveController.IsMissed(distance);
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
    public AnimatorStateInfo CurrentStateInfo { get { return Animator.CurrentStateInfo; } }
    public void SetTrigger(string parameter)
    {
        Animator.SetTrigger(parameter);
    }
    public void SetBool(string parameter, bool value)
    {
        Animator.SetBool(parameter, value);
    }
    public void SetFloat(string parameter, float value)
    {
        Animator.SetFloat(parameter, value);
    }
    public void SetInt(string parameter, int value)
    {
        Animator.SetInt(parameter, value);
    }
    #endregion
}

