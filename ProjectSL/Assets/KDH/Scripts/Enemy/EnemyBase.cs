using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [Tooltip("Enemy의 Status")]
    [SerializeField]
    protected EnemyStatus status = default;
    [SerializeField]
    protected EnemyResearchStatus researchStatus = default;
    protected IStateMachine stateMachine = default;
    protected IEnemyMoveController moveController = default;
    protected EnemyTargetResearch targetResearch = default;

    #region Property
    public EnemyStatus Status { get { return status; } protected set { status = value; } }
    public EnemyResearchStatus ResearchStatus { get { return researchStatus; } protected set { researchStatus = value; } }
    public IStateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
    public IEnemyMoveController MoveController { get { return moveController; } protected set { moveController = value; } }
    public EnemyTargetResearch TargetResearch { get { return targetResearch; } protected set { targetResearch = value; } }
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
        TryGetComponent<EnemyTargetResearch>(out targetResearch);

        MoveController.Init();
        TargetResearch.Init(ResearchStatus, new FieldOfView(transform, ResearchStatus));
    }

    protected void StartCoroutines()
    {
    }

    protected void Update()
    {
        StateMachine.Update();
    }

    #region StateMachine
    public void SetState(IState newState)
    {
        StateMachine.SetState(newState);
    }
    #endregion

    #region IEnemyMoveController
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

    #region IEnemyTargetResearch
    public IEnumerator FieldOfViewSearch(float delay)
    {
        return TargetResearch.FieldOfViewSearch(delay);
    }
    public bool IsFieldOfViewFind()
    {
        return TargetResearch.IsFieldOfViewFind();
    }
    #endregion

}
