using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [Tooltip("Enemy의 Status")]
    [SerializeField]
    protected EnemyStatus status = default;
    protected IStateMachine stateMachine = default;
    protected IEnemyMoveController moveController = default;

    #region Property
    public EnemyStatus Status { get { return status; } protected set { status = value; } }
    public IStateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
    public IEnemyMoveController MoveController { get { return moveController; } protected set { moveController = value; } }
    public Queue<Transform> Targets { get { return MoveController.Targets; } }
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

        MoveController.Init();
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
    public void Move()
    {
        MoveController.Move();
    }
    public void Stop()
    {
        MoveController.Stop();
    }
    public bool IsArrive()
    {
        return MoveController.IsArrive();
    }
    #endregion


}
